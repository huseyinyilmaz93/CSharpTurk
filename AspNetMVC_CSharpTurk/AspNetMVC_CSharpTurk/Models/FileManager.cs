using AspNetMVC_CSharpTurk.Models.FileManagerModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace AspNetMVC_CSharpTurk.Models
{
    public class FileManager
    {
        private string RootYol = "~/Files/Uploads";

        private string IconYolu = "/Content/Images/klasor.png"; 

        private List<string> izinVerilenUzantilar =
            new List<string> { ".ai", ".asx", ".avi", ".bmp", ".csv", ".dat", ".doc", ".docx", ".epub", ".fla", ".flv", ".gif", ".html", ".ico", ".jpeg", ".jpg", ".m4a", ".mobi", ".mov", ".mp3", ".mp4", ".mpa", ".mpg", ".mpp", ".pdf", ".png", ".pps", ".ppsx", ".ppt", ".pptx", ".ps", ".psd", ".qt", ".ra", ".ram", ".rar", ".rm", ".rtf", ".svg", ".swf", ".tif", ".txt", ".vcf", ".vsd", ".wav", ".wks", ".wma", ".wmv", ".wps", ".xls", ".xlsx", ".xml", ".zip" }; 

        private List<string> resimUzantilari =
            new List<string> { ".jpg", ".png", ".jpeg", ".gif", ".bmp" }; 

        public Tuple<bool,string> SetRoot(string kullaniciAdi)
        {
            if (!kullaniciAdi.Equals("admin")) this.RootYol = string.Concat(this.RootYol, "/", kullaniciAdi);
            else if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath(string.Concat(this.RootYol, "/", kullaniciAdi))))
            {
                this.KlasorEkle("~/Files/Uploads/", kullaniciAdi);
                RootYol = string.Concat(RootYol, "/", kullaniciAdi);
            }
            return new Tuple<bool,string>(true, this.RootYol);
        }

        public bool ResimMi(FileInfo fileInfo)
        {
            return resimUzantilari.Contains(Path.GetExtension(fileInfo.FullName).ToLower());
        }

        public bool RootIcindeMi(string path)
        {
            return path != null && Path.GetFullPath(path).StartsWith(Path.GetFullPath(RootYol));
        }

        public Tuple<bool,HttpStatusCode,string,List<Dosya>> GetKlasorDizin(string yol)
        {
            if (!RootIcindeMi(yol))
            {
                return new Tuple<bool,HttpStatusCode,string,List<Dosya>>(false,HttpStatusCode.BadRequest,"Belirtilen dosya yolu ana dizinin dışında",null);
            }
            if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath(yol)))
            {
                return new Tuple<bool,HttpStatusCode,string,List<Dosya>>(false,HttpStatusCode.BadRequest,"Belirtilen dizin bulunamadı",null);
            }

            DirectoryInfo rootKlasorBilgi = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(yol));

            List<Dosya> dosyalar = new List<Dosya>();

            foreach (DirectoryInfo klasorBilgi in rootKlasorBilgi.GetDirectories())
            {
                Dosya dosya = new Dosya();
                dosya.DosyaYolu = Path.Combine(yol, klasorBilgi.Name);
                dosya.DosyaYolu = dosya.DosyaYolu.Replace(@"\","/");
                dosya.DosyaAdi = klasorBilgi.Name;
                dosya.DosyaTuru = "dir";
                dosya.ResimURL = IconYolu;
                DosyaOzellik ozellikler = new DosyaOzellik();
                ozellikler.OlusturulmaTarihi = klasorBilgi.CreationTime;
                ozellikler.DegistirilmeTarihi = klasorBilgi.LastWriteTime;
                ozellikler.Genislik = "0";
                ozellikler.Yukseklik = "0";
                ozellikler.Boyut = "0";
                dosya.DosyaOzellikleri = ozellikler;
                dosyalar.Add(dosya);
            }

            foreach (FileInfo dosyaBilgi in rootKlasorBilgi.GetFiles())
            {
                Dosya dosya = new Dosya();
                dosya.DosyaYolu = Path.Combine(yol, dosyaBilgi.Name);
                dosya.DosyaYolu = dosya.DosyaYolu.Replace(@"\", "/");
                dosya.DosyaAdi = dosyaBilgi.Name;
                dosya.DosyaTuru = dosyaBilgi.Extension.Replace(".", "");

                if (ResimMi(dosyaBilgi))
                {
                    dosya.ResimURL = Path.Combine(yol, dosyaBilgi.Name) + "?" + dosyaBilgi.LastWriteTime.Ticks.ToString().Substring(1);
                }
                else
                {
                    var icon = String.Format("{0}{1}.png", IconYolu, dosyaBilgi.Extension.Replace(".", ""));
                    if (!System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(icon)))
                    {
                        dosya.ResimURL = icon;
                    }
                }
                DosyaOzellik ozellikler = new DosyaOzellik();
                ozellikler.OlusturulmaTarihi = dosyaBilgi.CreationTime;
                ozellikler.DegistirilmeTarihi = dosyaBilgi.LastWriteTime;
                if (ResimMi(dosyaBilgi))
                {
                    try
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromFile(dosyaBilgi.FullName))
                        {
                            ozellikler.Yukseklik = img.Height.ToString();
                            ozellikler.Genislik = img.Width.ToString();
                        }
                    }
                    catch
                    {
                    }
                }
                ozellikler.Boyut = dosyaBilgi.Length.ToString();
                dosya.DosyaOzellikleri = ozellikler;
                dosya.ResimURL = dosya.ResimURL.Substring(1);
                dosyalar.Add(dosya);
            }

            return new Tuple<bool,HttpStatusCode,string,List<Dosya>>(true,HttpStatusCode.OK,string.Concat("İşlem başarılı. ",dosyalar.Count.ToString()," adet dosya/klasör bulunmaktadır"),dosyalar);
        } 

        public Tuple<bool,HttpStatusCode,string,List<Dosya>> DosyaBilgi(string yol)
        {
            if (!RootIcindeMi(yol))
            {
                return new Tuple<bool,HttpStatusCode, string, List<Dosya>>(false,HttpStatusCode.BadRequest, "Belirtilen dosya yolu ana dizinin dışında", null);
            }
            if (!System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(yol)) && !Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath(yol)))
            {
                return new Tuple<bool,HttpStatusCode, string, List<Dosya>>(false,HttpStatusCode.BadRequest, "Belirtilen dizin bulunamadı", null);
            }

            List<Dosya> dosyalar = new List<Dosya>();

            FileAttributes attr = System.IO.File.GetAttributes(System.Web.Hosting.HostingEnvironment.MapPath(yol));

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo DirInfo = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(yol));

                Dosya dosya = new Dosya();
                dosya.DosyaYolu = yol;
                dosya.DosyaYolu = dosya.DosyaYolu.Replace(@"\", "/");
                dosya.DosyaAdi = DirInfo.Name;
                dosya.DosyaTuru = "dir";
                dosya.ResimURL = IconYolu;
                DosyaOzellik ozellikler = new DosyaOzellik();
                ozellikler.OlusturulmaTarihi = DirInfo.CreationTime;
                ozellikler.DegistirilmeTarihi = DirInfo.LastWriteTime;
                ozellikler.Genislik = "0";
                ozellikler.Yukseklik = "0";
                ozellikler.Boyut = "0";
                dosya.DosyaOzellikleri = ozellikler;
                dosyalar.Add(dosya);
            }
            else
            {
                FileInfo fileInfo = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(yol));
                Dosya dosya = new Dosya();
                dosya.DosyaYolu = yol;
                dosya.DosyaAdi = fileInfo.Name;
                dosya.DosyaTuru = fileInfo.Extension.Replace(".", "");

                if (ResimMi(fileInfo))
                {
                    dosya.ResimURL = yol + "?" + fileInfo.LastWriteTime.Ticks.ToString().Substring(1);
                }
                else
                {
                    dosya.ResimURL = String.Format("{0}{1}.png", IconYolu, fileInfo.Extension.Replace(".", "").Substring(1));
                }
                DosyaOzellik ozellik = new DosyaOzellik();
                ozellik.OlusturulmaTarihi = fileInfo.CreationTime;
                ozellik.DegistirilmeTarihi = fileInfo.LastWriteTime;

                if (ResimMi(fileInfo))
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(System.Web.Hosting.HostingEnvironment.MapPath(yol)))
                    {
                        ozellik.Yukseklik = img.Height.ToString();
                        ozellik.Genislik = img.Width.ToString();
                    }
                }
                ozellik.Boyut = fileInfo.Length.ToString();
                dosya.DosyaOzellikleri= ozellik;
                dosyalar.Add(dosya);
            }

            return new Tuple<bool,HttpStatusCode, string, List<Dosya>>(true,HttpStatusCode.OK, string.Concat("İşlem başarılı. ", dosyalar.Count.ToString(), " adet dosya/klasör bulunmaktadır"), dosyalar);

        } 

        public Tuple<bool,HttpStatusCode, string> Tasi(string eskiYol, string yeniYol)
        {
            if (!RootIcindeMi(eskiYol))
            {
                return new Tuple<bool,HttpStatusCode, string>(false,HttpStatusCode.BadRequest,"Belirtilen eski dosya yolu anadizin içerisinde değil");
            }
            else if (!RootIcindeMi(yeniYol))
            {
                return new Tuple<bool, HttpStatusCode, string>(false,HttpStatusCode.BadRequest, "Belirtilen yeni dosya yolu anadizin içerisinde değildir");
            }
            else if (!System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(eskiYol)) && !Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath(eskiYol)))
            {
                return new Tuple<bool, HttpStatusCode, string>(false,HttpStatusCode.BadRequest, "Taşımak için seçilen dosya bulunamadı.");
            }

            FileAttributes attr = System.IO.File.GetAttributes(System.Web.Hosting.HostingEnvironment.MapPath(eskiYol));


            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo eskiKlasorBilgi = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(eskiYol));
                yeniYol = Path.Combine(yeniYol, eskiKlasorBilgi.Name);
                Directory.Move(System.Web.Hosting.HostingEnvironment.MapPath(eskiYol), System.Web.Hosting.HostingEnvironment.MapPath(yeniYol));
                DirectoryInfo yeniKlasorBilgi = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(yeniYol));
            }
            else
            {
                FileInfo eskiDosya = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(eskiYol));
                FileInfo yeniDosya = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(yeniYol));
                if (yeniDosya.Extension != eskiDosya.Extension)
                {
                    //Don't allow extension to be changed
                    yeniDosya = new FileInfo(Path.ChangeExtension(yeniDosya.FullName, eskiDosya.Extension));
                }
                System.IO.File.Move(eskiDosya.FullName, yeniDosya.FullName);

            }

            return new Tuple<bool,HttpStatusCode,string>(true,HttpStatusCode.OK,"İşlem başarılı");
        } 

        public Tuple<bool,HttpStatusCode,string,List<Dosya>> YenidenAdlandir(string yol,string eskiIsim, string yeniIsim)
        {
            string eskiYol = yol;
            yol = string.Concat(yol, "/", eskiIsim);
            if (!RootIcindeMi(yol))
            {
                return new Tuple<bool, HttpStatusCode, string,List<Dosya>>(false,HttpStatusCode.BadRequest, "Belirtilen dosya belirtilen ana dizinin dışında",null);
            }
            if (!System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(yol)) && !Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath(yol)))
            {
                return new Tuple<bool, HttpStatusCode, string,List<Dosya>>(false,HttpStatusCode.BadRequest, "Belirtilen dosya bulunamadı",null);
            }

            FileAttributes attr = System.IO.File.GetAttributes(System.Web.Hosting.HostingEnvironment.MapPath(yol));

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo eskiKlasorBilgi = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(yol));
                Directory.Move(System.Web.Hosting.HostingEnvironment.MapPath(yol), Path.Combine(eskiKlasorBilgi.Parent.FullName, yeniIsim));
                DirectoryInfo yeniKlasorBilgi = new DirectoryInfo(Path.Combine(eskiKlasorBilgi.Parent.FullName, yeniIsim));

            }
            else
            {
                FileInfo eskiDosyaBilgi = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(yol));
                //Don't allow extension to be changed
                yeniIsim = Path.GetFileNameWithoutExtension(yeniIsim) + eskiDosyaBilgi.Extension;
                FileInfo yeniDosyaBilgi = new FileInfo(Path.Combine(eskiDosyaBilgi.Directory.FullName, yeniIsim));
                System.IO.File.Move(eskiDosyaBilgi.FullName, yeniDosyaBilgi.FullName);
            }

            return new Tuple<bool,HttpStatusCode, string,List<Dosya>>(true,HttpStatusCode.OK, "İşlem başarılı",this.GetKlasorDizin(eskiYol).Item4);
        } 

        public Tuple<bool,HttpStatusCode, string, List<Dosya>> KlasorEkle(string yol, string yeniKlasor)
        {
            if (!RootIcindeMi(yol))
            {
                return new Tuple<bool,HttpStatusCode,string,List<Dosya>>(false,HttpStatusCode.BadRequest,"Belirtilen yol ana dizin içerisinde bulunmamaktadır",null);
            }
            //Benzer klasör varsa Yeni Klasör(1) şeklinde oluşturmak için kullanılır.
            string tempFolder = yeniKlasor;
            int i = 1;
            while(true)
            {
                if (Directory.Exists(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(string.Concat(yol, "/", tempFolder)))))
                    tempFolder = string.Concat(yeniKlasor, "(", i++, ")");
                else
                {
                    Directory.CreateDirectory(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(yol), tempFolder));
                    //Klasör eklendikten sonra yeniden dizin bilgisi alınıyor...
                    return new Tuple<bool, HttpStatusCode, string,List<Dosya>>(true, HttpStatusCode.OK, "İşlem başarılı", this.GetKlasorDizin(yol).Item4);
                } 
            }


        }

        public Tuple<bool, HttpStatusCode, string, List<Dosya>> Sil(string yol,string dosyaAdi)
        {
            if (!RootIcindeMi(yol))
            {
                return new Tuple<bool, HttpStatusCode, string,List<Dosya>>(false,HttpStatusCode.BadRequest, "Silinmek istenen dosya ana dizinin dışında bulunmaktadır.",null);
            }
            if (!System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(yol)) && !Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath(yol)))
            {
                return new Tuple<bool, HttpStatusCode, string,List<Dosya>>(false,HttpStatusCode.BadRequest, "Dosya bulunamadı",null);
            }

            FileAttributes attr = System.IO.File.GetAttributes(System.Web.Hosting.HostingEnvironment.MapPath(yol));

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Directory.Delete(System.Web.Hosting.HostingEnvironment.MapPath(string.Concat(yol,"/",dosyaAdi)), true);
            }
            else
            {
                System.IO.File.Delete(System.Web.Hosting.HostingEnvironment.MapPath(string.Concat(yol,"/",dosyaAdi)));
            }

            return new Tuple<bool,HttpStatusCode,string,List<Dosya>>(true,HttpStatusCode.OK,"İşlem başarılı",this.GetKlasorDizin(yol).Item4);
        } 

        public Tuple<bool,HttpStatusCode,string,List<Dosya>> DosyaYukle(string yol, HttpFileCollection dosyalar)
        {
            if (dosyalar.Count == 0 || dosyalar[0].ContentLength == 0 || dosyalar == null)
            {
                return new Tuple<bool,HttpStatusCode, string,List<Dosya>>(false,HttpStatusCode.BadRequest, "Dosya eklemek için dosya gönderilmiş olması gerekmektedir",null);
            }
            else
            {
                if (!RootIcindeMi(yol))
                {
                    return new Tuple<bool,HttpStatusCode, string, List<Dosya>>(false,HttpStatusCode.BadRequest,"Belirtilen dosya ana dizinde değil",null);
                }
                else
                {
                    bool bayrak = false;
                    for (int i = 0; i < dosyalar.Count; i++)
                    {
                        if (! this.izinVerilenUzantilar.Contains(Path.GetExtension(dosyalar[i].FileName).ToLower()))
                        {
                            bayrak = true;
                            break;
                        }
                    }

                    if (bayrak) return new Tuple<bool,HttpStatusCode, string,List<Dosya>>(false,HttpStatusCode.MethodNotAllowed,"Gönderilen dosya/lar arasında izin verilmeyen bir uzantı bulunmaktadır.",null);

                    try
                    {
                        for (int i = 0; i < dosyalar.Count; i++)
                        {
                            //Dosya isimlerinde belirli karakterlere izin verilir
                            var baseFileName = Regex.Replace(Path.GetFileNameWithoutExtension(dosyalar[i].FileName), @"[^\w_-]", "");
                            var dosyaYolu = Path.Combine(yol, baseFileName + Path.GetExtension(dosyalar[i].FileName));

                            //Dosya ismini benzersiz yapma
                            int j = 0;
                            while (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(dosyaYolu)))
                            {
                                j = j + 1;
                                baseFileName = Regex.Replace(baseFileName, @"_[\d]+$", "");
                                dosyaYolu = Path.Combine(yol, baseFileName + "_" + j + Path.GetExtension(dosyalar[i].FileName));
                            }
                            dosyalar[i].SaveAs(System.Web.Hosting.HostingEnvironment.MapPath(dosyaYolu));
                        }
                        return new Tuple<bool,HttpStatusCode, string,List<Dosya>>(true,HttpStatusCode.OK, "İşlem başarılı",this.GetKlasorDizin(yol).Item4);
                    }
                    catch (Exception)
                    {
                        return new Tuple<bool,HttpStatusCode, string,List<Dosya>>(false,HttpStatusCode.InternalServerError,"Dosyaları kaydederken bir hata oluştu",null);
                    }
                }
            }
        }

        //public string Replace(string path)
        //{
        //    if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
        //    {
        //        return Error("No file provided.");
        //    }
        //    else if (!IsInRootPath(path))
        //    {
        //        return Error("Attempt to replace file outside root path");
        //    }
        //    else
        //    {
        //        var fi = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(path));
        //        HttpPostedFileBase file = Request.Files[0];
        //        if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
        //        {
        //            return Error("Uploaded file type is not allowed.");
        //        }
        //        else if (!Path.GetExtension(file.FileName).Equals(fi.Extension))
        //        {
        //            return Error("Replacement file must have the same extension as the file being replaced.");
        //        }
        //        else if (!fi.Exists)
        //        {
        //            return Error("File to replace not found.");
        //        }
        //        else
        //        {
        //            file.SaveAs(fi.FullName);

        //            return "<textarea>" + json.Serialize(new
        //            {
        //                Path = path.Replace("/" + fi.Name, ""),
        //                Name = fi.Name,
        //                Error = "No error",
        //                Code = 0
        //            }) + "</textarea>";
        //        }
        //    }
        //}

    }
}
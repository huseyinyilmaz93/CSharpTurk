var module = angular.module('angular', ['textAngular', 'ngSanitize']);

module.controller('Controller', function ($scope, $http) {
    //do something
    //$scope.gecerliKullanici = {};
    //$scope.tumHatalar = {}
    //$http({ method: 'GET', url: '/api/HesapAPI/KullaniciGetir/' + $scope.yazarId })
    //    .success(function (data,status,headers,config) {
    //        if (status == 200) {
    //            $scope.gecerliKullanici = data;
    //        }
    //    }).error(function (data, status, headers, config) {
    //        if (status == 405) {
    //            $scope.tumHatalar = data.ModelState;
    //        }
    //    })
});

module.controller('HataBildirController', ['$scope', '$http', function ($scope, $http) {
    $scope.hataBildirTumHatalar = {}
    $scope.submitHata = function () {
        $http({
            method: 'POST',
            url: '/api/HataBildirAPI/HataKaydet',
            contentType: 'application/json',
            data: $scope.yeniHata,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            }
        }).success(function (data, status, headers, config) {
            alert('Bildiriminiz için teşekkürler en yakın zamanda ilgilenilecektir.');
            $scope.yeniHata.email = "";
            $scope.yeniHata.hataMesaji = "";
            $scope.hataBildirTumHatalar = "";
        }).error(function (data, status, headers, config) {
            $scope.hataBildirTumHatalar = data.ModelState;
        })
    }
}]);

module.controller('GirisYapController', ['$scope', '$http', function ($scope, $http) {
    $scope.kullanici = {};
    $scope.oturumAc = {};
    $scope.tumHatalar = {};
    $scope.submitForm = function () {
        $http({
            method: 'POST',
            url: '/api/HesapAPI/GirisYap',
            contentType: 'application/json',
            data: $scope.kullanici,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            }
        }).success(function (data, status, headers, config) {
            $http({
                method: 'POST',
                url: '/Hesap/Giris',
                contentType: 'application/json',
                data: data,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            }).success(function (data, status, headers, config) {
                if (status == 200) {
                    if (data == "Admin") {
                        window.location = "/Admin/Index";
                    } else if (data == "Yazar") {
                        window.location = "/Yazar/Index";
                    } else if (data == "OturumHatasi") {
                        alert('Oturum açarken hata oluştu. Tekrar deneyin.');
                        window.location.reload();
                    } else {
                        window.location = "/Makale/Makaleler";
                    }
                }
            }).error(function (data, status, headers, config) {
                //window.location.reload();
                $scope.tumHatalar = data.ModelState;
            })
        }).error(function (data, status, headers, config) {
            //window.location.reload();
            $scope.tumHatalar = data.ModelState;
        })
    }
    $scope.sifremiUnuttumGorunum = false;

    $scope.sifremiUnuttumClick = function () {
        window.location = "/Site/SifremiUnuttum";
    };

    $scope.MailKontrol = function () {
        $http({
            method: 'POST',
            url: '/api/HesapAPI/MailKontrol',
            contentType: 'application/json',
            data: $scope.mail,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            }
        })
        .success(function (data, status, headers, config) {
            $scope.hataGorunum = true;
            alert('Şifre değişim isteğiniz alınmıştır. E postanızı kontrol edin!');
            window.location = "/Anasayfa";
        }).error(function (data, status, headers, config) {
            $scope.hataGorunum = false;
            $scope.mesaj = data.Message;
        })
    }

}]);

module.controller('KullaniciController', ['$scope', '$http', function ($scope, $http) {
    $scope.gecerliKullanici = {};
    $scope.tumHatalar = {}
    $http({ method: 'GET', url: '/api/HesapAPI/' + $scope.kullaniciId }).success(function (data, status, headers, config) {
        if (status == 200) {
            $scope.gecerliKullanici = data;
        }
    }).error(function (data, status, headers, config) {
        if (status == 405) {
            $scope.tumHatalar = data.ModelState;
        }
    })
}]);

module.controller('AramaController', ['$scope', '$http', function ($scope, $http) {
    $scope.hataGorunmezlik = false;
    angular.element(document).ready(function () {
        $scope.hataGorunmezlik = false;
    })
    $scope.Ara = function () {
        if ($scope.aramaFiltresi.length < 3)
            $scope.hataGorunmezlik = true;
        else {
            $scope.hataGorunmezlik = false;
            window.location = '/Site/AramaSonucu/' + $scope.aramaFiltresi;
        }
    }
}]);

module.controller('MakaleTipController', ['$scope', '$http', function ($scope, $http) {
    $scope.makaleTipleri = {};
    $http({ method: 'GET', url: '/api/MakaleAPI/MakaleTipleri' }).success(function (data, status, headers, config) {
        $scope.makaleTipleri = data;
    })
}])

module.controller('NavbarController', ['$scope', '$http', function ($scope, $http) {
    $http({ method: 'GET', url: '/api/StatikSayfaAPI/StatikSayfalar' }).success(function (data, status, headers, config) {
        $scope.sayfalar = data;
    });
}]);

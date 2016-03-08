/// <reference path="angular.min.js" />
/// <reference path="angular-sanitize.js" />

//Tüm makaleler [List Makale]
angular.module('tumMakaleler', []).
    controller('tumMakalelerController', function ($scope, $http) {
        $http({ method: 'GET', url: '/api/MakaleAPI' }).success(function (data, status, headers, config) {
            $scope.makaleler = data;
        }).error(function (data,status,headers,config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        });
    });
angular.bootstrap(document.getElementById('ang_makaleler'), ['tumMakaleler']);

//Makale Oluşturma [Create Makale]
angular.module('makaleOlustur', ['textAngular']).
    controller('makaleOlusturController', function ($scope, $http) {
        $scope.makale = {};
        $scope.submitForm = function () {
            $http({
                method: 'POST',
                url: '/api/MakaleAPI/' + $scope.yazarId,
                contentType: 'application/json',
                data: $scope.makale,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            })
              .success(function (data) {
                  if (data.errors) {
                      $scope.errorMakaleBaslik = data.errors.MakaleBaslik;
                      $scope.errorMakaleGovde = data.errors.MakaleGovde;
                      $scope.errorMakaleOzet = data.errors.MakaleOzet;
                  } else {
                      $scope.message = data.message;
                  }
          });
    };
});
angular.bootstrap(document.getElementById('ang_makaleler'), ['makaleOlustur']);

//MakaleId'ye göre x makalesini getirme [Get Makale]
var module = angular.module('makaleGetir', ['ngSanitize']).
    controller('makaleGetirController', function ($scope, $http, $sce, $locale) {
        $scope.makale = {};
        $scope.yeniYorum = {};
        $scope.yorumlar = {};
        $scope.yorumSayisi = 0;

        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/MakaleAPI/' + $scope.makaleId }).success(function (data, status, headers, config) {
                $scope.makale = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.makale.MakaleGovde);
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });

            $http({ method: 'GET', url: '/api/YorumAPI/' + $scope.makaleId }).success(function (data, status, headers, config) {
                $scope.yorumlar = data;
                $scope.yorumSayisi = data.length;
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        })

        $scope.submitForm = function () {
            $http({
                method: 'POST',
                url: '/api/YorumAPI/' + $scope.makaleId,
                contentType: 'application/json',
                data: $scope.yeniYorum,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            })
                .success(function (data, status, headers, config) {
                    //işlem başarılı
                    alert('Yorum kaydedildi!');
                }).error(function (data, status, headers, config) {
                    alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
                })
        }

    });

//Makale Düzenleme [Edit Makale]
angular.module('makaleDuzenle', ['textAngular']).
    controller('makaleDuzenleController', function ($scope, $http, $location) {
        $scope.eskiMakale = {};
        $scope.yeniMakale = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/MakaleAPI/' + $scope.makaleId }).success(function (data, status, headers, config) {
                $scope.eskiMakale = data;
                $scope.makaleBaslik = data.MakaleBaslik;
                $scope.makaleGovde = data.MakaleGovde;
                $scope.makaleOzet = data.MakaleOzet;
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        });

        $scope.submitForm = function () {
            $scope.yeniMakale.MakaleBaslik = $scope.makaleBaslik;
            $scope.yeniMakale.MakaleGovde = $scope.makaleGovde;
            $scope.yeniMakale.MakaleOzet = $scope.makaleOzet;
            $http({
                method: 'PUT',
                url: '/api/MakaleAPI/' + $scope.makaleId + '/' + $scope.yazarId,
                contentType: "application/json",
                data: $scope.yeniMakale,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            })
              .success(function (data) {
                  //işlem başarılı
              });
        };
    });
angular.bootstrap(document.getElementById('ang_makaleDuzenle'), ['makaleDuzenle']);

// Makale Silme [Delete Makale]
angular.module('makaleSil', []).
    controller('makaleSilController', function ($scope, $http, $sce) {
        $scope.makale = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/MakaleAPI/' + $scope.makaleId }).success(function (data, status, headers, config) {
                $scope.makale = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.makale.MakaleGovde);
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        })
        $scope.submitForm = function () {
            $http({ method: 'DELETE', url: '/api/MakaleAPI/' + $scope.makaleId + '/' + $scope.yazarId }).success(function (data, status, headers, config) {
                //işlem başarılı
            }).error(function (data,status,headers,config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        }
    });
angular.bootstrap(document.getElementById('ang_makaleSil'), ['makaleSil']);

//Makale Görüntüle (Yazara göre) [Makale List]
angular.module('makaleYazar',[]).
    controller('makaleYazarController', function ($scope,$http) {
        angular.element(document).ready(function () {
            $http({
                method: 'GET',
                url: '/api/MakaleAPI/YazaraGore/' + $scope.yazarId,
                //params: {yazarId: $scope.yazarId}, [İkinci bir yol]
                contentType: "application/json"
            }).success(function (data, status, headers, config) {
                //işlem başarılı
                $scope.makaleler = data;
            }).error(function (data,status,headers,config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        })
    })
angular.bootstrap(document.getElementById('ang_makaleYazar'), ['makaleYazar']);

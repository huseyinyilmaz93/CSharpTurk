/// <reference path="angular.min.js" />
/// <reference path="angular-sanitize.js" />

//Tüm duyurular [List Duyuru]
angular.module('tumDuyurular', []).
    controller('tumDuyurularController', function ($scope, $http) {
        $http({ method: 'GET', url: '/api/DuyuruAPI' }).success(function (data, status, headers, config) {
            $scope.duyurular = data;
        }).error(function (data, status, headers, config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        });
    });
angular.bootstrap(document.getElementById('ang_duyurular'), ['tumDuyurular']);

//Duyuru Oluşturma [Create Duyuru]
angular.module('duyuruOlustur', ['textAngular']).
    controller('duyuruOlusturController', function ($scope, $http) {
        $scope.duyuru = {};
        $scope.submitForm = function () {
            $http({
                method: 'POST',
                url: '/api/DuyuruAPI/' + $scope.userId,
                contentType: "application/json",
                data: $scope.duyuru,
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
                      $scope.errorDuyuruBaslik = data.errors.DuyuruBaslik;
                      $scope.errorDuyuruGovde = data.errors.DuyuruGovde;
                      $scope.errorDuyuruOzet = data.errors.DuyuruOzet;
                  } else {
                      $scope.message = data.message;
                  }
              });
        };
    });
angular.bootstrap(document.getElementById('ang_duyuru'), ['duyuruOlustur']);

//DuyuruId'ye göre x duyurusuna getirme [Get Duyuru]
angular.module('duyuruGetir', ['ngSanitize']).
    controller('duyuruGetirController', function ($scope, $http, $sce, $locale) {
        $scope.duyuru = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/DuyuruAPI/' + $scope.duyuruId }).success(function (data, status, headers, config) {
                $scope.duyuru = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.duyuru.DuyuruGovde);
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        })
    });
angular.bootstrap(document.getElementById('ang_duyuruGetir'), ['duyuruGetir']);

//Duyuru Düzenleme [Edit Duyuru]
angular.module('duyuruDuzenle', ['textAngular']).
    controller('duyuruDuzenleController', function ($scope, $http, $location) {
        $scope.eskiDuyuru = {};
        $scope.yeniDuyuru = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/DuyuruAPI/' + $scope.duyuruId  }).success(function (data, status, headers, config) {
                $scope.eskiDuyuru = data;
                $scope.duyuruBaslik = data.DuyuruBaslik;
                $scope.duyuruGovde = data.DuyuruGovde;
                $scope.duyuruOzet = data.DuyuruOzet;
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        });

        $scope.submitForm = function () {
            $scope.yeniDuyuru.DuyuruBaslik = $scope.duyuruBaslik;
            $scope.yeniDuyuru.DuyuruGovde = $scope.duyuruGovde;
            $scope.yeniDuyuru.DuyuruOzet = $scope.duyuruOzet;

            $http({
                method: 'PUT',
                url: '/api/DuyuruAPI/' + $scope.duyuruId + '/' + $scope.userId,
                contentType: "application/json",
                data: $scope.yeniDuyuru,
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
angular.bootstrap(document.getElementById('ang_duyuruDuzenle'), ['duyuruDuzenle']);

// Duyuru Silme [Delete Duyuru]
angular.module('duyuruSil', []).
    controller('duyuruSilController', function ($scope, $http, $sce) {
        $scope.duyuru = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/DuyuruAPI/' + $scope.duyuruId }).success(function (data, status, headers, config) {
                $scope.duyuru = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.duyuru.DuyuruGovde);
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        })
        $scope.submitForm = function () {
            $http({ method: 'DELETE', url: '/api/DuyuruAPI/' + $scope.duyuruId + '/' + $scope.userId }).success(function (data, status, headers, config) {
                //işlem başarılı
            }).error(function (data, status, headers, config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        }
    });
angular.bootstrap(document.getElementById('ang_duyuruSil'), ['duyuruSil']);

//Duyuru Görüntüle (Yazara göre) [Duyuru List]
angular.module('duyuruYazar', []).
    controller('duyuruYazarController', function ($scope, $http) {
        angular.element(document).ready(function () {
            alert($scope.yazarId);
            $http({
                method: 'GET',
                url: '/api/DuyuruAPI/' + yazarId,
                contentType: "application/json"
            }).success(function (data, status, headers, config) {
                //işlem başarılı
                $scope.duyurular = data;
            }).error(function (data, status, headers, config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        })
    })
angular.bootstrap(document.getElementById('ang_duyuruYazar'), ['duyuruYazar']);

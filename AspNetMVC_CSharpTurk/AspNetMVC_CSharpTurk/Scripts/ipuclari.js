/// <reference path="angular.min.js" />
/// <reference path="angular-sanitize.js" />

//Tüm ipucular [List Ipucu]
angular.module('tumIpucular', []).
    controller('tumIpucularController', function ($scope, $http) {
        $http({ method: 'GET', url: '/api/IpucuAPI' }).success(function (data, status, headers, config) {
            $scope.ipucular = data;
        }).error(function (data, status, headers, config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        });
    });
angular.bootstrap(document.getElementById('ang_ipucular'), ['tumIpucular']);

//Ipucu Oluşturma [Create Ipucu]
angular.module('ipucuOlustur', ['textAngular']).
    controller('ipucuOlusturController', function ($scope, $http) {
        $scope.ipucu = {};
        $scope.submitForm = function () {
            $http({
                method: 'POST',
                url: '/api/IpucuAPI',
                contentType: "application/json",
                data: $scope.ipucu,
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
                      $scope.errorIpucuBaslik = data.errors.IpucuBaslik;
                      $scope.errorIpucuGovde = data.errors.IpucuGovde;
                  } else {
                      $scope.message = data.message;
                  }
              });
        };
    });
angular.bootstrap(document.getElementById('ang_ipucu'), ['ipucuOlustur']);

//IpucuId'ye göre x ipucusuna getirme [Get Ipucu]
angular.module('ipucuGetir', ['ngSanitize']).
    controller('ipucuGetirController', function ($scope, $http, $sce, $locale) {
        $scope.ipucu = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/IpucuAPI/' + $scope.ipucuId }).success(function (data, status, headers, config) {
                $scope.ipucu = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.ipucu.IpucuGovde);
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        })
    });
angular.bootstrap(document.getElementById('ang_ipucuGetir'), ['ipucuGetir']);

//Ipucu Düzenleme [Edit Ipucu]
angular.module('ipucuDuzenle', ['textAngular']).
    controller('ipucuDuzenleController', function ($scope, $http, $location) {
        $scope.eskiIpucu = {};
        $scope.yeniIpucu = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/IpucuAPI/' + $scope.ipucuId }).success(function (data, status, headers, config) {
                $scope.eskiIpucu = data;
                $scope.ipucuBaslik = data.IpucuBaslik;
                $scope.ipucuGovde = data.IpucuGovde;
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        });

        $scope.submitForm = function () {
            $scope.yeniIpucu.IpucuBaslik = $scope.ipucuBaslik;
            $scope.yeniIpucu.IpucuGovde = $scope.ipucuGovde;
            $http({
                method: 'PUT',
                url: '/api/IpucuAPI/' + $scope.ipucuId,
                contentType: "application/json",
                data: $scope.yeniIpucu,
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
angular.bootstrap(document.getElementById('ang_ipucuDuzenle'), ['ipucuDuzenle']);

// Ipucu Silme [Delete Ipucu]
angular.module('ipucuSil', []).
    controller('ipucuSilController', function ($scope, $http, $sce) {
        $scope.ipucu = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/IpucuAPI/' + $scope.ipucuId }).success(function (data, status, headers, config) {
                $scope.ipucu = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.ipucu.IpucuGovde);
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        })
        $scope.submitForm = function () {
            $http({ method: 'DELETE', url: '/api/IpucuAPI/' + $scope.ipucuId }).success(function (data, status, headers, config) {
                //işlem başarılı
            }).error(function (data, status, headers, config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        }
    });
angular.bootstrap(document.getElementById('ang_ipucuSil'), ['ipucuSil']);

//Ipucu Görüntüle (Yazara göre) [Ipucu List]
angular.module('ipucuYazar', []).
    controller('ipucuYazarController', function ($scope, $http) {
        angular.element(document).ready(function () {
            alert($scope.yazarId);
            $http({
                method: 'GET',
                url: '/api/IpucuAPI/' + yazarId,
                contentType: "application/json"
            }).success(function (data, status, headers, config) {
                //işlem başarılı
                $scope.ipucular = data;
            }).error(function (data, status, headers, config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        })
    })
angular.bootstrap(document.getElementById('ang_ipucuYazar'), ['ipucuYazar']);

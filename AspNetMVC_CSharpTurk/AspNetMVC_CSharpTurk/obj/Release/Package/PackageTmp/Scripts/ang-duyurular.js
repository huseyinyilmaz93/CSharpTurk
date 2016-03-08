/// <reference path="angular.min.js" />
/// <reference path="angular-sanitize.min.js" />

//Tüm duyurular [List Duyuru]
angular.module('tumDuyurular', []).
    controller('tumDuyurularController', function ($scope,$http) {
        $http({method:'GET', url: '/api/DuyuruAPI'}).success(function (data,status,headers,config) {
            $scope.makaleler = data;
        }).error(function (data,status,headers,config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin.')
        })
    });
angular.bootstrap(document.getElementById('ang_duyurular'), ['tumDuyurular']);

//Makale Oluşturma [CREATE Duyuru]
angular.module('duyuruOlustur', ['textAngular']).
    controller('duyuruOlusturController', function ($scope, $http) {
        $scope.makale = {};
        $scope.submitForm = function () {
            $http({
                method: 'POST',
                url: '/api/DuyuruAPI',
                contentType: 'application/json',
                data: $scope.makale,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            }).success(function (data) {
                if (data.errors) {
                    alert('Bilgileri alırken hata oluştu. Sayfayı yeniden yükleyin.')
                }
            });
        };
    });
angular.bootstrap(document.getElementById('ang_duyuru'), ['duyuruOlustur']);

//DuyuruId'ye göre x duyurusunu getirme [GET Duyuru]
angular.module('duyuruGetir', ['ngSanitize']).
    controller('duyuruGetirController', function ($scope, $http, $sce, $locale) {
        $scope.duyuru = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/DuyuruAPI/' + scope.duyuruId }).success(function (data, status, headers, config) {
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

//Duyuru Düzenleme [EDIT Duyuru]
angular.module('duyuruDuzenle', ['textAngular']).
    controller('duyuruDuzenleController', function ($scope, $http, $location) {
        $scope.eskiDuyuru = {};
        $scope.yeniDuyuru = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/DuyuruAPI/' + $scope.duyuruId }).success(function (data, status, headers, config) {
                $scope.eskiDuyuru = data;
                $scope.duyuruBaslik = data.DuyuruBaslik;
                $scope.duyuruResimURL = data.DuyuruResimURL;
                $scope.duyuruGovde = data.DuyuruGovde;
                $scope.duyuruOzet = data.DuyuruOzet;
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        });

        $scope.submitForm = function () {
            $scope.yeniDuyuru.MakaleBaslik = $scope.duyuruBaslik;
            $scope.yeniDuyuru.DuyuruGovde = $scope.duyuruGovde;
            $scope.yeniDuyuru.DuyuruOzet = $scope.duyuruOzet;
            $scope.yeniDuyuru.DuyuruResimURL = $scope.duyuruResimURL;

            $http({
                method: 'PUT',
                url: '/api/DuyuruAPI/' + $scope.duyuruId,
                contentType: 'applicatin/json',
                data: $scope.yeniDuyuru,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            }).success(function (data) {
                //işlem başarılı
            })
        }
    });
angular.bootstrap(document.getElementById('ang_duyuruDuzenle'), ['duyuruDuzenle']);

//Makale Silme [DELETE Duyuru]
angular.module('duyuruSil', []).
    controller('duyuruSilController', function ($scope, $http, $sce) {
        $scope.duyuru = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/DuyuruAPI' + $scope.duyuruId }).success(function ($scope,$http,$sce) {
                $scope.duyuru = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.duyuru.DuyuruGovde);
                }
            }).error(function (data,status,headers,config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        })
        $scope.submitForm = function () {
            $http({ method: 'DELETE', url: '/api/DuyuruAPI' + $scope.duyuruId }).success(function (data, status, headers, config) {
                //işlem başarılı
            }).error(function (data,status,headers,config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        }
    });
angular.bootstrap(document.getElementById('ang_duyuruSil'), ['duyuruSil']);
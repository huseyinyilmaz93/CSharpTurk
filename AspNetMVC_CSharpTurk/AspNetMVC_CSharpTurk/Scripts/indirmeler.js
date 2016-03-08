/// <reference path="angular.min.js" />
/// <reference path="angular-sanitize.js" />

//Tüm indirmeler [List Indirme]
angular.module('tumIndirmeler', []).
    controller('tumIndirmelerController', function ($scope, $http) {
        $http({ method: 'GET', url: '/api/IndirmeAPI' }).success(function (data, status, headers, config) {
            $scope.indirmeler = data;
        }).error(function (data, status, headers, config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        });
    });
angular.bootstrap(document.getElementById('ang_indirmeler'), ['tumIndirmeler']);

//Indirme Oluşturma [Create Indirme]
angular.module('indirmeOlustur', ['textAngular']).
    controller('indirmeOlusturController', function ($scope, $http) {
        $scope.indirme = {};
        $scope.submitForm = function () {
            $http({
                method: 'POST',
                url: '/api/IndirmeAPI',
                contentType: 'application/json',
                data: $scope.indirme,
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
                      $scope.errorIndirmeBaslik = data.errors.IndirmeBaslik;
                      $scope.errorIndirmeResimURL = data.errors.IndirmeResimURL;
                      $scope.errorIndirmeGovde = data.errors.IndirmeGovde;
                      $scope.errorIndirmeOzet = data.errors.IndirmeOzet;
                  } else {
                      $scope.message = data.message;
                  }
              });
        };
    });
angular.bootstrap(document.getElementById('ang_indirmeler'), ['indirmeOlustur']);

//IndirmeId'ye göre x indirmesini getirme [Get Indirme]
angular.module('indirmeGetir', ['ngSanitize']).
    controller('indirmeGetirController', function ($scope, $http, $sce, $locale) {
        $scope.indirme = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/IndirmeAPI/' + $scope.indirmeId }).success(function (data, status, headers, config) {
                $scope.indirme = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.indirme.IndirmeGovde);
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        })
    });
angular.bootstrap(document.getElementById('ang_indirmeGetir'), ['indirmeGetir']);

//Indirme Düzenleme [Edit Indirme]
angular.module('indirmeDuzenle', ['textAngular']).
    controller('indirmeDuzenleController', function ($scope, $http, $location) {
        $scope.eskiIndirme = {};
        $scope.yeniIndirme = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/IndirmeAPI/' + $scope.indirmeId }).success(function (data, status, headers, config) {
                $scope.eskiIndirme = data;
                $scope.indirmeBaslik = data.IndirmeBaslik;
                $scope.indirmeResimURL = data.IndirmeResimURL;
                $scope.indirmeGovde = data.IndirmeGovde;
                $scope.indirmeOzet = data.IndirmeOzet;
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        });

        $scope.submitForm = function () {
            $scope.yeniIndirme.IndirmeBaslik = $scope.indirmeBaslik;
            $scope.yeniIndirme.IndirmeGovde = $scope.indirmeGovde;
            $scope.yeniIndirme.IndirmeOzet = $scope.indirmeOzet;
            $scope.yeniIndirme.IndirmeResimURL = $scope.indirmeResimURL;

            $http({
                method: 'PUT',
                url: '/api/IndirmeAPI/' + $scope.indirmeId,
                contentType: "application/json",
                data: $scope.yeniIndirme,
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
angular.bootstrap(document.getElementById('ang_indirmeDuzenle'), ['indirmeDuzenle']);

// Indirme Silme [Delete Indirme]
angular.module('indirmeSil', []).
    controller('indirmeSilController', function ($scope, $http, $sce) {
        $scope.indirme = {};
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/IndirmeAPI/' + $scope.indirmeId }).success(function (data, status, headers, config) {
                $scope.indirme = data;
                $scope.rawHtmlGovde = function () {
                    return $sce.trustAsHtml($scope.indirme.IndirmeGovde);
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        })
        $scope.submitForm = function () {
            $http({ method: 'DELETE', url: '/api/IndirmeAPI/' + $scope.indirmeId }).success(function (data, status, headers, config) {
                //işlem başarılı
            }).error(function (data, status, headers, config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        }
    });
angular.bootstrap(document.getElementById('ang_indirmeSil'), ['indirmeSil']);

//Indirme Görüntüle (Yazara göre) [Indirme List]
angular.module('indirmeYazar', []).
    controller('indirmeYazarController', function ($scope, $http) {
        angular.element(document).ready(function () {
            alert($scope.yazarId);
            $http({
                method: 'GET',
                url: '/api/IndirmeAPI/' + $scope.yazarId,
                contentType: "application/json"
            }).success(function (data, status, headers, config) {
                //işlem başarılı
                $scope.indirmeler = data;
            }).error(function (data, status, headers, config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        })
    })
angular.bootstrap(document.getElementById('ang_indirmeYazar'), ['indirmeYazar']);

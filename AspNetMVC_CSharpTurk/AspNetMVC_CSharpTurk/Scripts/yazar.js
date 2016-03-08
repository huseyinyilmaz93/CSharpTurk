/// <reference path="angular.min.js" />

//Yazara ait tüm makaleleri getirme [LIST Makale]
angular.module('makaleYazar', []).
    controller('makaleYazarController', function ($scope, $http) {
        angular.element(document).ready(function () {
            $http({
                method: 'GET',
                url: '/api/MakaleAPI/YazaraGore/' + $scope.yazarId,
                contentType: 'application/json'
            }).success(function (data, status, headers, config) {
                //işlem başarılı
                $scope.makaleler = data;
            }).error(function (data, status, headers, config) {
                alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        })
    })
angular.bootstrap(document.getElementById('ang_makaleYazar'), ['makaleYazar']);

//MakaleId'ye göre x makalesini getirme [Get Makale]
angular.module('makaleGetir', ['ngSanitize']).
    controller('makaleGetirController', function ($scope, $http, $sce, $locale) {
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
    });
angular.bootstrap(document.getElementById('ang_makaleGetir'), ['makaleGetir']);
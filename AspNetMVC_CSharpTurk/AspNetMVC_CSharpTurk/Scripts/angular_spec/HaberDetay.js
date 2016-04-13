var module = angular.module('angular');
module.controller('haberGetirController', ['$scope', '$http', '$sce', '$locale', function ($scope, $http, $sce, $locale) {
    $scope.haber = {};
    angular.element(document).ready(function () {
        $http({ method: 'GET', url: '/api/HaberAPI/HaberDetay/' + $scope.haberId }).success(function (data, status, headers, config) {
            $scope.haber = data;
            $scope.rawHtmlGovde = function () {
                return $sce.trustAsHtml($scope.haber.HaberGovde);
            }
        }).error(function (data, status, headers, config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        });

    })

}]);
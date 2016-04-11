var module = angular.module('angular');
module.controller('duyuruGetirController', ['$scope', '$http', function ($scope, $http, $sce, $locale) {
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
}]);


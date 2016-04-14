var module = angular.module('angular');
module.controller('etkinlikGetirController', ['$scope', '$http', '$sce', function ($scope, $http, $sce) {
    $scope.etkinlik = {};
    $scope.rawHtmlIcerik = function () {
        return $sce.trustAsHtml($scope.etkinlik.EtkinlikIcerik);
    }
    angular.element(document).ready(function () {
        $http({ method: 'GET', url: '/api/EtkinlikAPI/EtkinlikDetay/' + $scope.etkinlikId }).success(function (data, status, headers, config) {
            $scope.etkinlik = data;
        }).error(function (data, status, headers, config) {

        })
    });
}]);
var module = angular.module('angular');
module.controller('StatikSayfaController', ['$scope', '$http', '$sce', function ($scope, $http, $sce) {
    angular.element(document).ready(function () {
        $http({ method: 'GET', url: '/api/StatikSayfaAPI/GetStatikSayfa/' + $scope.id })
            .success(function (data, status, header, config) {
                $scope.sayfa = data;
            }).error(function () {
                $scope.tumHatalar = data.ModelState;
            })
    })
    $scope.rawHtml = function (str) {
        return $sce.trustAsHtml(str);
    }
}]);

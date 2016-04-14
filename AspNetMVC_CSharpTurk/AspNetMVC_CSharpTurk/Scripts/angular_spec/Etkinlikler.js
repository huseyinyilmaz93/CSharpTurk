var module = angular.module('angular');
module.controller('tumEtkinliklerController', ['$scope', '$http', function ($scope, $http) {
    $scope.currentPage = 0;
    $scope.pageSize = 10;
    $scope.numberOfPages = function () {
        return Math.ceil($scope.etkinlikler.length / $scope.pageSize);
    }
    $http({ method: 'GET', url: '/api/EtkinlikAPI/TumEtkinlikler' }).success(function (data, status, headers, config) {
        $scope.etkinlikler = data;
    }).error(function (data, status, headers, config) {
        alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
    });
}]);

module.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        return input.slice(start);
    }
});

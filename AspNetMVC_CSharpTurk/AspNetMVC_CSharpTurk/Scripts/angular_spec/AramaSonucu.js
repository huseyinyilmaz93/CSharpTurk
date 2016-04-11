var module = angular.module('angular');
module.controller('AramaController', ['$scope', '$http', '$sce', function ($scope, $http, $sce) {
    $scope.currentPage = 0;
    $scope.pageSize = 10;
    $scope.numberOfPages = function () {
        return Math.ceil($scope.makaleler.length / $scope.pageSize);
    }

    $scope.Ara = function (aramaFiltresi) {
        $http({ method: 'GET', url: '/api/MakaleAPI/Ara/' + aramaFiltresi }).success(function (data, status, headers, config) {
            $scope.makaleler = data;
            $scope.rawHtml = function (str) {
                return $sce.trustAsHtml(str);
            }
            if (data.length == 0) {
                alert('Arama sonucu bulunamadı! Ana sayfaya yönlendiriliyorsunuz ...');
                window.location = "/Home/Index";
            }
        }).error(function (data, status, headers, config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        });
    }
}]);

module.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        return input.slice(start);
    }
});

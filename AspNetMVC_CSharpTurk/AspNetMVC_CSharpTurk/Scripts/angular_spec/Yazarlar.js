var module = angular.module('angular');
module.controller('YazarlarController', ['$scope', '$http', function ($scope, $http) {
    $scope.currentPage = 0;
    $scope.pageSize = 10;
    $scope.numberOfPages = function () {
        return Math.ceil($scope.kullanicilar.length / $scope.pageSize);
    }
    $scope.kullanicilar = {};
    $scope.gorunmez = [];
    $http({ method: 'GET', url: '/api/HesapAPI' }).success(function (data, status, headers, config) {
        $scope.kullanicilar = data;
        $scope.topYorum = data.length;
        for (var i = 0; i < $scope.topYorum; i++) {
            $scope.gorunmez[i] = false;
        }
    });
    $scope.KullaniciSil = function (yazarId, index) {
        $http({ method: 'HEAD', url: '/api/HesapAPI/YazarSil/' + yazarId }).success(function (data, status, headers, config) {
            //başarılı
            $scope.gorunmez[index] = true;
        }).error(function (data, status, headers, config) {
            alert('İşlem gerçekleşmedi tekrar deneyin!');
        })
    }

}]);
module.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        return input.slice(start);
    }
});
module.directive('ngConfirmClick', [
function () {
    return {
        link: function (scope, element, attr) {
            var msg = attr.ngConfirmClick || "Are you sure?";
            var clickAction = attr.confirmedClick;
            element.bind('click', function (event) {
                if (window.confirm(msg)) {
                    scope.$eval(clickAction)
                }
            });
        }
    };
}]);

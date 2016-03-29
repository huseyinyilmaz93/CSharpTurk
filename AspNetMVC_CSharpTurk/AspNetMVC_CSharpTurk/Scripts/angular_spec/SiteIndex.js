var module = angular.module('angular');
module.controller('SonEklenenMakalelerController', ['$scope', '$http', function ($scope, $http) {
    $scope.currentPage = 0;
    $scope.pageSize = 6;
    $scope.sonEklenenMakaleler = {};
    $scope.numberOfPages = function () {
        return Math.ceil($scope.sonEklenenMakaleler.length / $scope.pageSize);
    }
    $http({ method: 'GET', url: '/api/MakaleAPI/Guncel30' }).success(function (data, status, headers, config) {
        $scope.sonEklenenMakaleler = data;
    }).error(function (data, status, header, config) {

    });
}]);

module.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        return input.slice(start);
    }
});

module.controller('HaberController', ['$scope', '$http', function ($scope, $http) {
    $scope.guncelHaber = {};
    $scope.guncelHaberler = {};
    $http({ method: 'GET', url: '/api/HaberAPI/GuncelHaber' }).success(function (data, status, headers, config) {
        $scope.guncelHaber = data;
        $scope.GuncelResim = {
            'background-image': 'url(' + data.ResimURL + ')'
        };
    }).error(function (data, status, headers, config) {

    });
    $http({ method: 'GET', url: '/api/HaberAPI/GuncelHaberler' }).success(function (data, status, headers, config) {
        $scope.guncelHaberler = data;
    }).error(function (data, status, headers, config) {

    });
}]);

module.controller('EtkinlikController', ['$scope', '$http', function ($scope, $http) {
    $scope.guncelEtkinlik = {};
    $scope.guncelEtkinlikler = {};
    $http({ method: 'GET', url: '/api/EtkinlikAPI/GuncelEtkinlik' }).success(function (data, status, headers, config) {
        $scope.guncelEtkinlik = data;
        $scope.guncelEtkinlikResim = {
            'background-image': 'url(' + data.ResimURL + ')'
        };
    }).error(function (data, status, headers, config) {

    });
    $http({ method: 'GET', url: '/api/EtkinlikAPI/GuncelEtkinlikler' }).success(function (data, status, headers, config) {
        $scope.guncelEtkinlikler = data;
    }).error(function (data, status, headers, config) {

    });
}]);

module.controller('IsIlaniController', ['$scope', '$http', function ($scope, $http) {
    $scope.guncelIlan = {};
    $scope.guncelIlanlar = {};
    $http({ method: 'GET', url: '/api/IsIlaniAPI/GuncelIlan' }).success(function (data, status, headers, config) {
        $scope.guncelIlan = data;
        $scope.GuncelIlanResim = {
            'background-image': 'url(' + data.ResimURL + ')'
        };
    }).error(function (data, status, headers, config) {

    });
    $http({ method: 'GET', url: '/api/IsIlaniAPI/GuncelIlanlar' }).success(function (data, status, headers, config) {
        $scope.guncelIlanlar = data;
    }).error(function (data, status, headers, config) {

    });

}]);

module.filter('htmlToPlaintext', function () {
    return function (text) {
        return angular.element(text).text();
    };
}
);

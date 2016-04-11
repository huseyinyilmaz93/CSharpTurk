var module = angular.module('angular', ['textAngular', 'ngSanitize'])
module.controller('DuyuruSayisi', function ($scope, $http) {
    $scope.duyuruSayisi = 0;
    $http({ method: 'GET', url: '/api/DuyuruAPI/Onaylanacak' }).success(function (data, status, headers, config) {
        $scope.duyuruSayisi = data;
    })
});
module.controller('YorumSayisi', ['$scope', '$http', function ($scope, $http) {
    $scope.yorumSayisi = 0;
    $http({ method: 'GET', url: '/api/YorumAPI/Onaylanacak' }).success(function (data, status, headers, config) {
        $scope.yorumSayisi = data;
    })
}]);
module.controller('HataBildirimSayisi', ['$scope', '$http', function ($scope, $http) {
    $scope.hataSayisi = 0;
    $http({ method: 'GET', url: '/api/HataBildirAPI/Onaylanacak' }).success(function (data, status, headers, config) {
        $scope.hataSayisi = data;
    })
}]);

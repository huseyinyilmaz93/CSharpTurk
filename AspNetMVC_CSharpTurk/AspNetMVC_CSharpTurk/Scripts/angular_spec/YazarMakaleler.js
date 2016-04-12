var module = angular.module('angular');
module.controller('makaleYazarController', function ($scope, $http) {
    $scope.currentPage = 0;
    $scope.pageSize = 10;
    $scope.AdSoyad = {};
    $scope.numberOfPages = function () {
        return Math.ceil($scope.makaleler.length / $scope.pageSize);
    }
    angular.element(document).ready(function () {
        $http({
            method: 'GET',
            url: '/api/MakaleAPI/YazaraGore/' + $scope.yazarId,
            contentType: 'application/json'
        }).success(function (data, status, headers, config) {
            //işlem başarılı
            $scope.AdSoyad = data[0].Yazar.Ad + " " + data[0].Yazar.Soyad;
            $scope.makaleler = data;
        }).error(function (data, status, headers, config) {
            alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        })
    })
});

module.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        return input.slice(start);
    }
});

//module.controller('KullaniciController', ['$scope', '$http', function ($scope, $http) {
//    $scope.gecerliKullanici = {};
//    $scope.tumHatalar = {}
//    $scope.KullaniciGetir = function (kullaniciId) { 
//        $http({ method: 'GET', url: '/api/HesapAPI/' + kullaniciId }).success(function (data, status, headers, config) {
//            if (status == 200) {
//                $scope.gecerliKullanici = data;
//            }
//        }).error(function (data, status, headers, config) {
//            if (status == 405) {
//                $scope.tumHatalar = data.ModelState;
//            }
//        })
//    }
//}]);


var module = angular.module('angular');
module.controller('duyuruSilController', ['$scope', '$http', function ($scope, $http, $sce) {
    $scope.duyuru = {};
    angular.element(document).ready(function () {
        $http({ method: 'GET', url: '/api/DuyuruAPI/' + $scope.duyuruId }).success(function (data, status, headers, config) {
            $scope.duyuru = data;
            $scope.rawHtmlGovde = function () {
                return $sce.trustAsHtml($scope.duyuru.DuyuruGovde);
            }
            alert('Duyuru Silindi ...');
            window.location = "/Duyuru/Duyurular";
        }).error(function (data, status, headers, config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfa yeniden yükleniyor!');
            window.location.reload();
        });
    })
    $scope.submitForm = function () {
        $http({ method: 'DELETE', url: '/api/DuyuruAPI/' + $scope.duyuruId + '/' + $scope.userId }).success(function (data, status, headers, config) {
            //işlem başarılı
        }).error(function (data, status, headers, config) {
            alert('İşlem sırasında bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        })
    }
}]);

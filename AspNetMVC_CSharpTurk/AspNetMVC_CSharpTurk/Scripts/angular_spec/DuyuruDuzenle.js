var module = angular.module('angular');
module.controller('duyuruDuzenleController', ['$scope', '$http', function ($scope, $http, $location) {
    $scope.eskiDuyuru = {};
    $scope.yeniDuyuru = {};
    $scope.tumHatalar = {};
    angular.element(document).ready(function () {
        $http({ method: 'GET', url: '/api/DuyuruAPI/' + $scope.duyuruId }).success(function (data, status, headers, config) {
            $scope.eskiDuyuru = data;
            $scope.duyuruBaslik = data.DuyuruBaslik;
            $scope.duyuruGovde = data.DuyuruGovde;
            $scope.duyuruOzet = data.DuyuruOzet;
            alert('Duyuru düzenlendi ...');
            window.location = '/Duyuru/Duyurular';
        }).error(function (data, status, headers, config) {
            alert('Bilgileri alırken bir hata oluştu. ');
        });
    });

    $scope.submitForm = function () {
        $scope.yeniDuyuru.DuyuruBaslik = $scope.duyuruBaslik;
        $scope.yeniDuyuru.DuyuruGovde = $scope.duyuruGovde;
        $scope.yeniDuyuru.DuyuruOzet = $scope.duyuruOzet;
        $http({
            method: 'PUT',
            url: '/api/DuyuruAPI/' + $scope.duyuruId + '/' + $scope.userId,
            contentType: "application/json",
            data: $scope.yeniDuyuru,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            }
        })
          .success(function (data, status, headers, config) {
              if (status == 200) {
                  alert('Duyuru başarıyla düzenlendi!');
                  window.location = "/Duyuru/Duyurular";
              } else {
                  alert('Bir hata meydana geldi lütfen tekrar deneyin .');
              }
          }).error(function (data, status, headers, config) {
              if (status == 400) {
                  $scope.tumHatalar = data.ModelState;
              } else if (status == 502) {
                  alert('Sunucuda bir hata meydana geldi. Lütfen tekrar deneyin!');
              } else {
                  alert('Önemli bir hata oluştu anasayfaya yönlendiriliyorsunuz!');
                  window.location = "/Home/Index";
              }
          })
    };
}]);

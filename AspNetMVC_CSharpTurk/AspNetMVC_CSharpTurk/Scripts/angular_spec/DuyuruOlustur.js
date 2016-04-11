var module = angular.module('angular');
module.controller('duyuruOlusturController', ['$scope', '$http', function ($scope, $http) {
    $scope.duyuru = {};
    $scope.tumHatalar = {};
    $scope.submitForm = function () {
        $http({
            method: 'POST',
            url: '/api/DuyuruAPI/' + $scope.userId,
            contentType: "application/json",
            data: $scope.duyuru,
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
                  alert('Duyuru kaydedildi ...');
                  window.location = "/Duyuru/Duyurular";

              } else {
                  $scope.tumHatalar = data.ModelState;
                  //$scope.message = data.message;
                  alert('İşlem sırasında bir hata oluştu!');
              }
          }).error(function (data, status, headers, config) {
              if (status == 400) {
                  alert('Form elemanlarını kontrol edin.');
                  $scope.tumHatalar = data.ModelState;
              } else if (status == 502) {
                  alert('Verileri kaydederken bir hata oluştu! Tekrar deneyin.');
                  window.location = "/Duyuru/Duyurular"
              } else {
                  alert('hata ?')
              }
              //window.location.reload();
          });
    };
}]);

var module = angular.module('angular');
module.controller('SifremiUnuttumController', ['$scope', '$http', function ($scope, $http) {
    $scope.hataGorunum = true;
    $scope.MailKontrol = function () {
        $http({
            method: 'POST',
            url: '/api/HesapAPI/MailKontrol',
            contentType: 'application/json',
            data: $scope.mail,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            }
        })
        .success(function (data, status, headers, config) {
            $scope.hataGorunum = true;
            alert('Şifre değişim isteğiniz alınmıştır. E postanızı kontrol edin!');
            window.location = "/Anasayfa";
        }).error(function (data, status, headers, config) {
            $scope.hataGorunum = false;
            $scope.mesaj = data.Message;
        })
    }
}])

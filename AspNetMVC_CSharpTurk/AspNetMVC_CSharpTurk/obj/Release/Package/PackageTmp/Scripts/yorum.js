/// <reference path="angular.min.js" />
/// <reference path="angular-sanitize.js" />


angular.module('yorum', []).
    controller('yorumController', function ($scope, $http) {
        $scope.yeniYorum = {};
        $scope.yorumlar = {};
        $scope.yorumSayisi = 0;
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/YorumAPI/' + $scope.makaleId }).success(function (data, status, headers, config) {
                $scope.yorumlar = data;
                $scope.yorumSayisi = data.length;
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            });
        });
        $scope.submitForm = function () {
            $http({
                method: 'POST',
                url: '/api/YorumAPI/' + $scope.makaleId,
                contentType: 'application/json',
                data: $scope.yeniYorum,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            })
                .success(function (data, status, headers, config) {
                //işlem başarılı
                alert('Yorum kaydedildi!');
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        }
    });
angular.bootstrap(document.getElementById('ang_yorumlar'), ['yorum']);


/// <reference path="angular.min.js" />
/// <reference path="angular-sanitize.min.js" />

//Kontrol edilmemiş duyuruları getirir.
angular.module('getSayi',[]).controller('getSayiController', function ($scope, $http) {
        $scope.yorumSayisi = 0;
        $scope.duyuruSayisi = 0;
        $http({ method: 'GET', url: '/api/DuyuruAPI/Onaylanacak' }).success(function (data, status, headers, config) {
            $scope.yorumSayisi = data;
        });
        $http({ method: 'GET', url: '/api/YorumAPI/Onaylanacak' }).success(function (data, status, headers, config) {
            $scope.yorumSayisi = data;
        });

    })
angular.bootstrap(document.getElementById('ang_bilgi'), ['getSayi']);

angular.module('duyuruKontrol',[]).
    controller('duyuruKontrolController', function ($scope, $http) {
        $scope.gorunmez = []
        $http({ method: 'GET', url: '/api/DuyuruAPI/Kontrol' }).success(function (data, status, headers, config) {
            $scope.duyurular = data;
            $scope.topDuyuru = data.length;
            for (var i = 0; i < $scope.topDuyuru; i++) {
                $scope.gorunmez[i] = false;
            }
        }).error(function (data, status, headers, config) {
            alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
        })
        $scope.Onayla = function (duyuruId,index) {
            $http({ method: 'HEAD', url: '/api/DuyuruAPI/Onay/' + duyuruId }).success(function (data, status, headers, config) {
                //başarılı
                $scope.gorunmez[index] = true;
            }).error(function (data, status, headers, config) {
                alert('İşlem gerçekleşmedi tekrar deneyin!');
            })
        }
        $scope.Reddet = function (duyuruId,index) {
            $http({ method: 'HEAD', url: '/api/DuyuruAPI/Red/' + duyuruId }).success(function (data, status, headers, config) {
                //başarılı
                $scope.gorunmez[index] = true;
            }).error(function (data, status, headers, config) {
                alert('İşlem gerçekleşmedi tekrar deneyin!');
            })
        }
    });
angular.bootstrap(document.getElementById('ang_duyuruKontrol'), ['duyuruKontrol']);

//Kontrol edilmemiş yorumları getirir.
angular.module('yorumKontrol', []).
    controller('yorumKontrolController', function ($scope, $http) {
        $scope.gorunmez = []
        angular.element(document).ready(function () {
            $http({ method: 'GET', url: '/api/YorumAPI/Kontrol' }).success(function (data, status, headers, config) {
                $scope.yorumlar = data;
                $scope.topYorum = data.length;
                for (var i = 0; i < $scope.topYorum; i++) {
                    $scope.gorunmez[i] = false;
                }
            }).error(function (data, status, headers, config) {
                alert('Bilgileri alırken bir hata oluştu. Sayfayı yenileyerek tekrar deneyin!');
            })
        })
        $scope.Onayla = function (yorumId, index) {
            $http({ method: 'HEAD', url: '/api/YorumAPI/Onay/' + yorumId }).success(function (data, status, headers, config) {
                //başarılı
                $scope.gorunmez[index] = true;
            }).error(function (data, status, headers, config) {
                alert('İşlem gerçekleşmedi tekrar deneyin!');
            })
        }
        $scope.Reddet = function (yorumId, index) {
            $http({ method: 'HEAD', url: '/api/YorumAPI/Red/' + yorumId }).success(function (data, status, headers, config) {
                //başarılı
                $scope.gorunmez[index] = true;
            }).error(function (data, status, headers, config) {
                alert('İşlem gerçekleşmedi tekrar deneyin!');
            })
        }
    });
angular.bootstrap(document.getElementById('ang_yorumKontrol'),['yorumKontrol'])
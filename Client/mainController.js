(function () {
    'use strict';
    angular
        .module('app')
        .controller('MainController', mainController);
    function mainController($scope, $http) {
        $http.get('http://localhost:61754/WCFService.svc/GetAllManufacturers').then(function (response) {
            $scope.products = response.data;
        });
    }
})();
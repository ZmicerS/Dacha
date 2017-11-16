(function () {
    var app = angular.module("appModuleDacha");   
    'use strict';
    app.controller('loginController', ['loginService', '$scope', '$http', '$state', '$stateParams', '$timeout', function (loginService, $scope, $http, $state, $stateParams, $timeout) {
        var vm = this;   
        vm.loginData = {};
        vm.loginData.userName = '';
        vm.loginData.password = '';   

        vm.registerData = {};
        vm.registerData.email = '';
        vm.registerData.password = '';

        vm.dataLoading = false;
        vm.message = '';
        vm.savedSuccessfully = false;      
        //////
        // 
        vm.login = function () {
            vm.dataLoading = true;
            vm.message = '';
                    
            loginService.login(vm.loginData).then(function (response) {               
                vm.dataLoading = false;                
                vm.savedSuccessfully = true;
                vm.message = 'Check in completed successfully';
                $timeout($state.go('companionshiplist'), 10000);
            },
                function (error) {                
                    vm.dataLoading = false;                    
                    vm.savedSuccessfully = false;
                    vm.message = error.data.error;
                });
        };

    }]);
})();


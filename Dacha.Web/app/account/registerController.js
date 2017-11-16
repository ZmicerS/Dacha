(function () {
    'use strict';

    angular.module('appModuleDacha')
        .controller('registerController', registerController);

    registerController.$inject = ['registerService', '$scope', '$http', '$state', '$stateParams', '$timeout'];
    function registerController(registerService, $scope, $http, $state, $stateParams, $timeout) {
        var vm = this;
        vm.registerData = {};
        vm.registerData.email = '';
        vm.registerData.password = '';
        vm.registerData.confirmPassword = ''
        vm.register = register;
        vm.dataLoading = false;
        vm.registerSuccess = false;
        vm.registerError = false;
        vm.registerMessage = '';

        function register() {
            vm.registerError = false;
            vm.registerMessage = '';
            vm.registerSuccess = false;
            vm.dataLoading = true;
            registerService.register(vm.registerData).then(function (response) {
                vm.dataLoading = false;               
                vm.registerSuccess = true;
                vm.registerMessage = response.data;
                $timeout($state.go('login'), 5000);
            },
                function (error) {
                    vm.dataLoading = false;                    
                    vm.registerError = true;
                    vm.registerMessage = error.data.Message;
                });
        }//
    }

})();
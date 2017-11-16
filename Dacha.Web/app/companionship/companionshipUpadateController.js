(function () {
    'use strict';

    angular.module('appModuleDacha')
        .controller('companionshipUpdateController', companionshipUpdateController);

    companionshipUpdateController.$inject = ['companionshipService', '$scope', '$http', '$state', '$stateParams'];
    function companionshipUpdateController(companionshipService, $scope, $http, $state, $stateParams) {     
        var vm = this;
        vm.dataLoading = false;
        vm.updateData = {
            id: $stateParams.id,
            name: $stateParams.name,
            address: $stateParams.address,
            registration: $stateParams.registration,
            chairman: $stateParams.chairman,
            membership: $stateParams.membership,
            addition: $stateParams.addition
        }
        //
        //
        vm.cancel = function () {          
            $state.go('companionshiplist');
        }
    //
        vm.update = function () {
            vm.dataLoading = true;
            var datasend = {
                Id: vm.updateData.id,
                Name: vm.updateData.name,
                Address: vm.updateData.address,
                Registration: vm.updateData.registration,
                Chairman: vm.updateData.chairman1,
                Membership: vm.updateData.membership,
                Addition: vm.updateData.addition
            };
            companionshipService.updateCompanionship(vm.updateData)
                .then(function (response) {
                vm.dataLoading = false;
                $state.go('companionshiplist');
                },
                function (error) { vm.dataLoading = false });
        };
        // 
    }//
})();
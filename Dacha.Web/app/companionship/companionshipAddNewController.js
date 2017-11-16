(function () {
    'use strict';

    angular.module('appModuleDacha')
        .controller('companionshipAddNewController', companionshipAddNewController);

    companionshipAddNewController.$inject = ['companionshipService', '$scope', '$http', '$state', '$stateParams'];
    function companionshipAddNewController(companionshipService, $scope, $http, $state, $stateParams) {     
        var vm = this;
        vm.dataLoading = false;
        vm.addData={
        id : "",
        name: "",
        address : "",
        registration : "",
        chairman : "",
        membership : "",
        addition : ""
    }
    //
        vm.addition = function () {          
            vm.dataLoading = true;
            var datasend = {
                Id: "",
                Name: vm.addData.name,
                Address: vm.addData.address,
                Registration: vm.addData.registration,
                Chairman : vm.addData.chairman1,
                Membership: vm.addData.membership,
                Addition: vm.addData.addition
            };
            companionshipService.addCompanionship(vm.addData)
                .then(function (response) {
                    vm.dataLoading = false;
                    $state.go('companionshiplist', {}, { reload: true });
                }, function (error) {
                    vm.dataLoading = false;});
        };

        vm.cancel = function () {
            $state.go('companionshiplist', {}, { reload: false });
        };

    }//
})();
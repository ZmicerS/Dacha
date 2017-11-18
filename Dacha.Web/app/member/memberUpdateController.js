//memberUpdateController
(function () {
    'use strict';
    angular.module('appModuleDacha').controller('memberUpdateController', memberUpdateController);

    memberUpdateController.$inject = ['memberService', '$scope', '$http', '$state', '$stateParams'];
    function memberUpdateController(memberService, $scope, $http, $state, $stateParams) {
        var vm = this;      
        var companionshipId = $stateParams.companionshipId;
        vm.companionshipName = $stateParams.companionshipName;
       
        vm.updateData = {
            companionshipId : $stateParams.companionshipId,
            id : $stateParams.id,
            owner : $stateParams.owner,
            plotNumber : $stateParams.plotNumber,
            plotAddress : $stateParams.plotAddress,
            plotSquare : $stateParams.plotSquare,
            ownerAddress : $stateParams.ownerAddress,
            addition : $stateParams.addition
        };
        
        vm.cancel = function () {                     
            $state.go('memberlist', {
                'companionshipId': companionshipId,
                'companionshipName': vm.companionshipName
            });
        }

        vm.dataLoading = false;      
        
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
            memberService.updateMember(vm.updateData)
                .then(function (response) {
                    vm.dataLoading = false;
                    $state.go('memberlist', {
                        'companionshipId': companionshipId,
                        'companionshipName': vm.companionshipName
                    });
                },
                function (error) { vm.dataLoading = false });
        };
    }
})();
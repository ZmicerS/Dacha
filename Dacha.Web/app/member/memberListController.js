(function () {
    'use strict';

    angular.module('appModuleDacha').controller('memberListController', memberListController);

    memberListController.$inject = ['memberService', '$scope', '$http', '$state', '$stateParams', '$uibModal'];
    function memberListController(memberService, $scope, $http, $state, $stateParams, $uibModal) {
        var vm = this;      
        var companionshipId = $stateParams.companionshipId;
        vm.companionshipName = $stateParams.companionshipName;
        if (companionshipId === undefined || companionshipId.length==0 )
        {
            companionshipId = sessionStorage.getItem('companionshipId');
        }

        if (vm.companionshipName === undefined || vm.companionshipName.length == 0) {
            vm.companionshipName = sessionStorage.getItem('companionshipName');
        }

        var deleteData = null;

        function getListMembersCompanionship() {
            //
            memberService.getMembersCompanionship(companionshipId).then(function (response) {
                vm.members = response.data;
                vm.dataLoading = false;
            },
                function (error) {
                    vm.dataLoading = false;
                });
        }
        
        getListMembersCompanionship();
        
        vm.goto = function (index, member) {         
            sessionStorage.setItem('memberId', member.Id);
            sessionStorage.setItem('memberOwner', member.Owner);
            $state.go('memberdoclist', {
                'memberId': member.Id,
                'memberOwner': member.Owner});
        }
        
        vm.addNew = function () {
            $state.go('memberaddnew', {
                'companionshipId': companionshipId,
                'companionshipName': vm.companionshipName
            });
        }

        //
        vm.update = function (index, member) {                      
            $state.go('memberupdate',
                {
                    'companionshipId': companionshipId, 'id': member.Id, 'owner': member.Owner, 'plotNumber': member.PlotNumber,
                    'plotAddress': member.PlotAddress, 'plotSquare': member.PlotSquare, 'ownerAddress': member.OwnerAddress, 'addition': member.Addition
                });
        }

        vm.delete = function (index, data) {          
            deleteData = data;                     
            askDelete();
        }

        function askDelete() {            
            openModal();
        };

        var openModal = function () {
            $scope.uibModalInstance = $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/app/member/memberdeletemodal.html',
                controller: 'memberDeleteHandlerController',
                controllerAs: '$ctrl',
                size: 'lg',
                resolve: {

                }
            }).result.then(function () {
                deleteMember();
            }, function () {               
            });
        }

        function deleteMember() {
            memberService.deleteMember(deleteData).then(function (response) {              
                getListMembersCompanionship();
                $state.reload();
            },
                function (error) {

                });
        }      
    }
    
    angular.module('appModuleDacha').controller("memberDeleteHandlerController", function ($scope, $uibModalInstance) {

        $scope.cancelModal = function () {
            $uibModalInstance.dismiss('close');
        }
        $scope.ok = function () {
            $uibModalInstance.close('save');
        }
    });
})();
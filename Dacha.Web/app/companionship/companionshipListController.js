(function () {
    'use strict';

    angular.module('appModuleDacha')
        .controller('companionshipListController', companionshipListController);

    companionshipListController.$inject = ['refreshTokenService','companionshipService', '$scope', '$http', '$state', '$stateParams', '$uibModal'];
    function companionshipListController(refreshTokenService,companionshipService, $scope, $http, $state, $stateParams, $uibModal) {
        var vm = this;
        vm.companionShips = {};
        vm.dataLoading = true;
        var deleteData = null;      
        //
        getListCompanionships();

        function getListCompanionships() { 
         //   refreshTokenService.refreshToken().then(function (response) { }, function (error) { });
        companionshipService.getCompanionships().then(function (response) {
            vm.companionShips = response.data;
            vm.dataLoading = false;
            },
            function (error) {
                vm.dataLoading = false;
            });
        }

        //
        vm.goto = function (index, data) {         
            sessionStorage.setItem('companionshipId', data.Id);
            sessionStorage.setItem('companionshipName', data.Name);
            $state.go('memberlist', {
                'companionshipId': data.Id,
                'companionshipName': data.Name });
        }

        vm.addNew = function () {         
            $state.go('companionshiplistaddnew');
        }
       
        vm.update = function (index, data) {       
            //TODO: logic to render data on popups and update and set;
               $state.go('companionshiplistupdate', {
                   'id': data.Id,
                   'name': data.Name,
                   'address': data.Address,
                   'registration': data.Registration,
                   'chairman': data.Chairman,
                   'membership': data.Membership,
                   'addition': data.Addition
               });
        }
       
        vm.delete = function (index, data) {          
             deleteData = data;           
            askDelete();
        }

        function  deleteCompanionship(){
            companionshipService.deleteCompanionship(deleteData).then(function (response) {               
            },
                function (error) {
                });
            getListCompanionships();
            $state.reload();
        }

        function askDelete()
        {
            openModal();
        };

        var openModal = function () {
            $scope.uibModalInstance = $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/app/companionship/companionshipdeletemodal.html',
                controller: 'ModelHandlerController',
                controllerAs: '$ctrl',
                size: 'lg',
                resolve: {

                }
            }).result.then(function () {
                deleteCompanionship();
            }, function () {
              
            });
        }     
    }
    
    
    angular.module('appModuleDacha').controller("ModelHandlerController", function ($scope, $uibModalInstance) {

        $scope.cancelModal = function () {          
            $uibModalInstance.dismiss('close');          
        }
        $scope.ok = function () {
            $uibModalInstance.close('save');
        }
    });
})();

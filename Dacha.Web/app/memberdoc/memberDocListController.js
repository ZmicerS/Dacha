(function () {
    'use strict';

    angular.module('appModuleDacha').controller('memberDocListController', memberDocListController);

    memberDocListController.$inject = ['memberDocService', '$scope', '$http', '$state', '$stateParams', '$uibModal'];
    function memberDocListController(memberDocService, $scope, $http, $state, $stateParams, $uibModal) {
        var vm = this;       
        var memberId = $stateParams.memberId;
        $scope.memberOwner = $stateParams.memberOwner;
        if (memberId === undefined || memberId.length == 0) {
            memberId = sessionStorage.getItem('memberId');
        }

        if ($scope.memberOwner === undefined || $scope.memberOwner.length == 0) {
            $scope.memberOwner = sessionStorage.getItem('memberOwner');
        }
        $scope.listImgId = {};
        //
        var deleteData = null;
        //
        getListIdDocs();
        function getListIdDocs()
        { 
            memberDocService.getListIdDocs(memberId)
                 .then(function (response) {               
                    $scope.listImgId = response.data;
                },
                function (error) {          
                });
            
        }        

         $scope.delete = function (item) {            
            deleteData = item;                 
            askDelete();
        }

        function askDelete() {           
            openModal();
        };

        var openModal = function () {
            $scope.uibModalInstance = $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/app/memberdoc/memberdocdeletemodal.html',
                controller: 'memberDocDeleteHandlerController',
                controllerAs: '$ctrl',
                size: 'lg',
                resolve: {

                }
            }).result.then(function () {
                deleteMemberDoc();
            }, function () {                
            });
            //;
        }//end

        function deleteMemberDoc() {
            memberDocService.deleteMember(deleteData)
                .then(function (response) {                    
                    getListIdDocs();
                }, function (response) {                    
                });
        }
    //

      //
        $scope.doUpload = function () {
            var file = $scope.docFile;
            if (file !== undefined) {
                var description = $scope.description;
                var fileFormData = new FormData();
                fileFormData.append('file', file);
                fileFormData.append('description', description);
                fileFormData.append('memberid', memberId);
                memberDocService.upload(fileFormData)
                    .then(function (response) {
                        // This function handles success                     
                        $scope.docFile = "";
                        getListIdDocs();
                    }, function (response) {                       
                    });
            }

        }
    //

    }    
    //
    angular.module('appModuleDacha').controller("memberDocDeleteHandlerController", function ($scope, $uibModalInstance) {

        $scope.cancelModal = function () {
            
            $uibModalInstance.dismiss('close');
        }
        $scope.ok = function () {
            $uibModalInstance.close('save');
        }
    });
    //
})();
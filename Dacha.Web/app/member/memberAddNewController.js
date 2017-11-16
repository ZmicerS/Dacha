(function () {
    'use strict';

    angular.module('appModuleDacha').controller('memberAddNewController', memberAddNewController);

    memberAddNewController.$inject = ['memberService', '$scope', '$http', '$state', '$stateParams'];
    function memberAddNewController(memberService, $scope, $http, $state, $stateParams) {
        var vm = this;     
        var companionshipId = $stateParams.companionshipId;
        vm.companionshipName = $stateParams.companionshipName;
        //
      
        vm.dataLoading = false;
        vm.addData = {
            id: "",
            owner: "",
            ownerAddress: "",
            plotNumber: "",
            plotAddress: "",
            plotSquare: "",
            addition: "",
            companionshipId: companionshipId
        };
        //       
        //
        vm.addition = function () {
              vm.dataLoading = true;
              memberService.addMember(vm.addData)
                  .then(function (response) {
                      vm.dataLoading = false;                     
                      $state.go('memberlist', {
                          'companionshipId': companionshipId,
                          'companionshipName': vm.companionshipName
                      });
                  }, function (error) {
                      vm.dataLoading = false;
                  });
        };

        vm.cancel = function () {             
            $state.go('memberlist', {
                'companionshipId': companionshipId,
                'companionshipName': vm.companionshipName
            });
        }
    }
    //
})();
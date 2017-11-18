(function () {
    'use strict';
    angular.module('appModuleDacha')
        .controller('headController', headController);  
    headController.$inject = ['$scope', '$state'];
    function headController($scope, $state) {
        var vm = this;
        $state.go('login');
    }
})();
(function () {
    var app = angular.module("appModuleDacha");
    'use strict';
    app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {     
        $stateProvider
            .state('companionshiplist', {
                url: '/companionshiplist',
                templateUrl: '/app/companionship/companionshiplist.html',
                controller: 'companionshipListController as vm'
            })
        
            .state('companionshiplistaddnew', {
                url: '/companionshiplistaddnew',               
                templateUrl: '/app/companionship/companionshipaddnew.html',
                controller: 'companionshipAddNewController as vm'
            })
            .state('companionshiplistupdate', {
                url: '/companionshiplistupdate',
            templateUrl: '/app/companionship/companionshipupdate.html',
            controller: 'companionshipUpdateController as vm',
            params: {               
                'id' : '',
                'name': '',
                'address': '',
                'registration': '',
                'chairman': '',
                'membership': '',
                'addition': ''
            }
        });
    });
})();
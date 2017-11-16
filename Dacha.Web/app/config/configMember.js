(function () {
    var app = angular.module("appModuleDacha");
    'use strict';
    app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider
            .state('memberlist', {
                url: '/memberlist',
                templateUrl: '/app/member/memberlist.html',
                controller: 'memberListController as vm',
                params: {
                    'companionshipId': '',
                    'companionshipName': ''
                }
            })
            .state('memberaddnew', {
                url: '/memberaddnew',
                templateUrl: '/app/member/memberaddnew.html',
                controller: 'memberAddNewController as vm',
                params: {
                    'companionshipId': '',
                    'companionshipName': ''
                }
            })
            .state('memberupdate', {
                url: '/memberupdate',
                templateUrl: '/app/member/memberupdate.html',
                controller: 'memberUpdateController as vm',
                params: {
                    'companionshipId': '',
                    'id': '',
                    'owner': '',
                    'plotNumber': '',
                    'plotAddress': '',
                    'plotSquare': '',
                    'ownerAddress': '',
                    'addition': ''
                }
            });
    });
})();
(function () {
    var app = angular.module("appModuleDacha");
    'use strict';
    app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider
            .state('memberdoclist', {
                url: '/memberdoclist',
                templateUrl: '/app/memberdoc/memberdoclist.html',
                controller: 'memberDocListController',// as vm',
                params: {
                    'memberId': '',
                    'memberOwner': ''
                }
            });
    });
})();
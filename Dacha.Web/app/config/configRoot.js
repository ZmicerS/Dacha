(function () {
    var app = angular.module("appModuleDacha");
    'use strict';
    app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {      
        $urlRouterProvider.otherwise('/head');
        $stateProvider
            .state('head', {
                url: '/head',
                template: '<div></div>', 
                controller: 'headController as vm'
            })
            .state('login', {
                url: '/login',             
                templateUrl: '/app/account/login.html',               
                controller: 'loginController as vm'
            })
            .state('register', {
                modal: true,
                url: '/register',
                templateUrl: '/app/account/register.html',                
                controller: 'registerController as vm'
            });           
    });
})();
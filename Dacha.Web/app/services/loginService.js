(function () {
    'use strict';
    angular.module('appModuleDacha')
    .factory('loginService', loginService); 
    loginService.$inject = ['$http', '$q', '$state', '$stateParams'];
    function loginService($http, $q, $state, $stateParams) {

        var service = {};
      
        service.login = function (loginData) {
           
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            var deferred = $q.defer();
            $http.post('/oauth2/token',
                data,
                {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                })
                .then(function (response) {                  
                    sessionStorage.setItem('access_token', response.data.access_token);
                    sessionStorage.setItem('token_type', response.data.token_type);
                    sessionStorage.setItem('expires_in', response.data.expires_in);
                    sessionStorage.setItem('refresh_token', response.data.refresh_token);                   
                    deferred.resolve(response);
                }, function (response) {
                                      
                    deferred.reject(response);
                });

            return deferred.promise;
        };
        return service;
    }

})();
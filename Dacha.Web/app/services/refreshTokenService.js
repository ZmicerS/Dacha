(function () {
    'use strict';
    angular.module('appModuleDacha')
    .factory('refreshTokenService', refreshTokenService);
    refreshTokenService.$inject = ['$http', '$q', '$state', '$stateParams'];
    function refreshTokenService($http, $q, $state, $stateParams) {
       
        var service = {};    
        var access_token = '';
        var token_type = '';
        var expires_in = '';
        var refresh_token = '';
        refresh_token = sessionStorage.getItem('refresh_token');
        //
        service.refreshToken = function () {            
            var data = "grant_type=refresh_token&refresh_token=" + refresh_token;            
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
                    deferred.reject(response);//.data);
                });

            return deferred.promise;
        };
        return service;
    }

})();
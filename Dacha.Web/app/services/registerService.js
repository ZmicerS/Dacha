(function () {
    'use strict';
    angular.module('appModuleDacha')
    .factory('registerService', registerService);
    registerService.$inject = ['$http', '$q', '$state', '$stateParams'];
    function registerService($http, $q, $state, $stateParams) {

        var service = {};

        service.register = function (registerData) {                       
            var deferred = $q.defer();

            var datasend = {
                Email: registerData.email,
                Password: registerData.password,
                ConfirmPassword: registerData.confirmPassword
            }

            $http({
                url: '/api/account/register',
                method: "POST",               
                headers: {
                    'Content-Type': 'application/json'
                },
                data: angular.toJson(datasend)
            })
                .then(function (response) {                                   
                    deferred.resolve(response);
                }, function (response) {                   
                    deferred.reject(response);
                });

            return deferred.promise;
        };
        return service;
    }
})();
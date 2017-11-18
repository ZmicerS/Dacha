(function () {
    'use strict';
    angular.module('appModuleDacha')
    .factory('companionshipService', companionshipService);
    companionshipService.$inject = ['$http', '$q', '$state', '$stateParams'];
    function companionshipService($http, $q, $state, $stateParams) {
        var urlCompanionship = '/api/Companionship';
        var service = {};
        service.getCompanionships = function () {
         
            var deferred = $q.defer();
            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
          
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
            $http({
                url: urlCompanionship,
                method: "GET",
                headers: currentHeader,
            })
                .then(function (response) {                                              
                    deferred.resolve(response);
                }, function (response) {                   
                    deferred.reject(response);//.data);//.error);
                });

            return deferred.promise;

        };

        service.addCompanionship = function (data)
        {
            var deferred = $q.defer();
            var datasend = {
                Id: "",
                Name: data.name,
                Address: data.address,
                Registration: data.registration,
                Chairman : data.chairman,
                Membership: data.membership,
                Addition: data.addition
            };
            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            currentHeader['Content-Type'] = 'application/json';
            if (accesstoken)
            {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
            
            $http({
                url: urlCompanionship,
                method: "POST",
                headers: currentHeader,               
                data: angular.toJson(datasend)
            })
                .then(function (response) {                                                 
                    deferred.resolve(response);
                }, function (response) {                    
                    deferred.reject(response);
                });
            return deferred.promise;
        }

        service.updateCompanionship = function (data) {
            var deferred = $q.defer();
            var datasend = {
                Id: data.id,
                Name: data.name,
                Address: data.address,
                Registration: data.registration,
                Chairman: data.chairman,
                Membership: data.membership,
                Addition: data.addition
            };
            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            currentHeader['Content-Type'] = 'application/json';
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
            
            $http({
                url: urlCompanionship +'/'+ data.id,
                method: "PUT",
                headers: currentHeader,             
                data: angular.toJson(datasend)
            })
                .then(function (response) {                                             
                    deferred.resolve(response);
                }, function (response) {                    
                    deferred.reject(response);
                });

            return deferred.promise;
        }

        service.deleteCompanionship = function (data) {
            var deferred = $q.defer();
            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            currentHeader['Content-Type'] = 'application/json';
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
            //

            $http({
                url: urlCompanionship + '/' + data.Id,
                method: "DELETE",
                headers: currentHeader                         
            }).then(function (response) {               
                deferred.resolve(response);
            }, function (response) {               
                deferred.reject(response);
            });
            return deferred.promise;
        }
        return service;
    }
})();

(function (app) {
    'use strict';
    angular.module('appModuleDacha')
    .factory('memberDocService', memberDocService);
    memberDocService.$inject = ['$http', '$q', '$state', '$stateParams'];
    function memberDocService($http, $q, $state, $stateParams) {
        var service = {};      
              
        service.getListIdDocs = function (memberId) {          
            var deferred = $q.defer();
            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};            
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }

            $http({              
                url: "api/MemberDocListId/" + memberId,
                method: "GET",
                headers: currentHeader
            })
                .then(function (response) {                                   
                    deferred.resolve(response);
                }, function (response) {                 
                    deferred.reject(response);
                });

            return deferred.promise;
        }

        service.upload = function (fileFormData) {
            var deferred = $q.defer();

            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            currentHeader['Content-Type'] = undefined;
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
            //

            $http({               
                url: '/api/upload/files',
                method: "POST",
                transformRequest: angular.identity,
                headers: currentHeader,
                data: fileFormData
            })
                .then(function (response) {                                                
                    deferred.resolve(response);
                }, function (response) {                 
                    deferred.reject(response);
                });
            return deferred.promise;
        }

        service.deleteMember = function (id) {
            var deferred = $q.defer();
            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            currentHeader['Content-Type'] = 'application/json';
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
            //

            $http({
                url: '/api/MemberDoc/' + id,
                method: "DELETE",
                headers: currentHeader,                            
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
//

(function () {
    'use strict';
    angular.module('appModuleDacha')
    .factory('memberService', memberService);
    memberService.$inject = ['$http', '$q', '$state', '$stateParams'];
    function memberService($http, $q, $state, $stateParams) {
        var service = {};   
        service.getMembersCompanionship = function (companionshipId) {         
            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
            
            var deferred = $q.defer();
            $http({
                url: '/api/GetMembersCompanionship/' + companionshipId,
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

        service.addMember = function (data) {
            var deferred = $q.defer();
            
            var datasend = {
                Id: "",//data.id,
                Owner: data.owner,
                OwnerAddress: data.ownerAddress,
                PlotNumber : data.plotNumber,
                PlotAddress: data.plotAddress,
                PlotSquare: data.plotSquare,
                Addition: data.addition,
                CompanionshipId: data.companionshipId
            };

            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            currentHeader['Content-Type'] = 'application/json';
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
 
            $http({
                url: '/api/Member',
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
        service.updateMember = function (data) {
            var deferred = $q.defer();
            var datasend = {
                Id: data.id,
                Owner: data.owner,
                OwnerAddress: data.ownerAddress,
                PlotNumber: data.plotNumber,
                PlotAddress: data.plotAddress,
                PlotSquare: data.plotSquare,
                Addition: data.addition,
                CompanionshipId: data.companionshipId
            };

            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            currentHeader['Content-Type'] = 'application/json';
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }

            $http({
                url: '/api/Member/' + data.id,
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

        service.deleteMember = function (data) {
            var deferred = $q.defer();
            var accesstoken = sessionStorage.getItem('access_token');
            var currentHeader = {};
            currentHeader['Content-Type'] = 'application/json';
            if (accesstoken) {
                currentHeader.Authorization = 'Bearer ' + accesstoken;
            }
            $http({
                url: '/api/Member/' + data.Id,
                method: "DELETE",
                headers: currentHeader              
            }).then(function (response) {                 
                deferred.resolve(response);
            }, function (response) {               
                deferred.reject(response);//.data);//.error);
            });
            return deferred.promise;
        }
        return service;
    }
})();

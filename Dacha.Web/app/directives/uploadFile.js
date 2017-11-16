(function () {
    "use strict";
    angular.module('appModuleDacha')
    //
    .directive('uploadFile', ['$parse', function ($parse) {
        return {
            restrict: 'A',           
            link: function (scope, element, attrs) {               
                var file_uploaded = $parse(attrs.uploadFile);          
                var file_uploadedSetter = file_uploaded.assign;                
                element.bind('change', function () {                        
                    scope.$apply(function () {                        
                        file_uploadedSetter(scope, element[0].files[0]);
                    });                     
                });
            }
        };
    }]);
//

})();
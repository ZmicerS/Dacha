(function () {
    "use strict";
    angular.module('appModuleDacha')
        .directive('uploadFiles', ['$parse', function ($parse) {
            var uploadFiles = function () {
                    var directive = {
                            link: function (scope, element, attrs) {
                                    var changedHandler = function () {
                                            scope.$apply(function () {
                                                    if (element[0].files) {
                                                            scope.files.length = 0;
                                                            angular.forEach(element[0].files, function (f) {
                                                                    scope.files.push(f);
                                                            });
                                                            scope.hasFiles = true;
                                                    }
                                            });
                                    };

                                    var resetHandler = function () {
                                            scope.$apply(function () {
                                                    scope.files.length = 0;
                                                    scope.hasFiles = false;
                                            });
                                    };

                                    element.bind('change', changedHandler);

                                    if (element[0].form) {
                                            angular.element(element[0].form).bind('reset', resetHandler);
                                    }

                                    // Watch the files so we can reset the input if needed
                                    scope.$watchCollection('files', function () {
                                            if (scope.files.length === 0) {
                                                    element.val(null);
                                            }
                                    })

                                    scope.$on('$destroy', function () {
                                            element.unbind('change', changedHandler);
                                            if (element[0].form) {
                                                    angular.element(element[0].form).unbind('reset', resetHandler);
                                            }
                                    });
                            },
                            restrict: 'A',
                            scope: {
                                    files: '=uploadFiles',
                                    hasFiles: '='
                            }
                    };

                    return directive;
            };
        }]);
    //

})();
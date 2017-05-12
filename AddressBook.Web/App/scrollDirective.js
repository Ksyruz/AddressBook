var uri = uri ||  getAppPath();
var modules = modules || [];
//var pagedata = pagedata || [];
var nav = nav || [];

(function () {
    'use strict';
    modules.push('scroll');

    angular.module('scroll', []).directive('whenScrolled', function () {
        return function (scope, elm, attr) {
            var raw = elm[0];

            elm.bind('scroll', function () {
                if (raw.scrollTop + raw.offsetHeight >= raw.scrollHeight) {
                    scope.$apply(attr.whenScrolled);
                }
            });
        };
    });
})();
var uri = uri || getAppPath();
var modules = modules || [];
var nav = nav || [];

(function () {
    'use strict';
    modules.push('ScrollFactory');

    angular.module('ScrollFactory', ['ngRoute', 'infinite-scroll', 'toastr'])
    .factory('ScrollSearch', ['$http', '$routeParams', function ($http, $routeParams) {
        var Scroll = function (urifor) {
            this.data = [];
            this.busy = false;
            this.after = '';
            this.end = true;
            this.counter = 0;
            this.controller = urifor;
        };

        Scroll.prototype.nextPage = function () {
            if (this.busy) return;

            if (this.end) {
                this.busy = true;
                $http.get(uri + 'Api/' + this.controller + '?search=' + $routeParams.id + 'page=' + this.counter)
                .then(function (response) {
                    var newitems = response.data;
                    for (var i = 0; i < newitems.length; i++) {
                        this.data.push(newitems[i]);
                    }
                    this.counter += 1;
                    if (response.data.length == 0) { this.end = false; }
                    else {
                        this.after = "t3_" + this.data[this.data.length - 1].ID;
                    }
                    //
                    this.busy = false;
                }.bind(this))
                .catch(function (response) {
                    toastr.error(response, 'error')
                });
            }
        };

        return Scroll;
    }])
    .factory('ScrollList', ['$http', '$routeParams', function ($http, $routeParams) {
        var Scroll = function (urifor) {
            this.data = [];
            this.busy = false;
            this.after = '';
            this.end = true;
            this.counter = 0;
            this.controller = urifor;
            //this.search = '';
        };

        Scroll.prototype.nextPage = function () {
            if (this.busy) return;

            if (this.end) {
                this.busy = true;
                var url = uri + 'Api/' + this.controller + "?";
                //toastr.info($routeParams, "Route")
                if (!isEmpty($routeParams) & !isEmpty($routeParams.id)) {
                    url = url + 'search=' + $routeParams.id + "&";
                }
                $http.get(url + 'page=' + this.counter)
                .then(function (response) {
                    //this.items.push(response.data);
                    var newitems = response.data;
                    for (var i = 0; i < newitems.length; i++) {
                        this.data.push(newitems[i]);
                    }
                    this.counter += 1;
                    if (response.data.length == 0) { this.end = false; }
                    else {
                        this.after = "t3_" + this.data[this.data.length - 1].ID;
                    }
                    this.busy = false;
                }.bind(this))
                 .catch(function (response) {
                     toastr.error("Could not get data", 'error');
                     console.log(response);
                 });
            }
        };

        Scroll.prototype.search = function () {
            if (this.busy) return;
            this.busy = true;
            this.end = true;
            this.data = [];
            this.counter = 0;
        };

        return Scroll;
    }]);
})();

function isEmpty(val) {
    return (val === undefined || val == null || val.length <= 0) ? true : false;
}
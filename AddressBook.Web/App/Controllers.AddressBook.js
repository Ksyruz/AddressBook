var uri = uri || getAppPath();
var appurl = appurl || getAppPath();
var modules = modules || [];
var nav = nav || [];

(function () {
    'use strict';
    modules.push('Address');
    nav.push('Address');
    angular.module('Address', ['ngRoute', 'ngAnimate', 'toastr', 'ScrollFactory', 'ngDropdowns', 'infinite-scroll', 'ui.bootstrap.datetimepicker', 'ui.dateTimeInput'])
    .controller('Address_list', ['$scope', '$http', 'ScrollList', function ($scope, $http, ScrollList) {
        $scope.scroll = new ScrollList('Address');
    }])
    .controller('Address_search', ['$scope', '$http', 'ScrollSearch', function ($scope, $http, ScrollSearch) {
        $scope.scroll = new ScrollSearch('Address');
    }])
    .controller('Address_details', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
        $http.get(uri + 'Api/Address/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; })
        .catch(function (response) {
            toastr.error('Could not get Address details', 'Error');
        });
    }])
    .controller('Address_create', ['$scope', '$http', '$routeParams', '$location', '$filter', function ($scope, $http, $routeParams, $location, $filter) {
        $scope.data = {
            ContactId: -1,
            Surname: '',
            Forename: '',
            PhoneIntlDialCode: 44,
            EmailAddress: '',
            TwitterId: '',
            IsFavourite: false,
            Gender: 0,
            Group: 0,
            IsRecent: false
        };

        $scope.save = function () {
            $http.post(uri + 'Api/Address/', $scope.data)
            .then(function (response) { $location.path("Address"); })
            .catch(function (response) {
                toastr.error('Could not save Address details!', 'Error');
                $scope.errors = JSON.parse(response.data.Message);
            });
        };
    }])
    .controller('Address_edit', ['$scope', '$http', '$routeParams', '$location', '$filter', function ($scope, $http, $routeParams, $location, $filter) {
        $http.get(uri + 'Api/Address/' + $routeParams.id)
        .then(function (response) {
            $scope.data = response.data;
        })
        .catch(function (response) {
            toastr.error('Could not get Address details!', 'Error');
        });

        $scope.save = function () {
            if(!$.isEmptyObject($scope.model.adding)){
                $scope.addEvent();
            }
            $http.put(uri + 'Api/Address/' + $routeParams.id, $scope.data)
            .then(function (response) { $location.path("Address"); toastr.success('Address was saved', 'Success'); })
            .catch(function (response) {
                $scope.errors = JSON.parse(response.data.Message);
                toastr.error('Could not save Address!', 'Error');
            });
        }
       
    }])
    .controller('Address_delete', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {
        $http.get(uri + 'Api/Address/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });
        $scope.save = function () {
            $http.delete(uri + 'Api/Address/' + $routeParams.id, $scope.data)
            .then(function (response) { $location.path("Address"); toastr.success('Address was deleted', 'Success'); })
            .catch(function (response) {
                $scope.errors = JSON.parse(response.data.Message);
                toastr.error('Could not delete Address!', 'Error');
            });
        }
    }])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
        .when('/Address', {
            title: 'Address - List',
            templateUrl: appurl + 'app/views/Address_List.html',
            controller: 'Address_list'
        })
        .when('/Address/Search/:id', {
            title: 'Address - List',
            templateUrl: appurl + 'app/views/Address_List.html',
            controller: 'Address_search'
        })
        .when('/Address/Create', {
            title: 'Address - Create',
            templateUrl: appurl + 'app/views/Address_Edit.html',
            controller: 'Address_create'
        })
        .when('/Address/Edit/:id', {
            title: 'Address - Edit',
            templateUrl: appurl + 'App/Views/Address_Edit.html',
            controller: 'Address_edit'
        })
        .when('/Address/Delete/:id', {
            title: 'Address - Delete',
            templateUrl: appurl + 'app/views/Address_Delete.html',
            controller: 'Address_delete'
        })
        .when('/Address/:id', {
            title: 'Address - Details',
            templateUrl: appurl + 'app/views/Address_Details.html',
            controller: 'Address_details'
        })
        .otherwise({
            title: 'Address - List',
            templateUrl: appurl + 'app/views/Address_List.html',
            controller: 'Address_list'
        });
    }]);
})();
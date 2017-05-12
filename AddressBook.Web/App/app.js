var uri = uri || getAppPath();
var modules = modules || [];
var nav = nav || [];
//valid_elements : 'a[href|target=_blank],strong/b,i,img,iframe,br,video',
//var uiTinymceConfig = uiTinymceConfig || {
//    selector: 'textarea',
//    height: 500,
//    theme: 'modern',
//    inline: false,
//    skin: 'lightgray',
//    trusted: true,
//    invalid_elements: "script, style",
//    verify_html: true,
//    media_strict: true,
//    convert_urls: false,
//    plugins: [
//      'advlist autolink lists link image charmap print preview hr anchor pagebreak',
//      'searchreplace wordcount visualblocks visualchars code fullscreen',
//      'insertdatetime media nonbreaking save table contextmenu directionality',
//      'emoticons template paste textcolor colorpicker textpattern imagetools'
//    ],
//    toolbar1: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media | forecolor backcolor emoticons',
//    image_advtab: true,
//    content_css: [
//      '//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
//      '//www.tinymce.com/css/codepen.min.css'],
//    imagetools_cors_hosts: ['www.tinymce.com', 'codepen.io']
//};

(function () {
    'use strict';
    modules.push('angular-clipboard')
    var app = angular.module('addressbookapp', modules);

    app.controller('MainMenu', function ($scope) {
        $scope.modules = modules;
        $scope.nav = nav;
    });

    app.run(['$rootElement', function ($rootElement) {
        $rootElement.off('click');
    }]);

    //angular.module('app').controller('Date', function ($scope) {
    //    $scope.Date = new Date();
    //});
    app.directive('a', function () {
        return {
            restrict: 'E',
            link: function (scope, elem, attrs) {
                if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                    elem.on('click', function (e) {
                        e.preventDefault();
                    });
                }
            }
        };
    });

    app.$inject = ['$scope', '$filter'];

    app.directive("customSort", function () {
        return {
            restrict: 'A',
            transclude: true,
            scope: {
                order: '=',
                sort: '='
            },
            template:
              ' <a ng-click="sort_by(order)" style="color: #555555;">' +
              '    <span ng-transclude></span>' +
              '    <i ng-class="selectedCls(order)"></i>' +
              '</a>',
            link: function (scope) {
                // change sorting order
                scope.sort_by = function (newSortingOrder) {
                    var sort = scope.sort;

                    if (sort.sortingOrder === newSortingOrder) {
                        sort.reverse = !sort.reverse;
                    }

                    sort.sortingOrder = newSortingOrder;
                };

                scope.selectedCls = function (column) {
                    if (column === scope.sort.sortingOrder) {
                        return ('icon-chevron-' + ((scope.sort.reverse) ? 'down' : 'up'));
                    }
                    else {
                        return 'icon-sort'
                    }
                };
            }// end link
        }
    });

    app.controller('search', function ($scope, $filter) {
        scope.sort = {
            sortingOrder: 'id',
            reverse: false
        };

        $scope.gap = 5;

        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 5;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.items = $scope.data;

        var searchMatch = function (haystack, needle) {
            if (!needle) {
                return true;
            }
            return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
        };

        // init the filtered items
        $scope.search = function () {
            $scope.filteredItems = $filter('filter')($scope.items, function (item) {
                for (var attr in item) {
                    if (searchMatch(item[attr], $scope.query))
                        return true;
                }
                return false;
            });
            // take care of the sorting order
            if ($scope.sort.sortingOrder !== '') {
                $scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.sort.sortingOrder, $scope.sort.reverse);
            }
            $scope.currentPage = 0;
            // now group by pages
            $scope.groupToPages();
        };

        // calculate page in place
        $scope.groupToPages = function () {
            $scope.pagedItems = [];

            for (var i = 0; i < $scope.filteredItems.length; i++) {
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
                }
            }
        };

        $scope.range = function (size, start, end) {
            var ret = [];
            console.log(size, start, end);

            if (size < end) {
                end = size;
                start = size - $scope.gap;
            }
            for (var i = start; i < end; i++) {
                ret.push(i);
            }
            console.log(ret);
            return ret;
        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage--;
            }
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pagedItems.length - 1) {
                $scope.currentPage++;
            }
        };

        $scope.setPage = function () {
            $scope.currentPage = this.n;
        };

        // functions have been describe process the data for display
        $scope.search();
    });

    modules.push('ngGetAppURLmod')
    angular.module('ngGetAppURLmod', [])
        .filter('trusted', ['$sce', function ($sce) {
            return function (url) {
                return $sce.trustAsResourceUrl(url);
            };
        }])
})();

function getAppURL() {
    var pathArray = location.pathname.split('/');
    var appPath = "/";
    for (var i = 1; i < pathArray.length - 2; i++) {
        appPath += pathArray[i] + "/";
    }
    return window.location.protocol + '//' + window.location.host + appPath;
    //return 'http://w28sdev05dev/symphony/';
}

function getAppPath() {
    return getAppURL();
    //return 'http://w28sdev05dev/symphony/service/';
    //return window.location.protocol + '//' + window.location.host + appPath + 'service/'; //service/
}

function GUID() {
    var d = new Date().getTime();
    if (window.performance && typeof window.performance.now === "function") {
        d += performance.now(); //use high-precision timer if available
    }
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}
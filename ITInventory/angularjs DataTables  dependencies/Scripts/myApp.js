//Demo of Searching Sorting and Pagination of Table with AngularJS - Advance Example

var myApp = angular.module('myApp', []);

//Not Necessary to Create Service, Same can be done in COntroller also as method like add() method
myApp.service('filteredListService', function () {

    this.searched = function (valLists, toSearch) {
        return _.filter(valLists,

        function (i) {
            /* Search Text in all 3 fields */
            return searchUtil(i, toSearch);
        });
    };

    this.paged = function (valLists, pageSize) {
        retVal = [];
        for (var i = 0; i < valLists.length; i++) {
            if (i % pageSize === 0) {
                retVal[Math.floor(i / pageSize)] = [valLists[i]];
            } else {
                retVal[Math.floor(i / pageSize)].push(valLists[i]);
            }
        }
        return retVal;
    };

});

//Inject Custom Service Created by us and Global service $filter. This is one way of specifying dependency Injection
var TableCtrl = myApp.controller('TableCtrl', function ($scope, $filter, filteredListService) {

    $scope.pageSize = 4;
    $scope.allItems = getDummyData();
    $scope.reverse = false;

    $scope.resetAll = function () {
        $scope.filteredList = $scope.allItems;
        $scope.newDeviceName = '';
        $scope.newName = '';
        $scope.newEmail = '';
        $scope.searchText = '';
        $scope.currentPage = 0;
        $scope.Header = ['', '', ''];
    }

    $scope.add = function () {
        $scope.allItems.push({
            DeviceName: $scope.newDeviceName,
            DeviceHistory: $scope.newName,
            Email: $scope.newEmail
        });
        $scope.resetAll();
    }

    $scope.search = function () {
        $scope.filteredList = filteredListService.searched($scope.allItems, $scope.searchText);

        if ($scope.searchText == '') {
            $scope.filteredList = $scope.allItems;
        }
        $scope.pagination();
    }

    // Calculate Total Number of Pages based on Search Result
    $scope.pagination = function () {
        $scope.ItemsByPage = filteredListService.paged($scope.filteredList, $scope.pageSize);
    };

    $scope.setPage = function () {
        $scope.currentPage = this.n;
    };

    $scope.firstPage = function () {
        $scope.currentPage = 0;
    };

    $scope.lastPage = function () {
        $scope.currentPage = $scope.ItemsByPage.length - 1;
    };

    $scope.range = function (input, total) {
        var ret = [];
        if (!total) {
            total = input;
            input = 0;
        }
        for (var i = input; i < total; i++) {
            if (i != 0 && i != total - 1) {
                ret.push(i);
            }
        }
        return ret;
    };

    $scope.sort = function (sortBy) {
        $scope.resetAll();

        $scope.columnToOrder = sortBy;

        //$Filter - Standard Service
        $scope.filteredList = $filter('orderBy')($scope.filteredList, $scope.columnToOrder, $scope.reverse);

        if ($scope.reverse) iconName = 'glyphicon glyphicon-chevron-up';
        else iconName = 'glyphicon glyphicon-chevron-down';

        if (sortBy === 'DeviceName') {
            $scope.Header[0] = iconName;
        } else if (sortBy === 'name') {
            $scope.Header[1] = iconName;
        } else {
            $scope.Header[2] = iconName;
        }

        $scope.reverse = !$scope.reverse;

        $scope.pagination();
    };

    //By Default sort ny Name
    $scope.sort('name');

});

function searchUtil(item, toSearch) {
    /* Search Text in all 3 fields */
    return (item.name.toLowerCase().indexOf(toSearch.toLowerCase()) > -1 || item.Email.toLowerCase().indexOf(toSearch.toLowerCase()) > -1 || item.DeviceName == toSearch) ? true : false;
}

/*Get Dummy Data for Example*/
function getDummyData() {
    debugger;
    $.ajax({
        type: "GET",
        url: "Devices/GetData",
        data: { userId: Id },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            alert(result)
        },
        error: function (response) {
            debugger;
            alert('eror');
        }
    });
}



//var app = angular.module('MyApp', ['datatables']);
    //app.controller('homeCtrl', ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder',
    //    function ($scope, $http, DTOptionsBuilder, DTColumnBuilder) {
    //        $scope.dtColumns = [
    //            //here We will add .withOption('name','column_name') for send column name to the server 
    //            DTColumnBuilder.newColumn("DeviceID", "DeviceID").withOption('name', 'DeviceID'),
    //            DTColumnBuilder.newColumn("DeviceName", "DeviceName").withOption('name', 'DeviceName'),
    //            DTColumnBuilder.newColumn("LocationFKID", "LocationFKID").withOption('name', 'LocationFKID'),
    //            DTColumnBuilder.newColumn("MACAddress", "MACAddress").withOption('name', 'MACAddress'),
    //            DTColumnBuilder.newColumn("AssignDate", "AssignDate").withOption('name', 'AssignDate'),
    //            DTColumnBuilder.newColumn("EntryDate", "EntryDate").withOption('name', 'EntryDate'),
    //            DTColumnBuilder.newColumn("DeviceTypeFKID", "DeviceTypeFKID").withOption('name', 'DeviceTypeFKID'),
    //            DTColumnBuilder.newColumn("DeviceStatus", "DeviceStatus").withOption('name', 'DeviceStatus'),

    //        ]
    //        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
    //            dataSrc: "data",
    //            url: "/Devices/getdata",
    //            type:"POST"
    //        })
    //        .withOption('processing', true) //for show progress bar
    //        .withOption('serverSide', true) // for server side processing
    //        .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
    //        .withDisplayLength(10) // Page size
    //        .withOption('aaSorting',[0,'asc']) // for default sorting column // here 0 means first column
//    }])

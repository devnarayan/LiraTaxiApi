(function () {
    'use strict';

    //create angularjs controller
    var app = angular.module('app', []);//set and get the angular module
    app.controller('driverController', ['$scope', '$http', driverController]);

    //angularjs controller method
    function driverController($scope, $http) {

        //declare variable for mainain ajax load and entry or edit mode
        $scope.loading = true;
        $scope.addMode = false;
        $scope.bashPath = "http://localhost:54025";
        //$scope.bashPath = "http://app.lirataxi.com";

        //get all driver information
        $http.get($scope.bashPath + '/api/driver/Get').success(function (data) {
            $scope.drivers = data;
            $scope.loading = false;
        })
        .error(function (data) {
            alert(data)
            $scope.error = "An Error has occured while loading posts!";
            $scope.loading = false;
        });

        //by pressing toggleEdit button ng-click in html, this method will be hit
        $scope.toggleEdit = function () {
            this.driver.editMode = !this.driver.editMode;
        };

        //by pressing toggleAdd button ng-click in html, this method will be hit
        $scope.toggleAdd = function () {
            $scope.addMode = !$scope.addMode;
        };

        //Inser driver
        $scope.add = function () {
            $scope.loading = true;
            $http.post($scope.bashPath + '/api/driver/Post/', this.newdriver).success(function (data) {
                alert("Added Successfully!!");
                $scope.addMode = false;
                $scope.drivers.push(data);
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Adding driver! " + data;
                $scope.loading = false;
            });
        };

        //Edit driver
        $scope.save = function () {
            alert("Edit");
            $scope.loading = true;
            var frien = this.driver;
            alert(frien);
            $http.put($scope.bashPath + '/api/driver/Put/' + frien.DriverID, frien).success(function (data) {
                alert("Saved Successfully!!");
                frien.editMode = false;
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Saving driver! " + data;
                $scope.loading = false;
            });
        };

        //Delete driver
        $scope.deletedriver = function () {
            $scope.loading = true;
            var DriverID = this.driver.DriverID;
            $http.delete($scope.bashPath + '/api/driver/Delete' + DriverID).success(function (data) {
                alert("Deleted Successfully!!");
                $.each($scope.drivers, function (i) {
                    if ($scope.drivers[i].DriverID === DriverID) {
                        $scope.drivers.splice(i, 1);
                        return false;
                    }
                });
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Saving driver! " + data;
                $scope.loading = false;
            });
        };
    }
})();
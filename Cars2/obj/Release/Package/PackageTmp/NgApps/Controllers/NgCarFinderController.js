
var app = angular.module('CarFinderApp', []);

app.controller('BobController', ['$scope', '$http', 'carSvc', function($scope, $http, carSvc) {
    $scope.selectedYear = '';
    $scope.selectedMake = '';
    $scope.selectedModel = '';
    $scope.selectedTrim = '';

    $scope.years = [];
    $scope.makes = [];
    $scope.models = [];
    $scope.trims = [];
    $scope.carView = [];

    // Use $scope.blocking to prevent extra db calls when $scope.trims is reset to null 
    //  - otherwise, onchange will call 'getCars()'
    //
    $scope.blocking = false;
//    $scope.cars = [];

    $scope.getYears = function () {
        carSvc.getYears()
            .then(function (data) { $scope.years = data; });
    };

    $scope.getMakes = function () {
        $scope.blocking = true;
        try{
            $scope.models = [];
            $scope.trims = [];
            $scope.carView = [];
            carSvc.getMakes($scope.selectedYear)
                .then(function (data) {
                    $scope.makes = data;
                });
        }
        catch (err) {
        }
        finally {
            $scope.blocking = false;
        }
    };

    $scope.getModels = function () {
        $scope.blocking = true;
        try {
            $scope.trims = [];
            $scope.carView = [];
            carSvc.getModels($scope.selectedYear, $scope.selectedMake)
                .then(function (data) {
                    $scope.models = data;
                });
        }
        catch (err) { }
        finally {
            $scope.blocking = false;
        }
    };

    function CheckDifferences(data) {
        if (data.cars.length > 1) {
            // There are multiple cars, so identify all fields where the data is different
            var c = data.cars;
            var base = c[0];      // Compare all other rows with this one
            // Now trace thru all properties...
            // And use normal 'for' loop since it's faster...
            var fields = Object.keys(base);
            var nCars = c.length;
            var nFields = fields.length;
            var i, j;
            var prop, field, same;
            var diff = "diff";
            for (i = 0; i < nFields; i++) {
                prop = fields[i];
                // Get the base value to compare against
                field = c[0][prop];
                // Now compare with all other cars...
                same = true;
                for (j = 1; j < nCars; j++) {
                    if (c[j][prop] != field) {
                        same = false;
                        break;
                    }
                }
                if (!same) {
                    // Add 'diff' property to each of these fields
                    //for (j = 0; j < nCars; j++)
                    base["d_" + prop] = "diff";
                }
            }
        }
    }

    $scope.getTrims = function () {
        carSvc.getTrims($scope.selectedYear, $scope.selectedMake, $scope.selectedModel)
            .then(function (data) {
                $scope.trims = data;
                $scope.getCarsNoTrim();
            });

        //$scope.trims = ['DX', 'LX', 'V'];
        //$('#yTrim')[0].selectedIndex = 0;
    };

    $scope.getCars = function () {
        if (!$scope.blocking) {
            carSvc.getCarsYMMT($scope.selectedYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim)
                .then(function (data) {
                    // See if there are any differences
                    CheckDifferences(data);
                    // And finally, update $scope.carView...
                    $scope.carView = data;
                });
        }
    };

    $scope.getCarsNoTrim = function () {
        carSvc.getCarsYMMT($scope.selectedYear, $scope.selectedMake, $scope.selectedModel)
            .then(function (data) {
                // See if there are any differences
                CheckDifferences(data);
                // And finally, update $scope.carView...
                $scope.carView = data;
            });
    };

    $scope.getYears();
}]);
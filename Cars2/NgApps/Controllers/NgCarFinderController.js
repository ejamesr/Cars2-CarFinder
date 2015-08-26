
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
//    $scope.cars = [];

    $scope.getYears = function () {
        carSvc.getYears()
            .then(function (data) { $scope.years = data; });
        // replace with call to service later
        // $scope.years = ['2000', '2001', '2002', '2003', '2004', '2005', '2006', '2007', '2008']
    };

    $scope.getMakes = function () {
        $scope.makes = [];
        $scope.models = [];
        $scope.trims = [];
        $scope.carView = [];
        carSvc.getMakes($scope.selectedYear)
            .then(function (data) {
                $scope.makes = data;
            });
        //// replace with call to service later
        //$scope.makes = ['Acura', 'Audi', 'BMW', 'Cadillac', 'Ford', 'Honda', 'Mazda', 'Nissan', 'Subaru', 'Toyota'];

        ////These options work, but only the second time, i.e., not until a choice has been made of the make
        //$scope.models = undefined;
        //$scope.trims = undefined;
        //$('#yMake')[0].selectedIndex = 0;
        //$('#yModel')[0].selectedIndex = 0;
        //$('#yTrim')[0].selectedIndex = 0;
        ////$("select#yMake").prop('selectedIndex', 0);
    };

    $scope.getModels = function () {
        $scope.trims = [];
        $scope.carView = [];
        carSvc.getModels($scope.selectedYear, $scope.selectedMake)
            .then(function (data) {
                $scope.models = data;
            });

        //// replace with call to service later
        //$scope.models = ['CX5', 'CTS', 'Corvette', 'Yaris', '4Runner'];
        //$scope.trims = undefined;
        //$('#yModel')[0].selectedIndex = 0;
        //$('#yTrim')[0].selectedIndex = 0;
    };

    $scope.getTrims = function () {
        carSvc.getTrims($scope.selectedYear, $scope.selectedMake, $scope.selectedModel)
            .then(function (data) {
                $scope.trims = data;
                if (data.length == 0)
                    $scope.getCarsNoTrim();
            });

        //$scope.trims = ['DX', 'LX', 'V'];
        //$('#yTrim')[0].selectedIndex = 0;
    };

    $scope.getCars = function() {
        carSvc.getCarsYMMT($scope.selectedYear, $scope.selectedMake, $scope.selectedModel, $scope.selectedTrim)
            .then(function (data) { $scope.carView = data });

        //$scope.cars = ['first car', 'second car', 'third car', 'LX', 'V'];
    };

    $scope.getCarsNoTrim = function () {
        carSvc.getCarsYMMT($scope.selectedYear, $scope.selectedMake, $scope.selectedModel)
            .then(function (data) { $scope.carView = data });

        //$scope.cars = ['first car', 'second car', 'third car', 'LX', 'V'];
    };

    $scope.getYears();
}]);
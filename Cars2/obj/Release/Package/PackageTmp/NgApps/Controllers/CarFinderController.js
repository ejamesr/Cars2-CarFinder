
var app = angular.module('CarFinderApp', []);

app.controller('BobController', ['$scope', '$http', function($scope, $http) {
    $scope.selectedYear = '';
    $scope.selectedMake = '';
    $scope.selectedModel = '';
    $scope.selectedTrim = '';

    $scope.years = [];
    $scope.makes = [];
    $scope.models = [];
    $scope.trims = [];
    $scope.cars = [];

    $scope.getYears = function () {
        // replace with call to service later
        $scope.years = ['2000', '2001', '2002', '2003', '2004', '2005'];
    };

    $scope.getMakes = function() {
        // replace with call to service later
        $scope.makes = ['Acura', 'Audi', 'BMW', 'Cadillac', 'Ford', 'Honda', 'Mazda', 'Nissan', 'Subaru', 'Toyota'];
        $('#makes').val("-- Select a make --");
    };

    $scope.getModels = function() {
        // replace with call to service later
        $scope.models = ['CX5', 'CTS', 'Corvette', 'Yaris', '4Runner'];
    };

    $scope.getTrims = function() {
        $scope.trims = ['DX', 'LX', 'V'];
    };

    $scope.getCars = function() {
        $scope.cars = ['first car', 'second car', 'third car', 'LX', 'V'];
    };

    $scope.getYears();
}]);
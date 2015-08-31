var app = angular.module('CarFinderApp');

app.factory('carSvc', ['$http', function ($http) {
    var factory = {};

    factory.getYears = function () {
        return $http.get('/api/years')
            .then(function (response) {
                return response.data;
            });
    };

    factory.getMakes = function (year) {
        var options = { params: { year: year } };
        return $http.get('/api/makes', options)
            .then(function (response) {
                return response.data;
            });
    };

    factory.getModels = function (year, make) {
        var options = { params: { year: year, make: make } };
        return $http.get('/api/models', options)
            .then(function (response) {
                return response.data;
            });
    };

    factory.getTrims = function (year, make, model) {
        var options = { params: { year: year, make: make, model: model } };
        return $http.get('/api/trims', options)
            .then(function (response) {
                return response.data;
            });
    };

    factory.getCarsYMMT = function (year, make, model, trim) {
        var options = { params: { year: year, make: make, model: model, trim: trim } };
        var x = $http.get('/cars', options)
            .then(function (response) {
                return response.data;
            });

        for (var propertyName in x) {
            var y = propertyName;
            var z = x[propertyName];
        }
        return x;
    };

    factory.getCarsYMM = function (year, make, model) {
        var options = { params: { year: year, make: make, model: model } };
        var x = $http.get('/cars', options)
            .then(function (response) {
                return response.data;
            });

        return x;
    };

    return factory;

}]);
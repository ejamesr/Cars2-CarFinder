var app = angular.module('CarFinderApp');

app.filter('fixAmp', function () {
    return function (input, year, make, model) {
        // Search for/replace '&amp;' string, it looks funny!
        if (input == '')
            return ('Image for: ' + year + ' ' + make + ' ' + model);

        //for (var i = 20; i<input.length; i++)
        //{
        //    var str = input.substring(i, 5);
        //    var x = input.charCodeAt(i);
        //}
        var str = input.replace('&amp;', '&');
        var ae = String.fromCharCode(226) + String.fromCharCode(8364) + String.fromCharCode(8220);
        str = str.replace(ae, '--');
        str = str.replace(ae, '--');
        return str;
    };
});
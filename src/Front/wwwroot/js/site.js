var myApp = angular.module('myModule', []).run(function() {
    console.log("hello from teams list");
});

myApp.controller('myController', function ($scope) {
    $scope.init = function (teams) {
        console.log(teams);
        $scope.teams = teams;
    }
});
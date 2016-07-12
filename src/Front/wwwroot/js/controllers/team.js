nbaFantasyApp
    .controller("teamController",
        function ($scope) {
            $scope.init = function (teams) {
                $scope.teams = teams;
            };
        });
nbaFantasyApp
    .controller("playerController",
        function ($scope) {
            $scope.init = function (players) {
                $scope.players = players;
            };
        });

app.controller('UserInfoCtrl', ['$scope', '$location', 'loginService', function ($scope, $location, loginService) {

    $scope.getUserInfo = function () {

        loginService.checkUserIsAuthentificated().then(function (response) {

            loginService.saveUserInfo(response.data);
            $location.path("/tasks");

        });
    };

    $scope.getUserInfo();

}]);
app.controller('LoginCtrl', ['$scope', '$location', 'loginService', function ($scope, $location, loginService) {

    $scope.login = function (user) {
        loginService.login(user).then(
            function (response) {
                loginService.saveUserInfo(response.data);
                $location.path("/tasks");
            },
            function (reason, status) {
                if (status == 400)
                    $scope.errorMessage = reason;
                else {
                    console.log(reason);
                }
            });
    };

    $scope.logout = function (user) {
        loginService.logout()
            .success(function (data) {
                loginService.logoutUser();
                $location.path("/account/login");
            })
            .error(function (reason) {
                console.log(reason);
            });
    };

}]);
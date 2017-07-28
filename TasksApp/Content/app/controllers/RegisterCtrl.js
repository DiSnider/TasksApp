app.controller('RegisterCtrl', ['$scope', '$location', 'loginService', function ($scope, $location, loginService) {

    $scope.register = function (user) {
        loginService.register(user).then(
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

}]);
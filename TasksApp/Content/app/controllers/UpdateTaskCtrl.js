app.controller('UpdateTaskCtrl', ['$scope', '$location', '$routeParams', 'tasksService', 'tags', function ($scope, $location, $routeParams, tasksService, tags) {

    $scope.tags = tags;

    tasksService.getTaskById($routeParams.id).then(function (response) {

        $scope.task = response.data;

    }, function (response) {
        console.error(response.data);
    });

    $scope.update = function (task) {

        tasksService.updateTask(task).then(
            function (response) {
                $location.path("/tasks");
            },
            function (response) {
                $scope.errorMessage = response.data;
                console.log(response.data);
            });
    };

    

}]);
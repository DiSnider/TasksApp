app.controller('CreateTaskCtrl', ['$scope', '$location', 'tasksService', 'tagsService', 'tags', function ($scope, $location, tasksService, tagsService, tags) {

    $scope.tags = tags;
    $scope.errorMessage = "";

    $scope.create = function (task) {

        tasksService.createTask(task).then(
            function (response) {
                $location.path("/tasks");
            },
            function (response) {
                $scope.errorMessage = response.data;
                console.log(response.data);
            });
    };

}]);
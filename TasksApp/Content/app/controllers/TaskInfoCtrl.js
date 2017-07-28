app.controller('TaskInfoCtrl', ['$scope', '$location', '$routeParams', 'tasksService', 'tagsService', 'tags', function ($scope, $location, $routeParams, tasksService, tags) {

    $scope.tags = tags;

    tasksService.getTaskById($routeParams.id).then(function (response) {
        $scope.task = response.data;
    }, function (response) {
        console.error(response.data);
    });

}]);
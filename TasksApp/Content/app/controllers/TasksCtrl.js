app.controller('TasksCtrl', ['$scope', '$location', '$routeParams', 'tasksService', 'tagsService', 'tasks', 'tags', function ($scope, $location, $routeParams, tasksService, tagsService, tasks, tags) {

    $scope.tasks = tasks;
    $scope.tags = tags;

    $scope.tasksPerPage = 2;
    $scope.pagesCount = Math.ceil($scope.tasks.length / $scope.tasksPerPage);
    $scope.page = $scope.pagesCount == 0 ? 0 : 1;

    $scope.searchOptions = {
        title: {
            enabled: false,
            pattern: ""
        },
        content: {
            enabled: false,
            pattern: ""
        },
        tags: {
            enabled: false,
            pattern: ""
        }
    };

    $scope.getNumbersForPagination = function () {
        var arr = [];
        for (var i = 1; i <= $scope.pagesCount; i++) {
            arr.push(i);
        }

        return arr;
    }

    $scope.changePage = function(newPageNumber) {
        if (newPageNumber > 0 && newPageNumber <= $scope.pagesCount){
            $scope.page = newPageNumber;
        }
    }

    function checkTaskIsSuitable(task) {
        var result = true;

        if ($scope.searchOptions.title.enabled) {
            result = result && task.title.includes($scope.searchOptions.title.pattern);
        }

        if ($scope.searchOptions.content.enabled) {
            result = result && task.content.includes($scope.searchOptions.content.pattern);
        }

        if ($scope.searchOptions.tags.enabled) {
            if ($scope.searchOptions.tags.pattern.name) {
                result = result && task.relatedTags.findIndex(tag => tag == $scope.searchOptions.tags.pattern.name) > -1;
            }           
        }

        return result;
    }

    $scope.getFilteredCurrentPageTasks = function () {
        var filteredArray = $scope.tasks.filter(checkTaskIsSuitable);
        $scope.pagesCount = Math.ceil(filteredArray.length / $scope.tasksPerPage);
        var startIndex = ($scope.page - 1) * $scope.tasksPerPage;
        var endIndex = (startIndex == filteredArray.length - 1) ? startIndex + 1 : startIndex + 2;
        return filteredArray.slice(startIndex, endIndex);
    }

    $scope.delete = function (id) {
        if (confirm("Are you sure you want to delete this task?")) {
            tasksService.deleteTaskById(id).then(
                function (response) {
                    var deletingElementIndex = $scope.tasks.findIndex(function (task) { return task.id == id });
                    if (deletingElementIndex > -1) {
                        $scope.tasks.splice(deletingElementIndex, 1);
                    }
                },
                function (reason, status) {
                    console.log(reason);
                });
        }    
    };
}]);
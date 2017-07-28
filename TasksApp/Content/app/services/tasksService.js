
app.factory("tasksService", ['$http', '$q', function ($http, $q) {
    return {

        getAllTasks: function () {
            var deferred = $q.defer();
            $http.get("/api/tasks").then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                deferred.reject();
            })
            return deferred.promise;
        },

        getTaskById: function (id) {
            return $http.get("/api/tasks/" + id);
        },

        createTask: function (task) {
            return $http.post("/api/tasks/", task);
        },

        updateTask: function (task) {
            return $http.put("/api/tasks/", task);
        },

        deleteTaskById: function (id) {
            return $http.delete("/api/tasks/" + id);
        }
    };

}]);
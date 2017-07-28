
app.factory("tagsService", ['$http', '$q', function ($http, $q) {
    return {

        getAllTags: function () {
            var deferred = $q.defer();
            $http.get("/api/tags").then(function (response) {
                deferred.resolve(response.data);
            }, function (response) {
                deferred.reject();
            })
            return deferred.promise;
        }

    };
}]);

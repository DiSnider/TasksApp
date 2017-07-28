app.factory("loginService", ['$http', '$rootScope', function ($http, $rootScope) {

    return {

        saveUserInfo: function(user) {
            $rootScope.currentUser = user;
        },

        logoutUser: function () {
            $rootScope.currentUser = null;
        },

        checkUserIsAuthentificated: function () {
            return $http.get("/api/account");
        },

        login: function (user) {
            return $http.post("/api/account/login", user);
        },

        logout: function() {
            return $http.post("/api/account/logout");
        },

        register: function (user) {
            return $http.post("/api/account/register", user);
        }

    };
}]);
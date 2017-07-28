app.factory('authInterceptor', ['$q', '$location', function ($q, $location) {

    var authInterceptorServiceFactory = {};

    var _responseError = function (rejection) {

        if (rejection.status === 401) {
            $location.path('/account/login');
        }

        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}])
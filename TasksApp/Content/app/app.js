
var app = angular.module('TasksApp', ['ngRoute'])
    .config(['$httpProvider', '$routeProvider', '$locationProvider', function ($httpProvider, $routeProvider, $locationProvider) {

        $httpProvider.interceptors.push('authInterceptor');

        $routeProvider
            .when('/account/login', {
                templateUrl: '/Content/app/views/account/login.html',
                controller: 'LoginCtrl',
                controllerAs: 'LoginCtrl'
            })
            .when('/account/register', {
                templateUrl: '/Content/app/views/account/register.html',
                controller: 'RegisterCtrl',
                controllerAs: 'RegisterCtrl'
            })
            .when('/tasks', {
                templateUrl: '/Content/app/views/tasks/list.html',
                controller: 'TasksCtrl',
                controllerAs: 'TasksCtrl',
                resolve: {
                    tasks: function (tasksService) {
                        return tasksService.getAllTasks();
                    },
                    tags: function (tagsService) {
                        return tagsService.getAllTags();
                    }
                }
            })
            .when('/tasks/create', {
                templateUrl: '/Content/app/views/tasks/create.html',
                controller: 'CreateTaskCtrl',
                controllerAs: 'CreateTaskCtrl',
                resolve: {
                    tags: function (tagsService) {
                        return tagsService.getAllTags();
                    }
                }
            })
            .when('/tasks/:id', {
                templateUrl: '/Content/app/views/tasks/onetask.html',
                controller: 'TaskInfoCtrl',
                controllerAs: 'TaskInfoCtrl',
                resolve: {
                    tags: function (tagsService) {
                        return tagsService.getAllTags();
                    }
                }
            })          
            .when('/tasks/update/:id', {
                templateUrl: '/Content/app/views/tasks/update.html',
                controller: 'UpdateTaskCtrl',
                controllerAs: 'UpdateTaskCtrl',
                resolve: {
                    tags: function (tagsService) {
                        return tagsService.getAllTags();
                    }
                }
            })
            .otherwise({
                redirectTo: '/tasks'
            });

    }
    ]);
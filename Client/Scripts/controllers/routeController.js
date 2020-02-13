var app = angular.module('deviceManagementApp', ['ngRoute', 'ngAnimate', 'ngCookies', 'ui.bootstrap']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'views/loginView.html',
            controller: 'loginController'
        })
        .when('/assignments', {
            templateUrl: function () {
                if (localStorage.getItem("userId")) {
                    return 'views/assignmentsView.html';
                }
                else {
                    return 'views/loginView.html'
                }
            },
            controller: 'assignmentsController'
        }).
    otherwise({
        redirectTo: '/'
    });
})

app.filter("jsDate", function () {
    return function (x) {
        return new Date(x);
    };
});
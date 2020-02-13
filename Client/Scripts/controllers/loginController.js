app.controller('loginController', ['$scope', '$location', '$cookies', '$animate', '$route', 'loginService',
    function ($scope, $location, $cookies, $animate, $route, loginService) {
        $scope.username = "";
        $scope.password = "";
        $scope.name = "";
        $scope.wrongData = false;
        $scope.login = true;
        $scope.showBtn = true;
        $scope.roles = [];
        $scope.locations = [];

        var getAllRoles = function () {
            loginService.getRoles().then(function (r) {
                if (r.data.GetAllRolesResult != null) {
                    var rolesList = r.data.GetAllRolesResult;
                    angular.forEach(rolesList, function (role) {
                        $scope.roles.push(role);
                    });
                }
            });
        }
        getAllRoles();

        var getAllLocations = function () {
            loginService.getLocations().then(function (r) {
                if (r.data.GetAllLocationsResult != null) {
                    var locationsList = r.data.GetAllLocationsResult;
                    angular.forEach(locationsList, function (location) {
                        $scope.locations.push(location);
                    });
                }
            });
        }
        getAllLocations();

        var shake = function () {
            var containerLogin = angular.element(document.querySelector('#containerLogin'));

            $animate.addClass(containerLogin, 'shake').then(function () {
                $animate.removeClass(containerLogin, 'shake');
            });

            $scope.wrongData = true;
        }

        var routeToMain = function () {
            $location.path('/assignments');
        }

        $scope.login = function () {
            loginService.login($scope.username, $scope.password).then(function (r) {
                if (r.data.LoginResult != null) {
                    var response = r.data.LoginResult;
                    $scope.userId = response.Id;

                    localStorage.setItem("userId", response.Id);
                    routeToMain();
                }
                else
                    shake();
            });
        }

        $scope.register = function () {
            if ($scope.username == '' || $scope.password == '' || $scope.name == '') {
                shake();
            }
            else {
                loginService.register($scope.username, $scope.password, $scope.name, $scope.role.Id, $scope.location.Id).then(function (r) {
                    if (r.data.InsertUserResult) {
                        console.log('am intrat aici');
                        var response = r.data.InsertUserResult;
                        $scope.userId = response.Id;

                        $scope.login = true;
                        $route.reload();
                    }
                    else
                        alert('A aparut o eroare!');
                });
            }
        }

        $scope.showRegister = function () {
            $scope.login = !$scope.login;
            console.log($scope.location);
            $scope.role = $scope.roles[0];
            $scope.location = $scope.locations[0];

        }
    }
]);


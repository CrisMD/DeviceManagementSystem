angular.module('deviceManagementApp')
.service('loginService', ['$http', 'requestService',
    function ($http, requestService) {
        this.getRoles = function () {
            return requestService.get('GetAllRoles');
        }

        this.getLocations = function () {
            return requestService.get('GetAllLocations');
        }

        this.login = function (username, password) {
            return requestService.get('Login?username=' + username + '&password=' + password);
        }

        this.register = function (username, password, name, role, location) {
            var data = {
                username: username,
                password: password,
                name: name,
                role: role,
                location: location
            }

            return requestService.post('InsertUser', data);
        }
    }
]);
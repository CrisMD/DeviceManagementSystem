angular.module('lcoation').factory('locationService', ['$http', function ($http) {

    const baseUrl = 'http://localhost:61754/WCFService.svc/';

    function addLocation(loginUser, location) {
        var params = {
            work_location: location.WorkLocation
        };

        return $http({
            method: 'POST',
            url: baseUrl + 'addLocation',
            params: params,
            headers: {
                'Content-Type': 'text/html',
                'Accept': 'application/json',
                'Authorization': loginUser.authorizationHeader
            }
        }).then(function (succes) {
            console.log("addLocation SUCCES");
            return succes
        }).catch(function (error) {
            console.log("addLocation FAILED");
            return error;
        })
    }

    function removeLocation(loginUser, locationId) {
        var params = { id: locationId };

        return $http({
            method: 'DELETE',
            url: baseUrl + 'removeLocation',
            params: params,
            headers: {
                'Content-Type': 'text/html',
                'Accept': 'application/json',
                'Authorization': loginUser.authorizationHeader
            }
        }).then(function (succes) {
            console.log("removeLocation SUCCES");
            return succes
        }).catch(function (error) {
            console.log("removeLocation FAILED");
            return error;
        })
    }

    function updateLocation(loginUser, location) {
        var params = {
            id: location.Id,
            work_location: location.WorkLocation,
            isActive: location.IsActive
        };

        return $http({
            method: 'PUT',
            url: baseUrl + 'updateLocation',
            params: params,
            headers: {
                'Content-Type': 'text/html',
                'Accept': 'application/json',
                'Authorization': loginUser.authorizationHeader
            }
        }).then(function (succes) {
            console.log("updateLocation SUCCES id: ", params.id);
            return succes
        }).catch(function (error) {
            console.log("updateLocation FAILED id: ", params.id);
            return error;
        })
    }

    function getLocations(loginUser) {
        return $http({
            method: 'GET',
            url: baseUrl + 'locations',
            headers: {
                'Content-Type': 'text/html',
                'Accept': 'application/json',
                'Authorization': loginUser.authorizationHeader
            }
        }).then(function (succes) {
            console.log("getLocations SUCCES");
            return succes;
        }).catch(function (error) {
            console.log("getLocations FAILED");
            return error;
        })
    }

    function getLocation(locationId) {
        var params = { id: locationId };

        return $http({
            method: 'GET',
            url: baseUrl + 'location',
            params: params,
            headers: {
                'Content-Type': 'text/html',
                'Accept': 'application/json'
            }
        }).then(function (succes) {
            console.log("getLocation SUCCES id: ", params.id);
            return succes;
        }).catch(function (error) {
            console.log("getLocation FAILED id: ", params.id);
            return error;
        })
    }

    return {
        addLocation: addLocation,
        removeLocation: removeLocation,
        updateLocation: updateLocation,
        getLocations: getLocations,
        getLocation: getLocation
    };

}]);
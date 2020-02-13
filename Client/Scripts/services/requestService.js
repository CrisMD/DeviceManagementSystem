angular.module('deviceManagementApp')
.service('requestService', ['$http', '$q', function ($http, $q) {
    var baseUrl = 'http://localhost:61754/WCFService.svc/';

    this.setAuthorizationHeader = function(token){
        $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;
        return;
    }

    this.request = function (method, url, data) {
        url = baseUrl + url;
        var deferred = $q.defer();

        $http({
            method: method,
            url: url,
            data: data,
            contentType: 'application/json',
        }).then(function (data) {
            deferred.resolve(data);
        }),
            (function (data, status, headers, config) {
                deferred.reject(data);
            });
        return deferred.promise;
    }

    this.get = function (url) {
        return this.request("GET", url);
    }

    this.post = function (url, data) {
        return this.request("POST", url, data);
    }

    this.delete = function (url) {
        return this.request("DELETE", url);
    }

    this.put = function (url, data) {
        return this.request("PUT", url, data);
    }
}]);

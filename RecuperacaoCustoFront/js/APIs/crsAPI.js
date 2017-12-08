angular.module('recCustoApp').service('crsAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'CRs';

    self.getCRs = function(search) {
        return $http.get(config.baseUrl + resource, {params:{search:search}});
    }

    self.getCR = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postCR = function(CR) {
        return $http.post(config.baseUrl + resource, CR);
    }

    self.deleteCR = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
angular.module('recCustoApp').service('categoriasCRAPI', ['$http', 'config', function($http, config) {

	var self = this;
    var resource = 'CategoriasCR';

    self.getCategoriasCR = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.postCategoriaCR = function(categoriaCR) {
        return $http.post(config.baseUrl + resource, categoriaCR);
    }

    self.deleteCategoriaCR = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }


}]);
angular.module('recCustoApp').service('tiposRecuperacoesAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'TiposRecuperacoes';

    self.getTiposRecuperacoes = function() {
        return $http.get(config.baseUrl + resource);
    }

}]);
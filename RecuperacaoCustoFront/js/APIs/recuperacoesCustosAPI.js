angular.module('recCustoApp').service('recuperacoesCustosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'RecuperacoesCustos';

    self.getRecuperacoesCustos = function(crOrigem, crDestino, respondido, aprovado) {
        return $http.get(config.baseUrl + resource, {params:{crOrigem:crOrigem, crDestino:crDestino, respondido:respondido, aprovado:aprovado}});
    }

    self.getRecuperacoesCustosPorCiclo = function(crOrigem, codCiclo) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/' + crOrigem + '/' + codCiclo);
    }

    self.getRecuperacaoCusto = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postRecuperacoesCustosPorCiclo = function(recuperacoes) {
        return $http.post(config.baseUrl + resource + '/PorCiclo', recuperacoes);
    }

    self.postRecuperacaoCusto = function(RecuperacaoCusto) {
        return $http.post(config.baseUrl + resource, RecuperacaoCusto);
    }

    self.putRecuperacaoCusto = function(id, RecuperacaoCusto) {
        return $http.put(config.baseUrl + resource + '/' + id, RecuperacaoCusto);
    }

    self.deleteRecuperacaoCusto = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
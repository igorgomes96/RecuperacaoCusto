angular.module('recCustoApp').service('recuperacoesCustosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'RecuperacoesCustos';

    self.getRecuperacoesCustosPorCiclo = function(codCiclo, crOrigem, crDestino, codTipo, respondido, aprovado) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/' + codCiclo, {params:{crOrigem:crOrigem, crDestino:crDestino, codTipo:codTipo, respondido:respondido, aprovado:aprovado}});
    }

    self.getRecuperacoesCustosEnviadasPorCiclo = function(codCiclo, respondido, aprovado) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/Enviadas/' + codCiclo, {params:{respondido:respondido, aprovado:aprovado}});
    }

    self.getRecuperacoesCustosRecebidasPorCiclo = function(codCiclo, respondido, aprovado) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/Recebidas/' + codCiclo, {params:{respondido:respondido, aprovado:aprovado}});
    }

    self.getRecuperacoesCustosPorCicloPorCREnvio = function(crOrigem, codCiclo) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/PorCREnvio/' + crOrigem + '/' + codCiclo);
    }

    self.getRecuperacoesCustosPorCicloPorCRDestino = function(crDestino, codCiclo) {
        return $http.get(config.baseUrl + resource + '/PorCiclo/PorCRDestino/' + crDestino + '/' + codCiclo);
    }

    self.getQtdaAprovacoesPendentes = function(codCiclo) {
        return $http.get(config.baseUrl + resource + '/QtdaAprovacoesPendentes/' + codCiclo);
    }

    self.putAprovarRecuperacao = function(codRec) {
        return $http.put(config.baseUrl + resource + '/Aprovar/' + codRec);
    }

    self.putReprovarRecuperacao = function(codRec, recuperacao) {
        return $http.put(config.baseUrl + resource + '/Reprovar/' + codRec, recuperacao);
    }

    self.postRecuperacoesCustosPorCiclo = function(recuperacoes) {
        return $http.post(config.baseUrl + resource + '/PorCiclo', recuperacoes);
    }

    self.deleteRecuperacaoCusto = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }
}]);
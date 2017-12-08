angular.module('recCustoApp').service('transferenciaReceitaAPI', ['$http', 'config', function($http, config) {

	var self = this;
    var resource = 'TransferenciasReceitas';

    self.getTransferenciasReceitas = function(ano, mes) {
        return $http.get(config.baseUrl + resource, {params:{ano:ano, mes:mes}});
    }

    self.getTransferenciaReceita = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postTransferenciaReceita = function(transferenciaReceita) {
        return $http.post(config.baseUrl + resource, transferenciaReceita);
    }

    self.putTransferenciaReceita = function(id, transferenciaReceita) {
        return $http.put(config.baseUrl + resource + '/' + id, transferenciaReceita);
    }

    self.deleteTransferenciaReceita = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }

    self.report = function(mes) {
        window.open(config.baseUrl + resource + '/Report?mes=' + mes);
    }

    self.getAnos = function() {
        return $http.get(config.baseUrl + resource + '/Anos');
    }

}]);
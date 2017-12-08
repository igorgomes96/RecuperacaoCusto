angular.module('recCustoApp').service('mesesBloqueadosTransfReceitaAPI', ['$http', 'config', function($http, config) {

	var self = this;
    var resource = 'MesesBloqueadosTransfReceita';

    self.getMesesBloqueadosTransfReceita = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.postMesBloqueadoTransfReceita = function(mesBloqueado) {
        return $http.post(config.baseUrl + resource, mesBloqueado);
    }

    self.desbloquearMesBloqueadoTransfReceita = function(mes) {
        return $http.post(config.baseUrl + resource + '/Desbloquear', mes);
    }

    self.getAnos = function() {
        return $http.get(config.baseUrl + resource + '/Anos');
    }

}]);
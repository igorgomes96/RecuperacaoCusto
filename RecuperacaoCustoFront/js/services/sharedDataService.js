angular.module('recCustoApp').service('sharedDataService', ['sessionStorageService', function(sessionStorageService) {

    var self = this;

    var usuario = null;
    var cicloAtual = null;
    var ultimoCR = null;

    self.setUsuario = function(user) {
        usuario = user;
    }

    self.getUsuario = function() {
        if (!usuario) {
            usuario = sessionStorageService.getUser();
            self.setUsuario(usuario);
        }
        return usuario;
    }

    self.setCicloAtual = function(valor) {
        cicloAtual = valor;
    }

    self.getCicloAtual = function() {
        return cicloAtual;
    }

    self.setUltimoCR = function(cr) {
        ultimoCR = cr;
    }

    self.getUltimoCR = function() {
        return ultimoCR;
    }




}]);
angular.module('recCustoApp').service('sharedDataService', ['sessionStorageService', '$rootScope', function(sessionStorageService, $rootScope) {

    var self = this;

    var usuario = null;
    var cicloAtual = null;
    var ultimoCR = null;
    var qtdaAprovacoesPendentes = 0;

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
        if (!valor)
            $rootScope.$broadcast('eventoAprovacao', null);
        else
            $rootScope.$broadcast('eventoAprovacao', valor.Codigo);
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
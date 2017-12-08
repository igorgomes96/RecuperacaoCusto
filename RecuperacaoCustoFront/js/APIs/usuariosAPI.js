angular.module('recCustoApp').service('usuariosAPI', ['$http', 'config', function($http, config) {

    var self = this;
    var resource = 'Usuarios';

    self.getUsuarios = function() {
        return $http.get(config.baseUrl + resource);
    }

    self.getGestores = function() {
        return $http.get(config.baseUrl + resource + '/Gestores');
    }

    self.getUsuario = function(id) {
        return $http.get(config.baseUrl + resource + '/' + id);
    }

    self.postUsuario = function(usuario) {
        return $http.post(config.baseUrl + resource, usuario);
    }

    self.putUsuarioAlteraSenha = function(id, senha) {
        return $http.put(config.baseUrl + resource + '/AlteraSenha/' + id, senha);
    }

    self.putUsuario = function(id, usuario) {
        return $http.put(config.baseUrl + resource + '/' + id, usuario);
    }

    self.deleteUsuario = function(id) {
        return $http.delete(config.baseUrl + resource + '/' + id);
    }

    self.recuperarSenha = function(login) {
        return $http.post(config.baseUrl + resource + '/' + login + '/RecuperarSenha');
    }
}]);
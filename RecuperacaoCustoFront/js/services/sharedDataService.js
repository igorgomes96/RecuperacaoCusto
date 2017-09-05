angular.module('recCustoApp').service('sharedDataService', ['localStorageService', function(localStorageService) {

    var self = this;

    var usuario = null;

    self.setUsuario = function(user) {
        usuario = user;
    }

    self.getUsuario = function() {
        if (!usuario) {
            usuario = localStorageService.getUser();
            self.setUsuario(usuario);
        }
        return usuario;
    }


}]);
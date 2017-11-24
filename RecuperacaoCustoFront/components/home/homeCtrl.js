angular.module('recCustoApp').controller('homeCtrl', ['sharedDataService', function(sharedDataService) {

	var self = this;

	self.usuario = sharedDataService.getUsuario();

	self.stateRecuperacao = self.usuario.Perfil == 'Administrador' ? 'containerRecCusto.ciclos' : 'containerRecCusto.envios';

}]);


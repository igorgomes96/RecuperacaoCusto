angular.module('recCustoApp').controller('containerTransfReceitaCtrl', ['sharedDataService', '$transitions', '$state', function(sharedDataService, $transitions, $state) {

	var self = this;
	
	self.usuario = sharedDataService.getUsuario();
	self.estadoAtual = $state.current.name;

	$transitions.onSuccess({}, function() {
		self.estadoAtual = $state.current.name;
	});
	
}]);
angular.module('recCustoApp').controller('containerRecCustoCtrl', ['$state', '$transitions', 'sharedDataService', '$scope', 'recuperacoesCustosAPI', function($state, $transitions, sharedDataService, $scope, recuperacoesCustosAPI) {

	var self = this;
	self.usuario = sharedDataService.getUsuario();
	self.estadoAtual = $state.current.name;
	var listenerAprovacao = null; 
	self.qtdaAprovacoesPendentes = null;


	$transitions.onSuccess({}, function() {
		self.estadoAtual = $state.current.name;
	});


	var qtdaAprovacoesPendentes = function(codCiclo) {
		recuperacoesCustosAPI.getQtdaAprovacoesPendentes(codCiclo)
		.then(function(dado) {
			self.qtdaAprovacoesPendentes = dado.data;
		});
	}

	if (self.usuario.Perfil == 'Gestor') {
		listenerAprovacao = $scope.$on('eventoAprovacao', function($event, codCiclo) {
			if (codCiclo) {
				qtdaAprovacoesPendentes(codCiclo);
			} else 
				self.qtdaAprovacoesPendentes = null;
		});
	}

	$scope.$on('$destroy', function () {
		if (listenerAprovacao)
			listenerAprovacao();
	});


}]);
angular.module('recCustoApp').controller('recebimentosCtrl', ['sharedDataService', 'messagesService', 'ciclosAPI', '$scope', 'crsAPI', 'recuperacoesCustosAPI', function(sharedDataService, messagesService, ciclosAPI, $scope, crsAPI, recuperacoesCustosAPI) {

	var self = this;

	self.usuario = sharedDataService.getUsuario();
	self.recuperacoes = [];
	$scope.cicloAtual = sharedDataService.getCicloAtual();

	var loadCiclos = function(success, error, ano, status) {
		ciclosAPI.getCiclos(ano, status)
		.then(success, error);
	}

	var sucessoLoadCiclos = function(dado) {
		dado.data.forEach(function (x) {
			x.DataInicio = new Date(x.DataInicio);
			x.DataFim = new Date(x.DataFim);
		});	
		self.ciclos = dado.data;

		if (self.ciclos.length > 0 && !$scope.cicloAtual)
			$scope.cicloAtual = self.ciclos[0];
	}

	var errorLoadCiclos = function(error) {
		messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os ciclos.');
	}

	var loadRecuperacoes = function(codCiclo) {
		recuperacoesCustosAPI.getRecuperacoesCustosRecebidasPorCiclo(codCiclo, true, true)
		.then(function(dado) {
			self.recuperacoes = dado.data;
		});
	}

	$scope.$watch('cicloAtual', function() {
		if ($scope.cicloAtual)
			loadRecuperacoes($scope.cicloAtual.Codigo);
		else 
			self.recuperacoes = [];
	});

	loadCiclos(sucessoLoadCiclos, errorLoadCiclos, null, 'Aberto');


}]);
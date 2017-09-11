angular.module('recCustoApp').controller('aprovacoesCtrl', ['sharedDataService', 'messagesService', 'ciclosAPI', '$scope', 'crsAPI', 'recuperacoesCustosAPI', function(sharedDataService, messagesService, ciclosAPI, $scope, crsAPI, recuperacoesCustosAPI) {

	var self = this;

	self.usuario = sharedDataService.getUsuario();
	self.recuperacoes = [];
	$scope.cicloAtual = sharedDataService.getCicloAtual();
	self.recReprova = null;

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
		recuperacoesCustosAPI.getRecuperacoesCustosRecebidasPorCiclo(codCiclo, false)
		.then(function(dado) {
			self.recuperacoes = dado.data;
		});
	}

	self.aprovarRecuperacao = function(codRec) {
		recuperacoesCustosAPI.putAprovarRecuperacao(codRec)
		.then(function() {
			messagesService.exibeMensagemSucesso('Recuperações aprovadas com sucesso!');
			loadRecuperacoes($scope.cicloAtual.Codigo);
		}, function(error) {
			messagesService.exibeMensagemErro(error.status, 'Erro ao aprovar recuperações de custos. Contate o administrador do sistema.');
		});
	}

	self.reprovarRecuperacao = function(codRec, rec) {
		recuperacoesCustosAPI.putReprovarRecuperacao(codRec, rec)
		.then(function() {
			messagesService.exibeMensagemSucesso('Recuperações reprovadas com sucesso!');
			loadRecuperacoes($scope.cicloAtual.Codigo);
		}, function(error) {
			messagesService.exibeMensagemErro(error.status, 'Erro ao reprovar recuperações de custos. Contate o administrador do sistema.');
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
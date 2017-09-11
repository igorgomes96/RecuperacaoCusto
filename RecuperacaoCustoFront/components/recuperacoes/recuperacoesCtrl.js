angular.module('recCustoApp').controller('recuperacoesCtrl', ['$scope', 'sharedDataService', 'ciclosAPI', 'messagesService', 'recuperacoesCustosAPI', 'tiposRecuperacoesAPI', function($scope, sharedDataService, ciclosAPI, messagesService, recuperacoesCustosAPI, tiposRecuperacoesAPI) {

	var self = this;

	self.recuperacoes = [];
	$scope.cicloAtual = sharedDataService.getCicloAtual();
	self.tipos = [];

	self.filtro = {
		crOrigem: null,
		crDestino: null,
		codTipo: null,
		respondido: true,
		aprovado: true
	}

	var loadTipos = function() {
		tiposRecuperacoesAPI.getTiposRecuperacoes()
		.then(function(dado) {
			self.tipos = dado.data;
		}, function(error) {
			messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os Tipos de Recuperações.');
		});
	}

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

	self.loadRecuperacoes = function(codCiclo) {
		recuperacoesCustosAPI.getRecuperacoesCustosPorCiclo(codCiclo, self.filtro.crOrigem, self.filtro.crDestino, self.filtro.codTipo, self.filtro.respondido, self.filtro.aprovado)
		.then(function(dado) {
			self.recuperacoes = dado.data;
		});
	}

	$scope.$watch('cicloAtual', function() {
		if ($scope.cicloAtual)
			self.loadRecuperacoes($scope.cicloAtual.Codigo);
		else 
			self.recuperacoes = [];
	});

	loadCiclos(sucessoLoadCiclos, errorLoadCiclos, null, null);
	loadTipos();


}]);
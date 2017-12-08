angular.module('recCustoApp').controller('recuperacoesCtrl', ['$scope', 'sharedDataService', 'ciclosAPI', 'messagesService', 'recuperacoesCustosAPI', 'tiposRecuperacoesAPI', '$rootScope', function($scope, sharedDataService, ciclosAPI, messagesService, recuperacoesCustosAPI, tiposRecuperacoesAPI, $rootScope) {

	var self = this;

	self.recuperacoes = [];
	$scope.cicloAtual = sharedDataService.getCicloAtual();
	self.tipos = [];

	self.filtro = {
		crOrigem: null,
		crDestino: null,
		codTipo: null,
		respondido: null,
		aprovado: null
	}

	var loadTipos = function() {
		tiposRecuperacoesAPI.getTiposRecuperacoes()
		.then(function(dado) {
			self.tipos = dado.data;
		}, function(error) {
			swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
			//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os Tipos de Recuperações.');
		});
	}

	var loadCiclos = function(success, error, ano, status) {
		$rootScope.mostraLoader = true;
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
		$rootScope.mostraLoader = false;
	}

	var errorLoadCiclos = function(error) {
		swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
		//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os ciclos.');
		$rootScope.mostraLoader = false;
	}

	self.loadRecuperacoes = function(codCiclo) {
		$rootScope.mostraLoader = true;
		recuperacoesCustosAPI.getRecuperacoesCustosPorCiclo(codCiclo, self.filtro.crOrigem, self.filtro.crDestino, self.filtro.codTipo, self.filtro.respondido, self.filtro.aprovado)
		.then(function(dado) {
			self.recuperacoes = dado.data;
			$rootScope.mostraLoader = false;
		}, function(error) {
			swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
			//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar as recuperações.');
			$rootScope.mostraLoader = false;
		});
	}

	self.deleteRecuperacao = function(id) {
		recuperacoesCustosAPI.deleteRecuperacaoCusto(id)
		.then(function() {
			self.loadRecuperacoes($scope.cicloAtual.Codigo);
			swal('Sucesso!', 'Recuperação de Custo cancelada com sucesso!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	$scope.$watch('cicloAtual', function() {
		if ($scope.cicloAtual){
			//self.loadRecuperacoes($scope.cicloAtual.Codigo);
			self.recuperacoes = [];
		}else 
			self.recuperacoes = [];
	});

	loadCiclos(sucessoLoadCiclos, errorLoadCiclos, null, null);
	loadTipos();


}]);
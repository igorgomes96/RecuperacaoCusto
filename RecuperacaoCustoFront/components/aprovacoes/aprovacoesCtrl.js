angular.module('recCustoApp').controller('aprovacoesCtrl', ['sharedDataService', 'ciclosAPI', '$scope', 'crsAPI', 'recuperacoesCustosAPI', '$rootScope', function(sharedDataService, ciclosAPI, $scope, crsAPI, recuperacoesCustosAPI, $rootScope) {

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
		swal('Erro', 'Falha ao carregar os ciclos.', 'error');
		//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os ciclos.');
	}

	var loadRecuperacoes = function(codCiclo) {
		$rootScope.mostraLoader = true;
		recuperacoesCustosAPI.getRecuperacoesCustosRecebidasPorCiclo(codCiclo, false)
		.then(function(dado) {
			$rootScope.mostraLoader = false;
			self.recuperacoes = dado.data;
		}, function(error) {
			swal('Erro ' + error.status, 'Erro ao carregar informações. Contate o administrador do sistema.', 'error');
			//messagesService.exibeMensagemErro(error.status, 'Erro ao carregar informações. Contate o administrador do sistema.');
			$rootScope.mostraLoader = false;
		});
	}

	self.aprovarRecuperacao = function(codRec) {
		$rootScope.mostraLoader = true;
		recuperacoesCustosAPI.putAprovarRecuperacao(codRec)
		.then(function() {
			//messagesService.exibeMensagemSucesso('Recuperações aprovadas com sucesso!');
			swal('Sucesso!', 'Recuperações aprovadas com sucesso!', 'success');
			loadRecuperacoes($scope.cicloAtual.Codigo);
			$rootScope.$broadcast('eventoAprovacao', $scope.cicloAtual.Codigo);
			$rootScope.mostraLoader = false;
		}, function(error) {
			swal('Erro ' + error.status, 'Erro ao aprovar recuperação de custos! ' + ((error.data && error.data.Message) || error.data || error), 'error');
			//messagesService.exibeMensagemErro(error.status, 'Erro ao aprovar recuperações de custos. Contate o administrador do sistema.');
			$rootScope.mostraLoader = false;
		});
	}

	self.reprovarRecuperacao = function(codRec, rec) {
		$rootScope.mostraLoader = true;
		recuperacoesCustosAPI.putReprovarRecuperacao(codRec, rec)
		.then(function() {
			//messagesService.exibeMensagemSucesso('Recuperações reprovadas com sucesso!');
			swal('Sucesso!', 'Recuperações reprovadas com sucesso!', 'success');
			loadRecuperacoes($scope.cicloAtual.Codigo);
			$rootScope.$broadcast('eventoAprovacao', $scope.cicloAtual.Codigo);
			$rootScope.mostraLoader = false;
		}, function(error) {
			swal('Erro ' + error.status, 'Erro ao aprovar recuperação de custos! ' + ((error.data && error.data.Message) || error.data || error), 'error');
			//messagesService.exibeMensagemErro(error.status, 'Erro ao reprovar recuperações de custos. Contate o administrador do sistema.');
			$rootScope.mostraLoader = false;
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
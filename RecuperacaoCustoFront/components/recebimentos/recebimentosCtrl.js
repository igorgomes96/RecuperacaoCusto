angular.module('recCustoApp').controller('recebimentosCtrl', ['sharedDataService', 'ciclosAPI', '$scope', 'crsAPI', 'recuperacoesCustosAPI', '$rootScope', function(sharedDataService, ciclosAPI, $scope, crsAPI, recuperacoesCustosAPI, $rootScope) {

	var self = this;

	self.usuario = sharedDataService.getUsuario();
	self.recuperacoes = [];
	$scope.cicloAtual = sharedDataService.getCicloAtual();

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

	var loadRecuperacoes = function(codCiclo) {
		$rootScope.mostraLoader = true;
		recuperacoesCustosAPI.getRecuperacoesCustosRecebidasPorCiclo(codCiclo, true, true)
		.then(function(dado) {
			self.recuperacoes = dado.data;
			$rootScope.mostraLoader = false;
		}, function(error) {
			swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
			//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar as recuperações.');
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
angular.module('recCustoApp').controller('ciclosCtrl', ['ciclosAPI', 'messagesService', '$scope', 'sharedDataService', 'crsAPI', function(ciclosAPI, messagesService, $scope, sharedDataService, crsAPI) {

	var self = this;
	self.ciclos = [];
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
		if (self.ciclos.length > 0)
			$scope.cicloAtual = self.ciclos[0];
	}

	var errorLoadCiclos = function(error) {
		messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os ciclos.');
	}

	self.abrirCiclo = function(ciclo) {
		ciclo.Status = 'Aberto';
		ciclosAPI.postCiclo(ciclo)
		.then(function(dado) {
			$scope.cicloAtual.Codigo = dado.data.Codigo;
			messagesService.exibeMensagemSucesso('Ciclo Aberto com sucesso!');
		});
	}

	self.fecharCiclo = function(ciclo) {
		ciclo.Status = 'Fechado';
		ciclosAPI.putCiclo(ciclo.Codigo, ciclo)
		.then(function(dado) {
			$scope.cicloAtual = null;
			messagesService.exibeMensagemSucesso('Ciclo Fechado com sucesso!');
		});
	}

	loadCiclos(sucessoLoadCiclos, errorLoadCiclos, null, 'Aberto');


	$scope.$watch('cicloAtual', function() {
		sharedDataService.setCicloAtual($scope.cicloAtual);
		//$rootScope.$broadcast('cicloAlteradoEvento', $scope.cicloAtual);
	});

}]);
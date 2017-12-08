angular.module('recCustoApp').controller('ciclosCtrl', ['ciclosAPI', '$scope', 'sharedDataService', 'crsAPI', function(ciclosAPI, $scope, sharedDataService, crsAPI) {

	var self = this;
	self.ciclos = [];
	self.todosCiclos = [];
	$scope.cicloAtual = sharedDataService.getCicloAtual();

	var loadCiclos = function(success, error, ano, status) {
		ciclosAPI.getCiclos(ano, status)
		.then(success, error);
	}

	var loadCiclosFechados = function() {
		ciclosAPI.getCiclos(null, 'Fechado')
		.then(function(dado) {
			self.todosCiclos = dado.data;
		});
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
		swal('Erro ' + error.status, 'Falha ao carregar os ciclos. ' + ((error.data && error.data.Message) || error.data || error), 'error');
		//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os ciclos.');
	}

	self.abrirCiclo = function(ciclo) {
		ciclo.Status = 'Aberto';
		ciclosAPI.postCiclo(ciclo)
		.then(function(dado) {
			$scope.cicloAtual.Codigo = dado.data.Codigo;
			swal('Sucesso!', 'Ciclo aberto com sucesso!', 'success');
			//messagesService.exibeMensagemSucesso('Ciclo Aberto com sucesso!');
		});
	}

	self.fecharCiclo = function(ciclo) {
		ciclo.Status = 'Fechado';
		ciclosAPI.putCiclo(ciclo.Codigo, ciclo)
		.then(function(dado) {
			loadCiclosFechados();
			$scope.cicloAtual = null;
			swal('Sucesso!', 'Ciclo fechado com sucesso!', 'success');
			//messagesService.exibeMensagemSucesso('Ciclo Fechado com sucesso!');
		});
	}

	self.reabrirCiclo = function(ciclo) {
		if (self.cicloAtual) {
			swal('Inválido!', 'Não podem haver ciclos abertos para reabrir outro ciclo!', 'error');
		} else { 
			ciclo.Status = 'Aberto';
			ciclosAPI.putCiclo(ciclo.Codigo, ciclo)
			.then(function() {
				loadCiclos(sucessoLoadCiclos, errorLoadCiclos, null, 'Aberto');
				loadCiclosFechados();
				swal('Sucesso!', 'Ciclo reaberto com sucesso!', 'success');
				//messagesService.exibeMensagemSucesso('Ciclo Fechado com sucesso!');
			});
		}
	}

	loadCiclos(sucessoLoadCiclos, errorLoadCiclos, null, 'Aberto');
	loadCiclosFechados();

	$scope.$watch('cicloAtual', function() {
		sharedDataService.setCicloAtual($scope.cicloAtual);
		//$rootScope.$broadcast('cicloAlteradoEvento', $scope.cicloAtual);
	});

}]);
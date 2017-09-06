angular.module('recCustoApp').controller('enviosCtrl', ['sharedDataService', 'messagesService', 'ciclosAPI', '$scope', 'crsAPI', 'tiposRecuperacoesAPI', 'recuperacoesCustosAPI', function(sharedDataService, messagesService, ciclosAPI, $scope, crsAPI, tiposRecuperacoesAPI, recuperacoesCustosAPI) {

	var self = this;
	self.crsOrigem = [];
	self.usuario = sharedDataService.getUsuario();
	self.tipos = [];
	self.recuperacoes = [];
	$scope.cicloAtual = sharedDataService.getCicloAtual();


	var loadCiclos = function(success, error, ano, status) {
		ciclosAPI.getCiclos(ano, status)
		.then(success, error);
	}

	var loadCRs = function(success, error, responsavel) {
		crsAPI.getCRs(responsavel)
		.then(success, error);
	}

	var loadTipos = function(success, error) {
		tiposRecuperacoesAPI.getTiposRecuperacoes()
		.then(function(dado) {
			self.tipos = dado.data;
		}, function(error) {
			messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os Tipos de Recuperações.');
		});
	}

	var loadRecuperacoes = function(crOrigem, codCiclo) {
		recuperacoesCustosAPI.getRecuperacoesCustosPorCicloPorCREnvio(crOrigem, codCiclo)
		.then(function(dado) {
			self.recuperacoes = dado.data;
		});
	}

	self.alteraCROrigem = function(crOrigem) {
		if (!crOrigem || !$scope.cicloAtual){
			self.recuperacoes = [];
		} else {
			loadRecuperacoes(crOrigem, $scope.cicloAtual.Codigo);
		}
	}

	self.enviarRecuperacoes = function(rec) {
		rec.CodCiclo = $scope.cicloAtual.Codigo;
		recuperacoesCustosAPI.postRecuperacoesCustosPorCiclo(rec)
		.then(function() {
			messagesService.exibeMensagemSucesso('Solicitação de recuperações de custos enviadas com sucesso!');
			loadRecuperacoes(self.novaRec.CROrigem, $scope.cicloAtual.Codigo);
			$scope.formRecuperacoes.$setPristine();
			self.novaRec = null;
		}, function(error) {
			if (error && error.status && error.status == 404) {
				messagesService.exibeMensagemErro(error.status, 'CR de destino não encontrado!');
			} else {
				messagesService.exibeMensagemErro(error.status, 'Falha ao enviar solicitação.');
			}
		});
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

	var sucessoLoadCRs = function(dado) {
		self.crsOrigem = dado.data;
		//if (self.crsOrigem && self.crsOrigem.length > 0)
			//self.novaRec.CROrigem = self.crsOrigem[0].Codigo;
	}

	var errorLoadCRs = function(error) {
		messagesService.exibeMensagemErro(error.status, "Falha ao carregar os CRs do usuário '" + self.usuario.Login + " - " + self.usuario.Nome + "'.");
	}

	loadCiclos(sucessoLoadCiclos, errorLoadCiclos, null, 'Aberto');
	loadCRs(sucessoLoadCRs, errorLoadCRs, self.usuario.Login);
	loadTipos();


	$scope.$watch('cicloAtual', function() {
		sharedDataService.setCicloAtual($scope.cicloAtual);
		if ($scope.cicloAtual && self.crOrigem) {
			loadRecuperacoes(self.novaRec.CROrigem, $scope.cicloAtual)
		} else {
			self.recuperacoes = [];
		}
		//$rootScope.$broadcast('cicloAlteradoEvento', $scope.cicloAtual);
	});


}]); 
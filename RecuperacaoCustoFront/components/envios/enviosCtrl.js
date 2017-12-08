angular.module('recCustoApp').controller('enviosCtrl', ['sharedDataService', 'ciclosAPI', '$scope', 'crsAPI', 'tiposRecuperacoesAPI', 'recuperacoesCustosAPI', 'uploadFileService', '$rootScope', function(sharedDataService, ciclosAPI, $scope, crsAPI, tiposRecuperacoesAPI, recuperacoesCustosAPI, uploadFileService, $rootScope) {

	var self = this;
	self.crsOrigem = [];
	self.usuario = sharedDataService.getUsuario();
	self.tipos = [];
	self.recuperacoes = [];
	self.novaRec = null;
	$scope.form = {};
	$scope.cicloAtual = sharedDataService.getCicloAtual();


	self.sendFile = function() {
		exibeLoader();
		uploadFileService.sendFile()
		.done(function (response) {
			$('input[type="file"]').val("");
			//$rootScope.$broadcast('newBaseEvent');
		  	swal('Sucesso!', 'Dados importados com sucesso!', 'success');
		  	//messagesService.exibeMensagemSucesso("Dados importados com sucesso!");
		  	$("#input-files-label").hide();
        	$("#button-upload-file").hide();
        	if ($scope.cicloAtual)
        		loadRecuperacoes($scope.cicloAtual.Codigo);
        	ocultaLoader();
		}).fail(function(jqXHR, textStatus, errorThrown) {
			if (jqXHR && jqXHR.status && jqXHR.responseText) {
				var e = JSON.parse(jqXHR.responseText);
				if (e.Message) {
					swal('Erro ' + jqXHR.status, e.Message, 'error');
					//messagesService.exibeMensagemErro(jqXHR.status, e.Message);
				} else {
					swal('Erro ' + jqXHR.status, jqXHR.responseText, 'error');
					//messagesService.exibeMensagemErro(jqXHR.status, jqXHR.responseText);
				}
			} else {
				swal('Erro!', 'Ocorreu um erro inesperado!', 'error');
				//messagesService.exibeMensagemErro(500, 'Ocorreu um error insperado!');
			}
			ocultaLoader();
		});	
	}

	self.deleteRecuperacao = function(id) {
		swal({
		  	title: "Confirma Cancelamento?",
		  	text: "O cancelamento de um Recuperação não pode ser desfeito!",
		  	icon: "warning",
		  	buttons: true,
		  	dangerMode: true,
		}).then((willDelete) => {
			if (willDelete) {
				recuperacoesCustosAPI.deleteRecuperacaoCusto(id)
				.then(function() {
					loadRecuperacoes($scope.cicloAtual.Codigo);
					swal('Sucesso!', 'Recuperação de Custo cancelada com sucesso!', 'success');
				}, function(error) {
					swal('Erro!', error.data.Message || error.data || error, 'error');
				});
			}
		});
	}


	var loadCiclos = function(success, error, ano, status) {
		$rootScope.mostraLoader = true;
		ciclosAPI.getCiclos(ano, status)
		.then(success, error);
	}


	var loadTipos = function(success, error) {
		$rootScope.mostraLoader = true;
		tiposRecuperacoesAPI.getTiposRecuperacoes()
		.then(function(dado) {
			self.tipos = dado.data;
			$rootScope.mostraLoader = false;
		}, function(error) {
			swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
			//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os Tipos de Recuperações.');
			$rootScope.mostraLoader = false;
		});
	}

	var loadRecuperacoes = function(codCiclo) {
		$rootScope.mostraLoader = true;
		recuperacoesCustosAPI.getRecuperacoesCustosEnviadasPorCiclo(codCiclo)
		.then(function(dado) {
			self.recuperacoes = dado.data;
			$rootScope.mostraLoader = false;
		}, function(error) {
			swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
			//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar as recuperações.');
			$rootScope.mostraLoader = false;
		});
	}

	self.enviarRecuperacoes = function() {
		exibeLoader();
		self.novaRec.CodCiclo = $scope.cicloAtual.Codigo;
		self.novaRec.DataHora = new Date();
		recuperacoesCustosAPI.postRecuperacoesCustosPorCiclo(self.novaRec)
		.then(function() {
			ocultaLoader();
			swal('Sucesso!', 'Solicitação de recuperações de custos enviada com sucesso!', 'success');
			//messagesService.exibeMensagemSucesso('Solicitação de recuperações de custos enviadas com sucesso!');
			loadRecuperacoes($scope.cicloAtual.Codigo);
			$scope.form.formRecuperacoes.$setPristine();
			self.novaRec = null;
			// self.novaRec = { CROrigem: sharedDataService.getUltimoCR() }
		}, function(error) {
			ocultaLoader();
			swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
			// if (error && error.status && error.data) {
			// 	messagesService.exibeMensagemErro(error.status, error.data);
			// } else {
			// 	messagesService.exibeMensagemErro(error.status, 'Falha ao enviar solicitação.');
			// }
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
		$rootScope.mostraLoader = false;
	}

	var errorLoadCiclos = function(error) {
		swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
		//messagesService.exibeMensagemErro(error.status, 'Falha ao carregar os ciclos.');
		$rootScope.mostraLoader = false;
	}

	loadCiclos(sucessoLoadCiclos, errorLoadCiclos, null, 'Aberto');
	// loadCRs(sucessoLoadCRs, errorLoadCRs, self.usuario.Login);
	loadTipos();


	$scope.$watch('cicloAtual', function() {
		sharedDataService.setCicloAtual($scope.cicloAtual);
		if ($scope.cicloAtual) {
			// loadRecuperacoes(self.novaRec.CROrigem, $scope.cicloAtual.Codigo)
			loadRecuperacoes($scope.cicloAtual.Codigo);
		} else {
			self.recuperacoes = [];
		}
		//$rootScope.$broadcast('cicloAlteradoEvento', $scope.cicloAtual);
	});

	if ($scope.cicloAtual)
		loadRecuperacoes($scope.cicloAtual.Codigo);


}]); 
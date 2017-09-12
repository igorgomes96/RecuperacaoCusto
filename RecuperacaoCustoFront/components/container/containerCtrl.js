angular.module('recCustoApp').controller('containerCtrl', ['$state', '$transitions', 'sessionStorageService', '$window', 'sharedDataService', 'autenticacaoAPI', 'messagesService', '$scope', 'recuperacoesCustosAPI', function($state, $transitions, sessionStorageService, $window, sharedDataService, autenticacaoAPI, messagesService, $scope, recuperacoesCustosAPI) {

	var self = this;
	self.estadoAtual = $state.current.name;
	self.usuario = sharedDataService.getUsuario();
	var listenerAprovacao = null; 
	self.qtdaAprovacoesPendentes = null;


	$transitions.onSuccess({}, function() {
		self.estadoAtual = $state.current.name;
	});

	self.logout = function() {
		sessionStorageService.deleteUser();
		autenticacaoAPI.postLogout()
		.then(function() {
			$window.location.replace('components/autenticacao/autenticacao.html');
		}, function() {
			messagesService.exibeMensagemErro(500, 'Erro ao realizar Logout!');
		});
	}

	var qtdaAprovacoesPendentes = function(codCiclo) {
		recuperacoesCustosAPI.getQtdaAprovacoesPendentes(codCiclo)
		.then(function(dado) {
			self.qtdaAprovacoesPendentes = dado.data;
		});
	}

	if (self.usuario.Perfil == 'Gestor') {
		listenerAprovacao = $scope.$on('eventoAprovacao', function($event, codCiclo) {
			if (codCiclo) {
				qtdaAprovacoesPendentes(codCiclo);
			} else 
				self.qtdaAprovacoesPendentes = null;
		});
	}

	$scope.$on('$destroy', function () {
		if (listenerAprovacao)
			listenerAprovacao();
	});


}]);
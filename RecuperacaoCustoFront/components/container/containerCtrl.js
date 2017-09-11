angular.module('recCustoApp').controller('containerCtrl', ['$state', '$transitions', 'sessionStorageService', '$window', 'sharedDataService', 'autenticacaoAPI', 'messagesService', function($state, $transitions, sessionStorageService, $window, sharedDataService, autenticacaoAPI, messagesService) {

	var self = this;
	self.estadoAtual = $state.current.name;
	self.usuario = sharedDataService.getUsuario();


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

}]);
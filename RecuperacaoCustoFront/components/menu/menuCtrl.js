angular.module('recCustoApp').controller('menuCtrl', ['$state', 'sharedDataService', 'sessionStorageService', 'autenticacaoAPI', '$window', function($state, sharedDataService, sessionStorageService, autenticacaoAPI, $window) {

	var self = this;
	self.usuario = sharedDataService.getUsuario();
	self.estadoAtual = $state.current.name;

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
angular.module('recCustoApp', ['ui.router']);

angular.module('recCustoApp').controller('autenticacaoCtrl', ['sessionStorageService', 'autenticacaoAPI', 'messagesService', function (sessionStorageService, autenticacaoAPI, messagesService) {
	var self = this;
	self.mostraLoader = false;

	self.keypressedSenha = function ($event, user) {
        if ($event.which == 13)
            self.autentica(user);
    }

	self.autentica = function(user) {
		self.mostraLoader = true;
		autenticacaoAPI.postLogin(user)
		.then(function(dado) {

			sessionStorageService.saveUser(dado.data);
			window.location.href = "../../index.html";
			self.mostraLoader = false;

		}, function(error) {
			self.mostraLoader = false;
			messagesService.exibeMensagemErro(error.status, error.data.Message);
			console.log(error);
			
		});

	}

	self.limpar = function(user) {
		if (user) {
			user.Login = "";
			user.Senha = "";
		}
	}


}]);
angular.module('recCustoApp', ['ui.router']);

angular.module('recCustoApp').controller('autenticacaoCtrl', ['sessionStorageService', 'autenticacaoAPI', function (sessionStorageService, autenticacaoAPI) {
	var self = this;


	self.autentica = function(user) {
		
		autenticacaoAPI.postLogin(user)
		.then(function(dado) {

			
			sessionStorageService.saveUser(dado.data);
			window.location.href = "../../index.html";
			
		}, function(error) {

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
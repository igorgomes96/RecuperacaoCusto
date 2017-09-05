angular.module('recCustoApp').controller('autenticacaoCtrl', ['sharedDataService', 'localStorageService', 'autenticacaoAPI', '$state', function (sharedDataService, localStorageService, autenticacaoAPI, $state) {
	var self = this;


	self.erro = null;

	self.autentica = function(user) {
		autenticacaoAPI.postLogin(user)
		.then(function(dado) {
			//$state.go("menuContainer.dashboard");
			$state.go("containerHome.home");
			localStorageService.saveUser(dado.data);
			sharedDataService.setUsuario(dado.data);
		}, function(error) {

			console.log(error);
			
		});

	}

	self.fechaErro = function() {
		self.erro = null;
	}

	self.limpar = function(user) {
		if (user) {
			user.Login = "";
			user.Senha = "";
		}
	}


}]);
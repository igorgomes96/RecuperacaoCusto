angular.module('recCustoApp').controller('usuarioCtrl', ['sharedDataService', 'usuariosAPI', 'messagesService', '$scope', 'sessionStorageService', 'autenticacaoAPI', '$window', function(sharedDataService, usuariosAPI, messagesService, $scope, sessionStorageService, autenticacaoAPI, $window) {

	var self = this;
	self.userLogado = self.usuario = sharedDataService.getUsuario();
	self.editando = false;
	self.alterarSenha = false; 

	var loadUsuario = function() {
		usuariosAPI.getUsuario(self.userLogado.Login)
		.then(function(dado) {
			self.usuario = dado.data;
		}, function(error) {
			swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
			//messagesService.exibeMensagemErro(error.status, 'Erro ao carregar dados do usuário!');
		});
	}

	self.logout = function() {
		sessionStorageService.deleteUser();
		autenticacaoAPI.postLogout()
		.then(function() {
			$window.location.replace('components/autenticacao/autenticacao.html');
		}, function() {
			messagesService.exibeMensagemErro(500, 'Erro ao realizar Logout!');
		});
		
	}

	self.cancelar = function() {
		self.editando = false;
		self.alterarSenha = false;
		loadUsuario();
		$scope.formUsuario.$setPristine();
	}

	self.salvar = function(usuario) {
		usuariosAPI.putUsuario(usuario.Login, usuario)
		.then(function(dado) {
			if (self.alterarSenha) {
				usuariosAPI.putUsuarioAlteraSenha(usuario.Login, usuario.NovaSenha)
				.then(function() {
					self.cancelar();
					swal('Sucesso!', 'Usuário salvo com sucesso!', 'success');
					//messagesService.exibeMensagemSucesso("Usuário salvo com sucesso!");
				}, function(error) {
					messagesService.exibeMensagemErro(error.status, error.data.Message || error.data || error);
				});
			}
			else{
				self.cancelar();
				swal('Sucesso!', 'Usuário salvo com sucesso!', 'success');
				//messagesService.exibeMensagemSucesso("Usuário salvo com sucesso!");
			}
		}, function(error) {
			console.log(error);
			swal('Erro ' + error.status, (error.data && error.data.Message) || error.data || error, 'error');
			//messagesService.exibeMensagemErro(error.status, error.data.Message);
		});
	}

	loadUsuario();
	
}]);
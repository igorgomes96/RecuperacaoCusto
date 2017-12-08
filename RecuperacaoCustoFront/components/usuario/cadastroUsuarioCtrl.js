angular.module('recCustoApp').controller('cadastroUsuarioCtrl', ['sharedDataService', 'usuariosAPI', '$scope', function(sharedDataService, usuariosAPI, $scope) {

	var self = this;
	self.usuarios = [];
	self.editaLogin = false;

	self.atualizaLogin = function(usuario) {
		if (self.editaLogin) return;
		var temp = usuario.Email == null ? [] : usuario.Email.split('@');
		if (temp.length > 0) {
			usuario.Login = temp[0];
		} else {
			usuario.Login = null;
		}
	}

	self.saveUser = function() {
		usuariosAPI.postUsuario(self.usuario)
		.then(function(dado) {
			$scope.formUsuario.$setPristine();
			self.usuario = null;
			loadUsuarios();
			self.editaLogin = false;
			swal('Sucesso!', 'Usuário salvo com sucesso!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	var loadUsuarios = function() {
		usuariosAPI.getUsuarios()
		.then(function(dado) {
			self.usuarios = dado.data;
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	self.selecionarUsuario = function(usuario) {
		self.usuario = usuario;
		$('#modalBuscaUsuario').modal('hide');
	}

	self.excluirUsuario = function(login) {
		swal({
		  	title: "Confirma Exclusão?",
		  	text: "A exclusão de um usuário não pode ser desfeita!",
		  	icon: "warning",
		  	buttons: true,
		  	dangerMode: true,
		}).then((willDelete) => {
		  	if (willDelete) {
		  		self.editaLogin = false;
		    	usuariosAPI.deleteUsuario(login)
		    	.then(function(dado) {
		    		loadUsuarios();
		    		swal('Sucesso!', 'Usuário excluído com sucesso!', 'success');
		    	}, function(error) {
					swal('Erro!', (error.data && error.data.Message) || error.data || error, 'error');
				});
		  	}
		});
	}

	self.recuperarSenha = function(usuario) {
		usuariosAPI.recuperarSenha(usuario.Login)
		.then(function() {
			swal('Sucesso!', 'A senha foi enviada para o e-mail ' + usuario.Email + '!', 'success');
		}, function(error) {
			swal('Erro!', (error.data && error.data.Message) || error.data || error, 'error');
		});
	}

	loadUsuarios();

}]);

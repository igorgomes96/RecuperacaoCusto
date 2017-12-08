angular.module('recCustoApp').controller('cadastroCRCtrl', ['usuariosAPI', 'categoriasCRAPI', 'crsAPI', function(usuariosAPI, categoriasCRAPI, crsAPI) {

	var self = this;

	self.gestores = [];

	self.keypressedCR = function ($event) {
        if ($event.which == 13)
            self.autentica(user);
    }


    self.keypressedFiltro = function ($event) {
        if ($event.which == 13)
            self.autentica(user);
    }


	var loadGestores = function() {
		usuariosAPI.getGestores()
		.then(function(dado) {
			self.gestores = dado.data;
		});
	}

	var loadCategorias = function() {
		categoriasCRAPI.getCategoriasCR()
		.then(function(dado) {
			self.categorias = dado.data;
		});
	}

	self.salvarCR = function() {
		crsAPI.postCR(self.cr)
		.then(function() {
			self.cr = null;
			self.loadCRs(self.filtroCR);
			swal('Sucesso!', 'CR salvo com sucesso!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	self.excluirCR = function(codigoCR) {
		crsAPI.deleteCR(codigoCR)
		.then(function() {
			self.cr = null;
			self.loadCRs(self.filtroCR);
			swal('Sucesso!', 'CR excluído com sucesso!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}


	self.buscarCR = function() {
		crsAPI.getCR(self.cr.Codigo)
		.then(function(dado) {
			self.cr = dado.data;
		}, function(error) {
			self.cr = null;
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	self.loadCRs = function(pesquisa) {
		crsAPI.getCRs(pesquisa)
		.then(function(dado) {
			self.crs = dado.data;
		}, function(error) {
			self.crs= null;
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	loadGestores();
	loadCategorias();
	
}]);
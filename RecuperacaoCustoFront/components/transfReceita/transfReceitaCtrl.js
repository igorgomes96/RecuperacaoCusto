angular.module('recCustoApp').controller('transfReceitaCtrl', ['transferenciaReceitaAPI', '$scope', 'mesesBloqueadosTransfReceitaAPI', function(transferenciaReceitaAPI, $scope, mesesBloqueadosTransfReceitaAPI) {

	var self = this;
	self.transf = null;

	self.anos = [];

	self.meses = [
		{Mes: 0, Texto: 'Janeiro'},
		{Mes: 1, Texto: 'Fevereiro'},
		{Mes: 2, Texto: 'Março'},
		{Mes: 3, Texto: 'Abril'},
		{Mes: 4, Texto: 'Maio'},
		{Mes: 5, Texto: 'Junho'},
		{Mes: 6, Texto: 'Julho'},
		{Mes: 7, Texto: 'Agosto'},
		{Mes: 8, Texto: 'Setembro'},
		{Mes: 9, Texto: 'Outubro'},
		{Mes: 10, Texto: 'Novembro'},
		{Mes: 11, Texto: 'Dezembro'}
	];

	var hoje = new Date();
	self.ano = hoje.getYear() + 1900;
	self.mes = hoje.getMonth();
	self.mesBloqueado = false;

	var loadAnos = function() {
		transferenciaReceitaAPI.getAnos()
		.then(function(dado) {
			self.anos = dado.data;
		});
	}

	self.saveTransferenciaReceita = function() {
		transferenciaReceitaAPI.postTransferenciaReceita(self.transf)
		.then(function() {
			self.transf = null;
			$scope.formTransf.$setPristine();
			self.loadTransf();
			loadAnos();
			swal('Sucesso!', 'Transferência de Receita realizada com sucesso!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	self.deleteTransf = function(codigo) {
		transferenciaReceitaAPI.deleteTransferenciaReceita(codigo)
		.then(function() {
			self.loadTransf();
			loadAnos();
			swal('Sucesso!', 'Transferência de Receita excluída com sucesso!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	var verificaMesBloqueado = function(ano, mes) {
		self.mesBloqueado = self.mesesBloqueados && self.mesesBloqueados.some(function(x) {
			return (x.Mes.getYear() + 1900) == ano && x.Mes.getMonth() == mes;
		});
	}

	self.loadTransf = function() {
		verificaMesBloqueado(self.ano, self.mes);
		transferenciaReceitaAPI.getTransferenciasReceitas(self.ano, self.mes + 1)
		.then(function(dado) {
			dado.data.forEach(function(x) {
				x.DataEmissao = new Date(x.DataEmissao); 
			});
			self.transferencias = dado.data;
		});
	}

	var loadMesesBloqueados = function() {
		mesesBloqueadosTransfReceitaAPI.getMesesBloqueadosTransfReceita()
		.then(function(dado) {
			dado.data.forEach(function(x) {
				x.Mes = new Date(x.Mes);
			});
			self.mesesBloqueados = dado.data;

			self.loadTransf();
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	loadMesesBloqueados();
	loadAnos();

}]);
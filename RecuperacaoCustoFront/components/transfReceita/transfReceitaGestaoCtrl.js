angular.module('recCustoApp').controller('transfReceitaGestaoCtrl', ['mesesBloqueadosTransfReceitaAPI','transferenciaReceitaAPI', '$filter', function(mesesBloqueadosTransfReceitaAPI, transferenciaReceitaAPI, $filter) {

	var self = this;

	var dataAtual = new Date();
	self.ano = dataAtual.getYear() + 1900;
	self.mes = dataAtual.getMonth();

	self.anosBloqueados = [];
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

	self.mesesBloqueados = [];

	self.filtrarPorAno = function(ano) {
		return function(mes) {
			return !ano || mes.Mes.getYear() + 1900 == ano;
		}
	}

	var loadAnos = function() {
		transferenciaReceitaAPI.getAnos()
		.then(function(dado) {
			self.anos = dado.data;
		});
	}

	var loadAnosBloqueados = function() {
		mesesBloqueadosTransfReceitaAPI.getAnos()
		.then(function(dado) {
			self.anosBloqueados = dado.data;
		});
	}

	var getMes = function() {
		return new Date(self.ano, self.mes, 1);
	}

	var loadMesesBloqueados = function() {
		mesesBloqueadosTransfReceitaAPI.getMesesBloqueadosTransfReceita()
		.then(function(dado) {
			dado.data.forEach(function(x) {
				x.Mes = new Date(x.Mes);
			});
			self.mesesBloqueados = dado.data;
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	self.bloquearMes = function() {
		mesesBloqueadosTransfReceitaAPI.postMesBloqueadoTransfReceita(getMes())
		.then(function() {
			loadMesesBloqueados();
			loadAnosBloqueados();
			swal('Sucesso!', 'Mês de Referência Bloqueado!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	self.desbloquearMes = function(mes) {
		mesesBloqueadosTransfReceitaAPI.desbloquearMesBloqueadoTransfReceita(mes)
		.then(function() {
			loadMesesBloqueados();
			loadAnosBloqueados();
			swal('Sucesso!', 'Mês de Referência Desbloqueado!', 'success');
		}, function(error) {
			swal('Erro!', error.data.Message || error.data || error, 'error');
		});
	}

	self.downloadFile = function() {
		console.log(getMes());
		transferenciaReceitaAPI.report(encodeURIComponent($filter('date')(getMes())));
	}

	loadMesesBloqueados();
	loadAnos();
	loadAnosBloqueados();

}]);
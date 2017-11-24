angular.module('recCustoApp').controller('transfReceitaCtrl', ['transferenciaReceitaAPI', '$scope', function(transferenciaReceitaAPI, $scope) {

	var self = this;
	self.transf = null;

	self.saveTransferenciaReceita = function() {
		transferenciaReceitaAPI.postTransferenciaReceita(self.transf)
		.then(function() {
			self.transf = null;
			$scope.formTransf.$setPristine();
			load();
		});
	}

	var load = function() {
		transferenciaReceitaAPI.getTransferenciasReceitas()
		.then(function(dado) {
			self.transferencias = dado.data;
		});
	}

	load();

}]);
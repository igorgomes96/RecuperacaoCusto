angular.module('recCustoApp').controller('containerCtrl', ['$state', '$transitions', function($state, $transitions) {

	var self = this;
	self.estadoAtual = $state.current.name;

	$transitions.onSuccess({}, function() {
		self.estadoAtual = $state.current.name;
	});

}]);
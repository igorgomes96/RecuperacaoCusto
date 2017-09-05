angular.module('recCustoApp').controller('enviosCtrl', [function() {

	var self = this;
	self.ano = (new Date()).getYear() + 1901;

	self.anos = [];
	for (var a = self.ano-1; a < self.ano + 4;a++)
		self.anos.push(a);

}]);
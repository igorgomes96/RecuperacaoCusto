angular.module('recCustoApp').service('autenticacaoAPI', ['$http', 'config', function($http, config) {

	this.postLogin = function(user) {
		return $http.post(config.baseUrl + "Autentica", user);
	}

	this.postLogout = function() {
		return $http.post(config.baseUrl + "Logout");
	}

}]);
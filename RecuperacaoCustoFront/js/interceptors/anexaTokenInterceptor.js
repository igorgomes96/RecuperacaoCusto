angular.module('recCustoApp').factory('anexaTokenInterceptor', ['sharedDataService', function(sharedDataService) {
	return {
		request: function(config) {
			var user = sharedDataService.getUsuario();
			if (user && user.Token) 
				config.headers['Authorization'] = 'Basic ' + user.Token;
			return config;

		}
	}
}]);
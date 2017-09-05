angular.module("recCustoApp").factory("anexaTokenInterceptor", ['sessionStorageService', function(sessionStorageService) {
	return {
		request: function(config) {
			var user = sessionStorageService.getUser();
			if (user && user.Token) 
				config.headers['Authorization'] = 'Basic ' + user.Token;
			return config;
		}
	}
}]);
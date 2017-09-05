angular.module('recCustoApp').config(['$httpProvider', function($httpProvider) {
	$httpProvider.interceptors.push('anexaTokenInterceptor');
	$httpProvider.interceptors.push('errorInterceptor');
}]);
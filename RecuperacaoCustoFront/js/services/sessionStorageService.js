angular.module('recCustoApp').service('sessionStorageService', ['$window', function($window) {
	var self = this;
	
	self.saveUser = function(user) {
		$window.sessionStorage.setItem('user', JSON.stringify(user));
	}

	self.getUser = function() {
		var temp = $window.sessionStorage.getItem('user');
		if (temp) temp = JSON.parse(temp);
		return temp; 
	}

	self.deleteUser = function() {
		$window.sessionStorage.removeItem('user');
	}
}]);
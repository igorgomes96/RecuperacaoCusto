angular.module('recCustoApp', ['ui.router']);

//Verifica se está autenticado
angular.module('recCustoApp').run(function() {

	if (!window.sessionStorage.getItem('user')) {
		window.location.replace('components/autenticacao/autenticacao.html');
	}

});
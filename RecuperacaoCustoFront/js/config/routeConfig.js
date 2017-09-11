angular.module('recCustoApp').config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function($stateProvider, $urlRouterProvider, $locationProvider) {

    //$locationProvider.hashPrefix('');
    $urlRouterProvider.otherwise('/home');
    
    $stateProvider

    .state('unauthenticated', {
        url: '/unauthenticated',
        templateUrl: 'components/autenticacao/unauthenticated.html'
    })

    .state('usuario', {
        url: '/usuario',
        templateUrl: 'components/usuario/usuario.html',
        controller: 'usuarioCtrl as ct'
    })

    .state('container', {
        templateUrl: 'components/container/container.html',
        controller: 'containerCtrl as ctMain'
    })

    .state('container.home', {
    	url: '/home',
    	templateUrl: 'components/home/home.html'
    })

    .state('container.ciclos', {
        url: '/ciclos',
        templateUrl: 'components/ciclos/ciclos.html',
        controller: 'ciclosCtrl as ct'
    })

    .state('container.envios', {
    	url: '/envios',
    	templateUrl: 'components/envios/envios.html',
    	controller: 'enviosCtrl as ct'
    })

    .state('container.aprovacoes', {
    	url: '/aprovacoes',
    	templateUrl: 'components/aprovacoes/aprovacoes.html',
    	controller: 'aprovacoesCtrl as ct'
    })

    .state('container.recebimentos', {
        url: '/recebimentos',
        templateUrl: 'components/recebimentos/recebimentos.html',
        controller: 'recebimentosCtrl as ct'
    })

    .state('container.recuperacoes', {
        url: '/recuperacoes',
        templateUrl: 'components/recuperacoes/recuperacoes.html',
        controller: 'recuperacoesCtrl as ct'
    });
    
}]);
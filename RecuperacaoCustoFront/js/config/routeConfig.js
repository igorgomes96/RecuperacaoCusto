angular.module('recCustoApp').config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function($stateProvider, $urlRouterProvider, $locationProvider) {

    //$locationProvider.hashPrefix('');
    $urlRouterProvider.otherwise('/home');
    
    $stateProvider

    // .state('autenticacao', {
    //     url: '/autenticacao',
    //     templateUrl: 'components/autenticacao/autenticacao.html',
    //     controller: 'autenticacaoCtrl as ct'
    // })

    .state('container', {
        templateUrl: 'components/container/container.html',
        controller: 'containerCtrl as ctMain'
    })

    .state('container.home', {
    	url: '/home',
    	templateUrl: 'components/home/home.html'
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
    });
    
}]);
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

    .state('containerRecCusto', {
        templateUrl: 'components/containerRecCusto/containerRecCusto.html',
        controller: 'containerRecCustoCtrl as ctMain'
    })

    .state('containerTransfReceita', {
        templateUrl: 'components/containerTransfReceita/containerTransfReceita.html',
        controller: 'containerTransfReceitaCtrl as ctMain'
    })

    .state('home', {
    	url: '/home',
    	templateUrl: 'components/home/home.html',
        controller: 'homeCtrl as ct'
    })

    .state('containerRecCusto.ciclos', {
        url: '/ciclos',
        templateUrl: 'components/ciclos/ciclos.html',
        controller: 'ciclosCtrl as ct'
    })

    .state('containerRecCusto.envios', {
    	url: '/envios',
    	templateUrl: 'components/envios/envios.html',
    	controller: 'enviosCtrl as ct'
    })

    .state('containerRecCusto.aprovacoes', {
    	url: '/aprovacoes',
    	templateUrl: 'components/aprovacoes/aprovacoes.html',
    	controller: 'aprovacoesCtrl as ct'
    })

    .state('containerRecCusto.recebimentos', {
        url: '/recebimentos',
        templateUrl: 'components/recebimentos/recebimentos.html',
        controller: 'recebimentosCtrl as ct'
    })

    .state('containerRecCusto.recuperacoes', {
        url: '/recuperacoes',
        templateUrl: 'components/recuperacoes/recuperacoes.html',
        controller: 'recuperacoesCtrl as ct'
    })

    .state('containerTransfReceita.transferenciaReceita', {
        url: '/transferenciaReceita',
        templateUrl: 'components/transfReceita/transfReceita.html',
        controller: 'transfReceitaCtrl as ct'
    });
    
}]);
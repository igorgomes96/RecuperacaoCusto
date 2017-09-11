angular.module('recCustoApp').service('importacoesAPI', ['$http', 'config', 'sharedDataService', function($http, config, sharedDataService) {

    var self = this;
    var resource = 'Importacoes';

    self.uploadFile = function(form) {
    	var user = sharedDataService.getUsuario();
    	var token = null;
    	if (user)
    		token = user.Token;

    	var settings = {
		  "async": true,
		  "crossDomain": true,
		  "url": config.baseUrl + 'Importacoes/Upload',
		  "method": "POST",
		  "headers": {
		  	"authorization": "Basic " + token,
		    "cache-control": "no-cache"
		  },
		  "processData": false,
		  "contentType": false,
		  "mimeType": "multipart/form-data",
		  "data": form
		}

		return $.ajax(settings);
    }

}]);
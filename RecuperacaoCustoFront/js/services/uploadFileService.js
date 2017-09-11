angular.module('recCustoApp').service('uploadFileService', ['messagesService', 'importacoesAPI', 'sharedDataService', function(messagesService, importacoesAPI, sharedDataService) {

	var self = this;

	self.sendFile = function() {

		var form = new FormData();
		form.append("arquivo", $('input[type=file]')[0].files[0]);
		form.append("ciclo", sharedDataService.getCicloAtual().Codigo);

		return importacoesAPI.uploadFile(form);

	}

}]);
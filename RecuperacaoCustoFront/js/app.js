angular.module('recCustoApp', ['ui.router', 'uiInputSiblings']);

//Verifica se está autenticado
angular.module('recCustoApp').run(function() {

	if (!window.sessionStorage.getItem('user')) {
		window.location.replace('components/autenticacao/autenticacao.html');
	}

});

//Loader.css
var exibeLoader = function() {
    $('body').append('<div class="loader"><div class="ball-scale-multiple"><div></div><div></div><div></div></div></div>');
}
var ocultaLoader = function() {
    $('.loader').remove();
}


$(document).ready(function() {

	//Listener para alteração de arquivos em input files.
	//Exibe caixa de texto com nome do arquivo e botão de upload quando é escolhido algum arquivo, ou oculta os mesmos se nenhum arquivo é escolhido
	$(document).on('change', ':file', function() {
	    var input = $(this);
	    var numFiles = input.get(0).files ? input.get(0).files.length : 1;
	    if (!numFiles) {
	        $("#input-files-label").hide();
	        $("#button-upload-file").hide();
	    }
	    else {
	        $("#input-files-label").show();
	        $("#button-upload-file").show();
	        $("#input-files-label").val(input[0].files[0].name);
	    }
	});

});
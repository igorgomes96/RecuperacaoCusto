using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class CategoriaCRDTO
    {
        [Required(ErrorMessage = "A categoria do CR deve ser informada!")]
        [StringLength(5, ErrorMessage = "A categoria do CR deve ter entre 3 e 5 caracteres!", MinimumLength = 3)]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O prefixo da conta do CR deve ser informado!")]
        [StringLength(5, ErrorMessage = "O prefixo da conta do CR deve ter entre 3 e 5 caracteres!", MinimumLength = 3)]
        public string Numero { get; set; }
    }
}
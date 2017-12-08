using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class CRDTO
    {
        
        [Required(ErrorMessage = "O Código do CR deve ser informado!")]
        [StringLength(9, ErrorMessage = "O Código do CR deve possuir 9 caracteres!", MinimumLength = 9)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O Código do CR deve ser informado!")]
        [StringLength(255, ErrorMessage = "O Código do CR pode possuir no máximo 255 caracteres!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O Responsável pelo CR deve ser informado!")]
        public string ResponsavelLogin { get; set; }

        public string ResponsavelNome { get; set; }

        [Required(ErrorMessage = "A categoria do CR deve ser informada!")]
        public string Categoria { get; set; }

    }
}
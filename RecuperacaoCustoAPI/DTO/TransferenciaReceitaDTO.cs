using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class TransferenciaReceitaDTO
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "CR de Origem não informado!")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "O CR deve ser composta somente por caraceteres numéricos!")]
        [MaxLength(20, ErrorMessage = "Quantidade máxima de caracteres (20) ultrapassada para o CR de Origem!")]
        public string CROrigem { get; set; }

        [Required(ErrorMessage = "CR de Destino não informado!")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "O CR deve ser composta somente por caraceteres numéricos!")]
        [MaxLength(20, ErrorMessage = "Quantidade máxima de caracteres (20) ultrapassada para o CR de Destino!")]
        public string CRDestino { get; set; }

        [Required(ErrorMessage = "Regime de Tributação não informado!")]
        [MaxLength(50, ErrorMessage = "Quantidade máxima de caracteres (50) ultrapassada para o Regime de Tributação!")]
        public string RegimeTributacao { get; set; }

        public float ISS { get; set; }

        public float Valor { get; set; }

        [Required(ErrorMessage = "Intercompany não informado!")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "O Intercompany deve ter 3 caracteres!")]
        public string Intercompany { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "A NF deve ser composta somente por caraceteres numéricos!")]
        [MaxLength(10, ErrorMessage = "Quantidade máxima de caracteres (10) ultrapassada para o Regime de Tributação!")]
        public string NF { get; set; }

        public System.DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "Razão Social não informada!")]
        [MaxLength(20, ErrorMessage = "Quantidade máxima de caracteres (20) ultrapassada para a Razão Social!")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Histórico não informado!")]
        [MaxLength(150, ErrorMessage = "Quantidade máxima de caracteres (150) ultrapassada para o Histórico!")]
        public string Historico { get; set; }

        public System.DateTime DataHora { get; set; }
        
        public string LoginUsuario { get; set; }
    }
}
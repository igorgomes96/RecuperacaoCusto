using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Properties;

namespace RecuperacaoCustoAPI.DTO
{
    public class EmailDTO
    {

        public EmailDTO() {
            To = new List<string>();
        }
        public EmailDTO (IEnumerable<string> to, string subject, IEnumerable<RecuperacaoCusto> recuperacoes)
        {
            To = new List<string>();
            To = to;
            Subject = subject;
            string tabela = "";
            foreach (RecuperacaoCusto rec in recuperacoes)
            {
                string temp = string.Format(Resources.Recuperacoes, rec.CROrigem, rec.CRDestino, rec.TipoRecuperacao.Tipo, rec.Motivo);
                tabela += temp;
            }
            Message = string.Format(Resources.Email, tabela);
        }

        public EmailDTO(IEnumerable<string> to, string subject, RecuperacaoCusto recuperacao)
        {
            To = new List<string>();
            To = to;
            Subject = subject;
            string tabela = string.Format(Resources.Recuperacoes, recuperacao.CROrigem, recuperacao.CRDestino, recuperacao.TipoRecuperacao.Tipo, recuperacao.Motivo);
            Message = string.Format(Resources.Email, tabela);
        }

        public IEnumerable<string> To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
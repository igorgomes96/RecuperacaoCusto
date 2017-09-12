using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.DTO
{
    public class RecuperacoesCicloDTO
    {
        public RecuperacoesCicloDTO () {
            Recuperacoes = new Dictionary<int, float>();
        }

        public IEnumerable<RecuperacaoCustoMes> GetRecuperacoesMensais()
        {
            List<RecuperacaoCustoMes> retorno = new List<RecuperacaoCustoMes>();
            foreach (int key in Recuperacoes.Keys)
            {
                retorno.Add(new RecuperacaoCustoMes
                {
                    CodMesCiclo = key,
                    CodRecuperacao = Codigo,
                    Valor = Recuperacoes[key]
                });
            }
            return retorno;
        }

        public RecuperacaoCusto GetRecuperacaoCusto()
        {
            RecuperacaoCusto rec = new RecuperacaoCusto
            {
                Codigo = Codigo,
                CROrigem = CROrigem,
                CRDestino = CRDestino,
                DataHora = DataHora,
                Aprovado = Aprovado,
                Resposta = Resposta,
                DataHoraResposta = DataHoraResposta,
                TipoRecuperacaoCod = TipoRecuperacaoCod,
                Motivo = Motivo,
                CodCiclo = CodCiclo,
                LoginEnvio = LoginEnvio
            };
            return rec;
        }

        public RecuperacoesCicloDTO(RecuperacaoCusto rec, Ciclo ciclo)
        {
            Contexto db = new Contexto();
            if (rec == null || ciclo == null || rec.Origem == null || rec.Destino == null || rec.RecuperacoesMensais == null)
                return;

            Codigo = rec.Codigo;
            CROrigem = rec.CROrigem;
            DescricaoCROrigem = rec.Origem.Descricao;
            EmailOrigem = rec.Origem.Responsavel.Email;
            ResponsavelOrigem = rec.Origem.Responsavel.Nome;
            CRDestino = rec.CRDestino;
            DescricaoCRDestino = rec.Destino.Descricao;
            ResponsavelDestino = rec.Destino.Responsavel.Nome;
            EmailDestino = rec.Destino.Responsavel.Email;
            DataHora = rec.DataHora;
            Aprovado = rec.Aprovado;
            Resposta = rec.Resposta;
            DataHoraResposta = rec.DataHoraResposta;
            TipoRecuperacaoCod = rec.TipoRecuperacaoCod;
            Tipo = rec.TipoRecuperacao.Tipo;
            CodCiclo = rec.CodCiclo;
            if (rec.Usuario != null) {
                NomeEnvio = rec.Usuario.Nome;
            }
            
            if (rec.TipoRecuperacao != null)
            {
                Tipo = rec.TipoRecuperacao.Tipo;
                Conta = rec.TipoRecuperacao.Conta;
            }
            Motivo = rec.Motivo;

            Recuperacoes = new Dictionary<int, float>();
            foreach (MesCiclo m in ciclo.MesesCiclos)
            {
                RecuperacaoCustoMes r = db.RecuperacaoCustoMes.Find(rec.Codigo, m.Codigo);

                if (r == null)
                {
                    Recuperacoes.Add(m.Codigo, 0);
                } else
                {
                    Recuperacoes.Add(m.Codigo, r.Valor);
                }
            }

        }

        public int Codigo { get; set; }
        public string CROrigem { get; set; }
        public string DescricaoCROrigem { get; set; }
        public string EmailOrigem { get; set; }
        public string ResponsavelOrigem { get; set; }
        public string CRDestino { get; set; }
        public string DescricaoCRDestino { get; set; }
        public string ResponsavelDestino { get; set; }
        public string EmailDestino { get; set; }
        public System.DateTime DataHora { get; set; }
        public Nullable<bool> Aprovado { get; set; }
        public string Resposta { get; set; }
        public Nullable<System.DateTime> DataHoraResposta { get; set; }
        public int TipoRecuperacaoCod { get; set; }
        public string Tipo { get; set; }
        public string Conta { get; set; }
        public string Motivo { get; set; }
        public int CodCiclo { get; set; }
        public string LoginEnvio { get; set; }
        public string NomeEnvio { get; set; }
        public IDictionary<int, float> Recuperacoes { get; set; }  //CodMes, Valor Recuperado
        
    }
}
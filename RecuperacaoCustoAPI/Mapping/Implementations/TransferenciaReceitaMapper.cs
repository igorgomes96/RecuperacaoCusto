using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Mapping.Implementations
{
    public class TransferenciaReceitaMapper : ISingleMapper<TransferenciaReceita, TransferenciaReceitaDTO>
    {

        public TransferenciaReceitaDTO Map(TransferenciaReceita source)
        {
            return new TransferenciaReceitaDTO
            {
                Codigo = source.Codigo,
                CROrigem = source.CROrigem,
                CRDestino = source.CRDestino,
                RegimeTributacao = source.RegimeTributacao,
                ISS = source.ISS,
                CPRB = source.CPRB,
                Valor = source.Valor,
                Intercompany = source.Intercompany,
                NF = source.NF,
                DataEmissao = source.DataEmissao,
                RazaoSocial = source.RazaoSocial,
                Historico = source.Historico,
                DataHora = source.DataHora,
                LoginUsuario = source.LoginUsuario
            };
        }

        public TransferenciaReceita Map(TransferenciaReceitaDTO destination)
        {
            return new TransferenciaReceita
            {
                Codigo = destination.Codigo,
                CROrigem = destination.CROrigem,
                CRDestino = destination.CRDestino,
                RegimeTributacao = destination.RegimeTributacao,
                ISS = destination.ISS,
                CPRB = destination.CPRB,
                Valor = destination.Valor,
                Intercompany = destination.Intercompany,
                NF = destination.NF,
                DataEmissao = destination.DataEmissao,
                RazaoSocial = destination.RazaoSocial,
                Historico = destination.Historico,
                DataHora = destination.DataHora,
                LoginUsuario = destination.LoginUsuario
            };
        }
    }
}
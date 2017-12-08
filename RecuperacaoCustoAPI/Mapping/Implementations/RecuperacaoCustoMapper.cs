using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Mapping.Implementations
{
    public class RecuperacaoCustoMapper : ISingleMapper<RecuperacaoCusto, RecuperacaoCustoDTO>
    {
        public RecuperacaoCustoDTO Map(RecuperacaoCusto source)
        {
            return source == null ? null : new RecuperacaoCustoDTO
            {
                CRDestino = source.CRDestino,
                CROrigem = source.CROrigem,
                DataHora = source.DataHora,
                Aprovado = source.Aprovado,
                Resposta = source.Resposta,
                DataHoraResposta = source.DataHoraResposta,
                TipoRecuperacaoCod = source.TipoRecuperacaoCod,
                Motivo = source.Motivo,
                CodCiclo = source.CodCiclo,
                LoginEnvio = source.LoginEnvio,
                NomeEnvio = source.Usuario?.Nome,
                Tipo = source.TipoRecuperacao?.Tipo,
                Conta = source.TipoRecuperacao?.Conta
            };
        }

        public RecuperacaoCusto Map(RecuperacaoCustoDTO destination)
        {
            return destination == null ? null : new RecuperacaoCusto
            {
                CRDestino = destination.CRDestino,
                CROrigem = destination.CROrigem,
                DataHora = destination.DataHora,
                Aprovado = destination.Aprovado,
                Resposta = destination.Resposta,
                DataHoraResposta = destination.DataHoraResposta,
                TipoRecuperacaoCod = destination.TipoRecuperacaoCod,
                Motivo = destination.Motivo,
                CodCiclo = destination.CodCiclo,
                LoginEnvio = destination.LoginEnvio
            };
        }
    }
}
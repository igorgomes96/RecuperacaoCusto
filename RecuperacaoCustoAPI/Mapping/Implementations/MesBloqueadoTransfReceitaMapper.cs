using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Mapping.Implementations
{
    public class MesBloqueadoTransfReceitaMapper : ISingleMapper<MesBloqueadoTransfReceita, MesBloqueadoTransfReceitaDTO>
    {
        public MesBloqueadoTransfReceitaDTO Map(MesBloqueadoTransfReceita source)
        {
            return source == null ? null : new MesBloqueadoTransfReceitaDTO { Mes = source.Mes };
        }

        public MesBloqueadoTransfReceita Map(MesBloqueadoTransfReceitaDTO destination)
        {
            return destination == null ? null : new MesBloqueadoTransfReceita { Mes = destination.Mes };
        }
    }
}
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Mapping.Implementations
{
    public class MesCicloMapper : ISingleMapper<MesCiclo, MesCicloDTO>
    {
        public MesCicloDTO Map(MesCiclo source)
        {
            return source == null ? null : new MesCicloDTO
            {
                Codigo = source.Codigo,
                Mes = source.Mes,
                CodCiclo = source.CodCiclo,
            };
        }

        public MesCiclo Map(MesCicloDTO destination)
        {
            return destination == null ? null : new MesCiclo
            {
                Codigo = destination.Codigo,
                Mes = destination.Mes,
                CodCiclo = destination.CodCiclo,
            };
        }
    }
}
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Mapping.Implementations
{
    public class CategoriaCRMapper : ISingleMapper<CategoriaCR, CategoriaCRDTO>
    {
        public CategoriaCRDTO Map(CategoriaCR source)
        {
            return source == null ? null : new CategoriaCRDTO
            {
                Categoria = source.Categoria,
                Numero = source.Numero
            };
        }

        public CategoriaCR Map(CategoriaCRDTO destination)
        {
            return destination == null ? null : new CategoriaCR
            {
                Categoria = destination.Categoria,
                Numero = destination.Numero
            };
        }
    }
}
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Mapping.Interfaces;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecuperacaoCustoAPI.Mapping.Implementations
{
    public class CRMapper : ISingleMapper<CR, CRDTO>
    {
        public CRDTO Map(CR source)
        {
            return new CRDTO
            {
                Codigo = source.Codigo,
                Descricao = source.Descricao,
                ResponsavelLogin = source.ResponsavelLogin,
                Categoria = source.Categoria,
                ResponsavelNome = source.Responsavel?.Nome
            };
        }

        public CR Map(CRDTO destination)
        {
            return new CR
            {
                Codigo = destination.Codigo,
                Descricao = destination.Descricao,
                ResponsavelLogin = destination.ResponsavelLogin,
                Categoria = destination.Categoria
            };
        }
    }
}
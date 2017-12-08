using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Filters;

namespace RecuperacaoCustoAPI.Controllers
{
    [AuthenticationFilter]
    public class TiposRecuperacoesController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/TiposRecuperacoes
        public IEnumerable<TipoRecuperacaoDTO> GetTipoRecuperacao()
        {
            return db.TipoRecuperacao.ToList()
                .Select(x => new TipoRecuperacaoDTO(x));
        }

    }
}
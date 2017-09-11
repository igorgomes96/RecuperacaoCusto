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

        /*// GET: api/TiposRecuperacoes/5
        [ResponseType(typeof(TipoRecuperacao))]
        public IHttpActionResult GetTipoRecuperacao(int id)
        {
            TipoRecuperacao tipoRecuperacao = db.TipoRecuperacao.Find(id);
            if (tipoRecuperacao == null)
            {
                return NotFound();
            }

            return Ok(tipoRecuperacao);
        }

        // PUT: api/TiposRecuperacoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoRecuperacao(int id, TipoRecuperacao tipoRecuperacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoRecuperacao.Codigo)
            {
                return BadRequest();
            }

            db.Entry(tipoRecuperacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoRecuperacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TiposRecuperacoes
        [ResponseType(typeof(TipoRecuperacao))]
        public IHttpActionResult PostTipoRecuperacao(TipoRecuperacao tipoRecuperacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoRecuperacao.Add(tipoRecuperacao);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoRecuperacao.Codigo }, tipoRecuperacao);
        }

        // DELETE: api/TiposRecuperacoes/5
        [ResponseType(typeof(TipoRecuperacao))]
        public IHttpActionResult DeleteTipoRecuperacao(int id)
        {
            TipoRecuperacao tipoRecuperacao = db.TipoRecuperacao.Find(id);
            if (tipoRecuperacao == null)
            {
                return NotFound();
            }

            db.TipoRecuperacao.Remove(tipoRecuperacao);
            db.SaveChanges();

            return Ok(tipoRecuperacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoRecuperacaoExists(int id)
        {
            return db.TipoRecuperacao.Count(e => e.Codigo == id) > 0;
        }*/
    }
}
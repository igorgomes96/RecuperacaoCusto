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
using System.Data.Entity.Migrations;

namespace RecuperacaoCustoAPI.Controllers
{
    public class RecuperacoesCustosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/RecuperacoesCustos
        public IEnumerable<RecuperacaoCustoDTO> GetRecuperacaoCusto(string crOrigem = null, string crDestino = null, bool? respondido = null, bool? aprovado = null)
        {
            return db.RecuperacaoCusto.ToList()
                .Where(x => (crOrigem == null || x.CROrigem == crOrigem) && (crDestino == null || x.CRDestino == crDestino)
                && (respondido == null || ((respondido == true && x.Aprovado != null) || (respondido == false && x.Aprovado == null))) 
                && (aprovado == null || x.Aprovado == aprovado))
                .Select(x => new RecuperacaoCustoDTO(x));
        }

        [HttpGet]
        [Route("api/RecuperacoesCustos/PorCiclo/{crOrigem}/{codCiclo}")]
        [ResponseType(typeof(IEnumerable<RecuperacoesCicloDTO>))]
        public IHttpActionResult GetRecuperacoesCustosPorCiclo (string crOrigem, int codCiclo)
        {
            CR cr = db.CR.Find(crOrigem);
            if (cr == null) return NotFound();

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            IEnumerable<RecuperacoesCicloDTO> retorno = new List<RecuperacoesCicloDTO>();

            foreach (RecuperacaoCusto rec in db.RecuperacaoCusto.Where(x => x.CROrigem == crOrigem && x.CodCiclo == codCiclo))
            {
                ((List<RecuperacoesCicloDTO>)retorno).Add(new RecuperacoesCicloDTO(rec, ciclo));
            }

            return Ok(retorno);

        }

        [HttpPost]
        [Route("api/RecuperacoesCustos/PorCiclo")]
        [ResponseType(typeof(IEnumerable<RecuperacoesCicloDTO>))]
        public IHttpActionResult PostRecuperacoesCustoPorCiclo (IEnumerable<RecuperacoesCicloDTO> recuperacoes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (RecuperacoesCicloDTO rec in recuperacoes)
            {
                db.RecuperacaoCusto.AddOrUpdate(rec.GetRecuperacaoCusto());

                foreach (RecuperacaoCustoMes rm in rec.GetRecuperacoesMensais())
                {
                    if (rm.Valor == 0)
                    {
                        if (RecupercaoCustoMesExists(rm.CodRecuperacao, rm.CodMesCiclo))
                            db.Entry(rm).State = EntityState.Deleted;
                    }
                    else
                        db.RecuperacaoCustoMes.AddOrUpdate(rm);
                }
            }

            try
            {
                db.SaveChanges();
            } catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();

        }

        // GET: api/RecuperacoesCustos/5
        [ResponseType(typeof(RecuperacaoCustoDTO))]
        public IHttpActionResult GetRecuperacaoCusto(int id)
        {
            RecuperacaoCusto recuperacaoCusto = db.RecuperacaoCusto.Find(id);
            if (recuperacaoCusto == null)
            {
                return NotFound();
            }

            return Ok(new RecuperacaoCustoDTO(recuperacaoCusto));
        }

        // PUT: api/RecuperacoesCustos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecuperacaoCusto(int id, RecuperacaoCusto recuperacaoCusto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recuperacaoCusto.Codigo)
            {
                return BadRequest();
            }

            db.Entry(recuperacaoCusto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecuperacaoCustoExists(id))
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

        // POST: api/RecuperacoesCustos
        [ResponseType(typeof(RecuperacaoCustoDTO))]
        public IHttpActionResult PostRecuperacaoCusto(RecuperacaoCusto recuperacaoCusto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RecuperacaoCusto.Add(recuperacaoCusto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RecuperacaoCustoExists(recuperacaoCusto.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = recuperacaoCusto.Codigo }, new RecuperacaoCustoDTO(recuperacaoCusto));
        }

        // DELETE: api/RecuperacoesCustos/5
        [ResponseType(typeof(RecuperacaoCustoDTO))]
        public IHttpActionResult DeleteRecuperacaoCusto(int id)
        {
            RecuperacaoCusto recuperacaoCusto = db.RecuperacaoCusto.Find(id);
            if (recuperacaoCusto == null)
            {
                return NotFound();
            }

            RecuperacaoCustoDTO r = new RecuperacaoCustoDTO(recuperacaoCusto);
            db.RecuperacaoCusto.Remove(recuperacaoCusto);
            db.SaveChanges();

            return Ok(r);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecuperacaoCustoExists(int codigo)
        {
            return db.RecuperacaoCusto.Count(e => e.Codigo == codigo) > 0;
        }

        private bool RecupercaoCustoMesExists(int codRecuperacao, int codMes)
        {
            return db.RecuperacaoCustoMes.Count(x => x.CodRecuperacao == codRecuperacao && x.CodMesCiclo == codMes) > 0;
        }
    }
}
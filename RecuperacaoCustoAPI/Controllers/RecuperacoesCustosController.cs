﻿using System;
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
using RecuperacaoCustoAPI.Filters;
using RecuperacaoCustoAPI.Service;

namespace RecuperacaoCustoAPI.Controllers
{
    [AuthenticationFilter]
    public class RecuperacoesCustosController : ApiController
    {
        private Contexto db = new Contexto();


        [HttpGet]
        [Route("api/RecuperacoesCustos/PorCiclo/{codCiclo}")]
        [ResponseType(typeof(IEnumerable<RecuperacoesCicloDTO>))]
        public IHttpActionResult GetRecuperacoesPorCiclo (int codCiclo, string crOrigem = null, string crDestino = null, int? codTipo = null, bool? respondido = null, bool? aprovado = null)
        {
            Ciclo c = db.Ciclo.Find(codCiclo);
            if (c == null) return NotFound();

            return Ok(db.RecuperacaoCusto.ToList()
                .Where(x => (crOrigem == null || x.CROrigem == crOrigem) && (crDestino == null || x.CRDestino == crDestino) && (!codTipo.HasValue || x.TipoRecuperacaoCod == codTipo)
                && (respondido == null || ((respondido == true && x.Aprovado != null) || (respondido == false && x.Aprovado == null)))
                && (aprovado == null || x.Aprovado == aprovado))
                .Select(x => new RecuperacoesCicloDTO(x, c))
                .OrderBy(x => x.DataHora).OrderBy(x => x.CRDestino).OrderBy(x => x.CROrigem));
        }

        [HttpGet]
        [Route("api/RecuperacoesCustos/PorCiclo/Enviadas/{codCiclo}")]
        [ResponseType(typeof(IEnumerable<RecuperacoesCicloDTO>))]
        public IHttpActionResult GetRecuperacoesCustosEnviadasPorCiclo(int codCiclo, bool? respondido = null, bool? aprovado = null)
        {

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            IEnumerable<RecuperacoesCicloDTO> retorno = new List<RecuperacoesCicloDTO>();

            IEnumerable<RecuperacaoCusto> lista = db.RecuperacaoCusto.Where(x => (aprovado == null || x.Aprovado == aprovado) && (!respondido.HasValue || respondido.Value ? x.Aprovado != null : x.Aprovado == null) && (x.CodCiclo == codCiclo));
            if (!User.IsInRole("Administrador"))
                lista = lista.Where(x => ((CustomIdentity)User.Identity).Usuario.CRs.Count(y => y.Codigo == x.CROrigem) > 0);

            foreach (RecuperacaoCusto rec in lista)
            {
                ((List<RecuperacoesCicloDTO>)retorno).Add(new RecuperacoesCicloDTO(rec, ciclo));
            }

            return Ok(retorno);

        }

        [HttpGet]
        [Route("api/RecuperacoesCustos/PorCiclo/Recebidas/{codCiclo}")]
        [ResponseType(typeof(IEnumerable<RecuperacoesCicloDTO>))]
        public IHttpActionResult GetRecuperacoesCustosRecebidasPorCiclo(int codCiclo, bool? respondido = null, bool? aprovado = null)
        {

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            IEnumerable<RecuperacoesCicloDTO> retorno = new List<RecuperacoesCicloDTO>();

            IEnumerable<RecuperacaoCusto> lista = db.RecuperacaoCusto.Where(x => (aprovado == null || x.Aprovado == aprovado) && (!respondido.HasValue || respondido.Value ? x.Aprovado != null : x.Aprovado == null) && (x.CodCiclo == codCiclo));
            if (!User.IsInRole("Administrador"))
                lista = lista.Where(x => ((CustomIdentity)User.Identity).Usuario.CRs.Count(y => y.Codigo == x.CRDestino) > 0);


            foreach (RecuperacaoCusto rec in lista)
            {
                ((List<RecuperacoesCicloDTO>)retorno).Add(new RecuperacoesCicloDTO(rec, ciclo));
            }

            return Ok(retorno);

        }

        [HttpGet]
        [Route("api/RecuperacoesCustos/PorCiclo/PorCREnvio/{crOrigem}/{codCiclo}")]
        [ResponseType(typeof(IEnumerable<RecuperacoesCicloDTO>))]
        public IHttpActionResult GetRecuperacoesCustosPorCicloPorCREnvio (string crOrigem, int codCiclo)
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

        [HttpGet]
        [Route("api/RecuperacoesCustos/PorCiclo/PorCRDestino/{crDestino}/{codCiclo}")]
        [ResponseType(typeof(IEnumerable<RecuperacoesCicloDTO>))]
        public IHttpActionResult GetRecuperacoesCustosPorCicloPorCRDestino(string crDestino, int codCiclo)
        {
            CR cr = db.CR.Find(crDestino);
            if (cr == null) return NotFound();

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            IEnumerable<RecuperacoesCicloDTO> retorno = new List<RecuperacoesCicloDTO>();

            foreach (RecuperacaoCusto rec in db.RecuperacaoCusto.Where(x => x.CRDestino == crDestino && x.CodCiclo == codCiclo))
            {
                ((List<RecuperacoesCicloDTO>)retorno).Add(new RecuperacoesCicloDTO(rec, ciclo));
            }

            return Ok(retorno);

        }

        [HttpPut]
        [Route("api/RecuperacoesCustos/Reprovar/{codRec}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ReprovarRecuperacao(int codRec, RecuperacaoCusto rec)
        {
            RecuperacaoCusto r = db.RecuperacaoCusto.Find(codRec);
            if (r == null)
                return NotFound();

            r.Aprovado = false;
            r.DataHoraResposta = DateTime.Now;
            r.Resposta = rec.Resposta;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/RecuperacoesCustos/Aprovar/{codRec}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult AprovarRecuperacao(int codRec)
        {
            RecuperacaoCusto r = db.RecuperacaoCusto.Find(codRec);
            if (r == null)
                return NotFound();

            r.Aprovado = true;
            r.DataHoraResposta = DateTime.Now; 

            try
            {
                db.SaveChanges();
            } catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/RecuperacoesCustos/PorCiclo")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PostRecuperacoesCustoPorCiclo (RecuperacoesCicloDTO rec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.CR.Find(rec.CRDestino) == null) return NotFound();

            RecuperacaoCusto r = rec.GetRecuperacaoCusto();
            db.RecuperacaoCusto.AddOrUpdate(r);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            foreach (RecuperacaoCustoMes rm in rec.GetRecuperacoesMensais())
            {
                rm.CodRecuperacao = r.Codigo;
                if (rm.Valor == 0)
                {
                    if (RecupercaoCustoMesExists(rm.CodRecuperacao, rm.CodMesCiclo))
                        db.Entry(rm).State = EntityState.Deleted;
                }
                else
                    db.RecuperacaoCustoMes.AddOrUpdate(rm);
            }

            try
            {
                db.Entry(r).State = EntityState.Detached;
                r = db.RecuperacaoCusto.Find(r.Codigo);
                db.SaveChanges();
                SendEmail.Send(new EmailDTO(new [] { r.Destino.Responsavel.Email }, "Recuperações de Custos", r));
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
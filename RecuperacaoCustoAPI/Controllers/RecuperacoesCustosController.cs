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
using RecuperacaoCustoAPI.Services.Interfaces;
using RecuperacaoCustoAPI.Exceptions;
using System.Threading;

namespace RecuperacaoCustoAPI.Controllers
{
    [AuthenticationFilter]
    public class RecuperacoesCustosController : ApiController
    {
        private Contexto db = new Contexto();

        private readonly IRecuperacoesService _recService;

        public RecuperacoesCustosController(IRecuperacoesService recService)
        {
            _recService = recService;
        }

        [ResponseType(typeof(RecuperacoesCicloDTO))]
        public IHttpActionResult DeleteTransferencia(int id)
        {
            try
            {
                return Ok(_recService.CancelarRecuperacao(id));
            } catch (NaoEncontradoException<RecuperacaoCusto>)
            {
                return Content(HttpStatusCode.NotFound, "Cód. de Recuperação não encontrado!");
            }
        }

        [HttpGet]
        [Route("api/RecuperacoesCustos/PorCiclo/{codCiclo}")]
        [ResponseType(typeof(IEnumerable<RecuperacoesCicloDTO>))]
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult GetRecuperacoesPorCiclo(int codCiclo, string crOrigem = null, string crDestino = null, int? codTipo = null, bool? respondido = null, bool? aprovado = null)
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

            IEnumerable<RecuperacaoCusto> lista = db.RecuperacaoCusto
                .Where(x => (aprovado == null || x.Aprovado == aprovado) && (!respondido.HasValue || (respondido.Value ? x.Aprovado != null : x.Aprovado == null)) && (x.CodCiclo == codCiclo));
            if (User.IsInRole("Gestor"))
                lista = lista.Where(x => (((CustomIdentity)User.Identity).Usuario.CRs.Count(y => y.Codigo == x.CROrigem) > 0) || x.LoginEnvio == ((CustomIdentity)User.Identity).Usuario.Login);
            else if (User.IsInRole("Usuário"))
                lista = lista.Where(x => x.LoginEnvio == ((CustomIdentity)User.Identity).Usuario.Login);

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

            IEnumerable<RecuperacaoCusto> lista = db.RecuperacaoCusto
                .Where(x => (aprovado == null || x.Aprovado == aprovado) && (!respondido.HasValue || (respondido.Value ? x.Aprovado != null : x.Aprovado == null)) && (x.CodCiclo == codCiclo));
            if (User.IsInRole("Gestor"))
                lista = lista.Where(x => ((CustomIdentity)User.Identity).Usuario.CRs.Count(y => y.Codigo == x.CRDestino) > 0);
            else if (User.IsInRole("Usuário"))
                lista = lista.Where(x => x.LoginEnvio == ((CustomIdentity)User.Identity).Usuario.Login);

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

            Usuario user = ((CustomIdentity)User.Identity).Usuario;

            IEnumerable<RecuperacoesCicloDTO> retorno = new List<RecuperacoesCicloDTO>();
            IEnumerable<RecuperacaoCusto> lista = db.RecuperacaoCusto.Where(x => x.CROrigem == crOrigem && x.CodCiclo == codCiclo); ;

            if (User.IsInRole("Usuário"))
                lista = lista.Where(x => x.LoginEnvio == user.Login);
            else if (User.IsInRole("Gestor"))
            {
                if (cr.ResponsavelLogin != user.Login)
                    lista = lista.Where(x => x.Destino.ResponsavelLogin == user.Login);
            }

            foreach (RecuperacaoCusto rec in lista)
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

            Usuario user = ((CustomIdentity)User.Identity).Usuario;

            IEnumerable<RecuperacoesCicloDTO> retorno = new List<RecuperacoesCicloDTO>();
            IEnumerable<RecuperacaoCusto> lista = db.RecuperacaoCusto.Where(x => x.CRDestino == crDestino && x.CodCiclo == codCiclo);

            if (User.IsInRole("Usuário"))
                lista = lista.Where(x => x.LoginEnvio == user.Login);
            else if (User.IsInRole("Gestor"))
            {
                if (cr.ResponsavelLogin != user.Login)
                    lista = lista.Where(x => x.Origem.ResponsavelLogin == user.Login);
            }
                

            foreach (RecuperacaoCusto rec in lista)
            {
                ((List<RecuperacoesCicloDTO>)retorno).Add(new RecuperacoesCicloDTO(rec, ciclo));
            }

            return Ok(retorno);

        }

        [HttpPut]
        [Route("api/RecuperacoesCustos/Reprovar/{codRec}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Gestor")]
        public IHttpActionResult ReprovarRecuperacao(int codRec, RecuperacaoCusto rec)
        {
            Usuario user = ((CustomIdentity)User.Identity).Usuario;

            RecuperacaoCusto r = db.RecuperacaoCusto.Find(codRec);
            if (r == null)
                return NotFound();

            CR cr = db.CR.Find(rec.CRDestino);
            if (cr.ResponsavelLogin != user.Login)
                return Content(HttpStatusCode.Forbidden, "O CR de destino (" + cr.Codigo + ") não está abaixo de sua gestão!");

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
        [Authorize(Roles = "Gestor")]
        public IHttpActionResult AprovarRecuperacao(int codRec)
        {
            Usuario user = ((CustomIdentity)User.Identity).Usuario;

            RecuperacaoCusto r = db.RecuperacaoCusto.Find(codRec);
            if (r == null)
                return NotFound();

            CR cr = db.CR.Find(r.CRDestino);
            if (cr.ResponsavelLogin != user.Login)
                return Content(HttpStatusCode.Forbidden, "O CR de destino (" + cr.Codigo + ") não está abaixo de sua gestão!");

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
            Usuario user = ((CustomIdentity)User.Identity).Usuario;
            rec.LoginEnvio = user.Login;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.CR.Find(rec.CROrigem) == null) return Content(HttpStatusCode.NotFound, "CR de Origem não encontrado!");
            if (db.CR.Find(rec.CRDestino) == null) return Content(HttpStatusCode.NotFound, "CR de Destino não encontrado!");
            

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
                Thread t = new Thread(SendEmail.Send);
                t.Start(new EmailDTO(new[] { r.Destino.Responsavel.Email }, "Recuperações de Custos", r));
            } catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();

        }

        [HttpGet]
        [ResponseType(typeof(int))]
        [Route("api/RecuperacoesCustos/QtdaAprovacoesPendentes/{codCiclo}")]
        [Authorize(Roles = "Gestor")]
        public IHttpActionResult QtdaAprovacoesPendentes(int codCiclo)
        {
            Usuario user = ((CustomIdentity)User.Identity).Usuario;

            Ciclo ciclo = db.Ciclo.Find(codCiclo);
            if (ciclo == null) return NotFound();

            return Ok(db.RecuperacaoCusto
                .Count(x => x.Aprovado == null && x.CodCiclo == codCiclo && x.Destino.ResponsavelLogin == user.Login));

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
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

namespace RecuperacaoCustoAPI.Controllers
{
    public class CiclosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Ciclos
        public IEnumerable<CicloDTO> GetCiclo()
        {
            return db.Ciclo.ToList()
                .Select(x => new CicloDTO(x));
        }

        // GET: api/Ciclos/5
        [ResponseType(typeof(CicloDTO))]
        public IHttpActionResult GetCiclo(int id)
        {
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return NotFound();
            }

            return Ok(new CicloDTO(ciclo));
        }

        // PUT: api/Ciclos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCiclo(int id, Ciclo ciclo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ciclo.Codigo)
            {
                return BadRequest();
            }

            Ciclo c = db.Ciclo.Find(id);

            if (c == null) return NotFound();

            if (c.DataFim != ciclo.DataFim || c.DataInicio != ciclo.DataInicio)
                return BadRequest("Data início e data fim não podem ser alterados!");

            db.Entry(ciclo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ciclos
        [ResponseType(typeof(CicloDTO))]
        public IHttpActionResult PostCiclo(Ciclo ciclo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ciclo.Add(ciclo);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            DateTime pivo = new DateTime(ciclo.DataInicio.Year, ciclo.DataInicio.Month, 1);
            while (pivo < ciclo.DataFim)
            {
                MesCiclo mes = new MesCiclo
                {
                    Mes = pivo,
                    CodCiclo = ciclo.Codigo
                };
                db.MesCiclo.Add(mes);
                pivo = pivo.AddMonths(1);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return CreatedAtRoute("DefaultApi", new { id = ciclo.Codigo }, new CicloDTO(ciclo));
        }

        // DELETE: api/Ciclos/5
        [ResponseType(typeof(CicloDTO))]
        public IHttpActionResult DeleteCiclo(int id)
        {
            Ciclo ciclo = db.Ciclo.Find(id);
            if (ciclo == null)
            {
                return NotFound();
            }

            CicloDTO c = new CicloDTO(ciclo);
            db.Ciclo.Remove(ciclo);
            db.SaveChanges();

            return Ok(c);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CicloExists(int id)
        {
            return db.Ciclo.Count(e => e.Codigo == id) > 0;
        }
    }
}
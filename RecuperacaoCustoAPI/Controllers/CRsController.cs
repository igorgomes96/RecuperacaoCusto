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
    public class CRsController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/CRs
        public IEnumerable<CRDTO> GetCRs(string responsavel = null)
        {
            return db.CR.ToList()
                .Where(x => (responsavel == null || x.ResponsavelLogin == responsavel))
                .Select(x => new CRDTO(x));
        }

        // GET: api/CRs/5
        [ResponseType(typeof(CRDTO))]
        public IHttpActionResult GetCR(string id)
        {
            CR cR = db.CR.Find(id);
            if (cR == null)
            {
                return NotFound();
            }

            return Ok(new CRDTO(cR));
        }

        // PUT: api/CRs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCR(string id, CR cR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cR.Codigo)
            {
                return BadRequest();
            }

            db.Entry(cR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CRExists(id))
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

        // POST: api/CRs
        [ResponseType(typeof(CRDTO))]
        public IHttpActionResult PostCR(CR cR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CR.Add(cR);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CRExists(cR.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cR.Codigo }, new CRDTO(cR));
        }

        // DELETE: api/CRs/5
        [ResponseType(typeof(CRDTO))]
        public IHttpActionResult DeleteCR(string id)
        {
            CR cR = db.CR.Find(id);
            if (cR == null)
            {
                return NotFound();
            }

            CRDTO c = new CRDTO(cR);
            db.CR.Remove(cR);
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

        private bool CRExists(string id)
        {
            return db.CR.Count(e => e.Codigo == id) > 0;
        }
    }
}
using RecuperacaoCustoAPI.DTO;
using RecuperacaoCustoAPI.Exceptions;
using RecuperacaoCustoAPI.Filters;
using RecuperacaoCustoAPI.Models;
using RecuperacaoCustoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace RecuperacaoCustoAPI.Controllers
{
    public class TransferenciasReceitasController : ApiController
    {
        private readonly ITransferenciaReceitaService _transferenciaReceitaService;

        public TransferenciasReceitasController(ITransferenciaReceitaService transferenciaReceitaService)
        {
            _transferenciaReceitaService = transferenciaReceitaService;
        }

        [AuthenticationFilter]
        [ResponseType(typeof(IEnumerable<TransferenciaReceitaDTO>))]
        public IHttpActionResult Get(int? ano, int? mes)
        {
            if (User.IsInRole("Administrador"))
                return Ok(_transferenciaReceitaService.List(x => x.DataEmissao.Year == ano && x.DataEmissao.Month == mes));

            return Ok(_transferenciaReceitaService.List(x => x.LoginUsuario == User.Identity.Name && x.DataEmissao.Year == ano && x.DataEmissao.Month == mes));
        }

        [HttpGet]
        [Route("api/TransferenciasReceitas/Report")]
        [AllowAnonymous]
        public HttpResponseMessage Report(DateTime? mes = null)
        {
            if (mes == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            HttpResponseMessage result = null;
            Random rdn = new Random();
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            string FileName = @root + @"/Modelo_Planilha_WEB_Bloco" + rdn.Next() + ".xlsm";
            string Template = @root + @"/Modelo_Planilha_WEB_Bloco.xlsm";
            try
            {
                
                    _transferenciaReceitaService.Report(FileName, Template, mes.Value);

                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(File.ReadAllBytes(FileName));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Modelo_Planilha_WEB_Bloco.xlsm"
                };
                result.Content.Headers.Add("Access-Control-Expose-Headers", "x-filename"); //Sem ele, o header 'x-filename' não aparece
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                //result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.Add("x-filename", "Modelo_Planilha_WEB_Bloco.xlsm");

                return result;

            }
            catch (Exception e)
            {
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                return result;
            } finally
            {
                File.Delete(FileName);
            }

        }

        [AuthenticationFilter]
        [ResponseType(typeof(TransferenciaReceitaDTO))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_transferenciaReceitaService.Find(id));
            } catch (NaoEncontradoException<TransferenciaReceita> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("api/TransferenciasReceitas/Anos")]
        [ResponseType(typeof(ICollection<int>))]
        public IHttpActionResult GetAnos()
        {
            return Ok(_transferenciaReceitaService.GetAnos());
        }

        [AuthenticationFilter]
        [ResponseType(typeof(TransferenciaReceitaDTO))]
        public IHttpActionResult Post(TransferenciaReceitaDTO transf)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, ModelState);

            try
            {
                transf.LoginUsuario = User.Identity.Name;
                return Ok(_transferenciaReceitaService.Save(transf));
            } catch (ConflitoException<TransferenciaReceita> e)
            {
                return Content(HttpStatusCode.Conflict, e.Message);
            }
            catch (NaoEncontradoException<string> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [AuthenticationFilter]
        [ResponseType(typeof(TransferenciaReceitaDTO))]
        public IHttpActionResult Put(int id, TransferenciaReceitaDTO transf)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, ModelState);

            if (transf.Codigo != id)
                return Content(HttpStatusCode.BadRequest, "Inconsistência de parâmetro!");

            try
            {
                return Ok(_transferenciaReceitaService.Update(transf));
            } catch (NaoEncontradoException<TransferenciaReceita> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            } catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [AuthenticationFilter]
        [ResponseType(typeof(TransferenciaReceitaDTO))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(_transferenciaReceitaService.Delete(id));
            }
            catch (NaoEncontradoException<TransferenciaReceita> e)
            {
                return Content(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}

using RecuperacaoCustoAPI.Excel;
using RecuperacaoCustoAPI.Filters;
using RecuperacaoCustoAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RecuperacaoCustoAPI.Controllers
{
    [AuthenticationFilter]
    public class ImportacoesController : ApiController
    {
        private Contexto db = new Contexto();

        [HttpPost]
        [Route("api/Importacoes/Upload")]
        public async Task<HttpResponseMessage> Upload()
        {
            // Verifica se request contém multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            //Diretório App_Data, para salvar o arquivo temporariamente
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            // Lê o arquivo da requisição assincronamente
            await Request.Content.ReadAsMultipartAsync(provider);

            // Para cada arquivo enviado
            foreach (MultipartFileData file in provider.FileData)
            {
                try
                {
                    int codCiclo;
                    if (!int.TryParse(provider.FormData.Get("ciclo"), out codCiclo))
                        return Request.CreateResponse(HttpStatusCode.BadRequest);

                    Ciclo ciclo = db.Ciclo.Find(codCiclo);
                    if (ciclo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Cód de Ciclo informado não existente!");

                    if (ciclo.Status == "Fechado")
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "O ciclo " + codCiclo + " já está fechado!");

                    ImportacaoOleDB imp = new ImportacaoOleDB(file.LocalFileName, ciclo, User);
                    imp.ExecutarImportacao();
                    // Salva as alterações no banco
                    //db.SaveChanges();

                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                finally
                {
                    try
                    {
                        File.Delete(file.LocalFileName);
                    }
                    catch (Exception) { }

                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Dados importados com sucesso!");
        }
    }
}

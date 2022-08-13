using Newtonsoft.Json;
using Renave.Anfir.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Renave.Anfir.Controllers
{
    /// <summary>
    /// Transferir estoque para ITE solicitante.
    /// </summary>
    public class TransferenciasParaIteController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] TransferenciaEstoqueParaIteSolicitacao solicitacao)
        {
            try
            {
                var url = basePath + "/api/ite/transferencias-para-ite";

                if (solicitacao.ID_Empresa == null || solicitacao.ID_Empresa == string.Empty)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "ID_Empresa não cadastrada.");

                var certificadoFileName = "planalto_industria_2022.pfx";
                var certificadoPassword = "123456789";

                var pathCert = HttpContext.Current.Server.MapPath("~/") + @"certificados\" + certificadoFileName;

                var certificate = new X509Certificate2(pathCert, certificadoPassword);
                var handler = new HttpClientHandler();
                handler.ClientCertificates.Add(certificate);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(solicitacao);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(url),
                        Headers = {
                        { "Accept", "application/json" },
                    },
                        Content = new StringContent(json)
                        {
                            Headers = {
                            ContentType = new MediaTypeHeaderValue("application/json")
                            }
                        }
                    };

                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = response.Content.ReadAsStringAsync();
                            var retorno = JsonConvert.DeserializeObject<TransferenciaEstoqueParaIteRetorno>(jsonString.Result);

                            return Request.CreateResponse(retorno);
                        }
                        else if (response.StatusCode == (HttpStatusCode)422)
                        {
                            var jsonString = response.Content.ReadAsStringAsync();
                            var retorno = JsonConvert.DeserializeObject<ErroRetorno>(jsonString.Result);

                            return Request.CreateResponse((HttpStatusCode)422, retorno);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
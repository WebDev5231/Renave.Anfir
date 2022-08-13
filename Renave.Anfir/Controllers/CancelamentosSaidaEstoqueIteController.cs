using Newtonsoft.Json;
using Renave.Anfir.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Renave.Anfir.Controllers
{
    /// <summary>
    /// Cancelar saída de estoque ITE.
    /// </summary>
    public class CancelamentosSaidaEstoqueIteController : ApiController
    {

        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] CancelamentosSaidaEstoqueIteSolicitacao solicitacao)
        {
            try
            {
                var url = basePath + "/api/ite/cancelamentos-saida-estoque-ite";

                using (var client = new HttpClient())
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
                            var retorno = JsonConvert.DeserializeObject<object>(jsonString.Result);

                            return Request.CreateResponse(retorno);
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
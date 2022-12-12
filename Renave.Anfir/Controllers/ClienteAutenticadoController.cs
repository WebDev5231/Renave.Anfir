using Newtonsoft.Json;
using Renave.Anfir.Business;
using Renave.Anfir.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Renave.Anfir.Controllers
{
    /// <summary>
    /// Consultar os dados do cliente autenticado.
    /// </summary>
    [RoutePrefix("api")]
    public class ClienteAutenticadoController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [HttpGet]
        [Route("ite/cliente-autenticado")]
        public async Task<HttpResponseMessage> Get(int ID_Empresa)
        {
            try
            {
                if (ID_Empresa == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                else
                {
                    var url = basePath + "/api/ite/cliente-autenticado";

                    var certificadoBusiness = new CertificadoBusiness();
                    var handler = certificadoBusiness.GetHandler(ID_Empresa);

                    using (var client = new HttpClient(handler))
                    {
                        var response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = response.Content.ReadAsStringAsync();
                            var retorno = JsonConvert.DeserializeObject<ClienteAutenticado>(jsonString.Result);

                            return Request.CreateResponse(retorno);
                        }
                        else
                        {
                            return Request.CreateResponse(response.StatusCode, response.Content.ReadAsStringAsync());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("montadora/cliente-autenticado")]
        public async Task<HttpResponseMessage> GetMontadoraClienteAutenticado(int ID_Empresa)
        {
            try
            {
                if (ID_Empresa == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                else
                {
                    var url = basePath + "/api/montadora/cliente-autenticado";

                    var certificadoBusiness = new CertificadoBusiness();
                    var handler = certificadoBusiness.GetHandler(ID_Empresa);

                    using (var client = new HttpClient(handler))
                    {
                        var response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = response.Content.ReadAsStringAsync();
                            var retorno = JsonConvert.DeserializeObject<ClienteAutenticado>(jsonString.Result);

                            return Request.CreateResponse(retorno);
                        }
                        else
                        {
                            return Request.CreateResponse(response.StatusCode, response.Content.ReadAsStringAsync());
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
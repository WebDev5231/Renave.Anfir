using Newtonsoft.Json;
using Renave.Anfir.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;

namespace Renave.Anfir.Controllers
{
    public class ClienteAutenticadoController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];
        private string baseCertPath = ConfigurationManager.AppSettings["CertUrl"];

        [HttpGet]
        public async Task<HttpResponseMessage> Get(int ID_Empresa)
        {
            try
            {
                if (ID_Empresa == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                else
                {
                    var url = basePath + "/api/ite/cliente-autenticado";

                    var certificadoFileName = "planalto_industria_2022.pfx";
                    var certificadoPassword = "123456789";

                    var pathCert = @baseCertPath + certificadoFileName;

                    var certificate = new X509Certificate2(pathCert, certificadoPassword);
                    var handler = new HttpClientHandler();
                    handler.ClientCertificates.Add(certificate);

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
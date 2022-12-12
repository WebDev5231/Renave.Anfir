using Newtonsoft.Json;
using Renave.Anfir.Business;
using Renave.Anfir.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Renave.Anfir.Controllers
{
    /// <summary>
    /// Consultar entregas.
    /// </summary>
    [RoutePrefix("api")]
    public class EntregasVeiculoZeroKmController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [Route("montadora/entregas-veiculo-zero-km")]
        public async Task<HttpResponseMessage> Get(int ID_Empresa, string chassi, string cnpjEstabelecimentoEntregador, string cnpjEstabelecimentoVendedor, string estado)
        {
            try
            {
                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(ID_Empresa);

                var strParamsArray = new string[4];

                strParamsArray[0] = "chassi=" + chassi;
                strParamsArray[1] = "cnpjEstabelecimentoEntregador=" + cnpjEstabelecimentoEntregador;
                strParamsArray[2] = "cnpjEstabelecimentoVendedor=" + cnpjEstabelecimentoVendedor;
                strParamsArray[3] = "estado=" + estado;

                var sbParams = new StringBuilder();

                for (int i = 0; i < strParamsArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strParamsArray[i]))
                    {
                        if (string.IsNullOrEmpty(sbParams.ToString()))
                            sbParams.Append(strParamsArray[i]);
                        else
                            sbParams.Append("&" + strParamsArray[i]);
                    }
                }

                var url = basePath + "/api/montadora/entregas-veiculo-zero-km?" + sbParams.ToString();

                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();

                        var retorno = JsonConvert.DeserializeObject<List<Entrega>>(jsonString.Result);
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
                        return Request.CreateResponse(response.StatusCode, response.Content.ReadAsStringAsync());
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

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
    /// Consultar registro de estoque. Se não informar nenhum parâmetro de pesquisa, será retornado todo o estoque do estabelecimento.
    /// </summary>
    [RoutePrefix("api")]
    public class EstoquesController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [Route("ite/estoques")]
        public async Task<HttpResponseMessage> Get(int ID_Empresa, string chassi, string estadoEstoque, string placa)
        {
            try
            {
                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(ID_Empresa);

                var url = basePath + "/api/ite/estoques?chassi=" + chassi + "&estadoEstoque=" + estadoEstoque;

                if (!string.IsNullOrEmpty(estadoEstoque))
                    url = basePath + "/api/ite/estoques?estadoEstoque=" + estadoEstoque;

                if (!string.IsNullOrEmpty(chassi))
                    url = basePath + "/api/ite/estoques?chassi=" + chassi + "&estadoEstoque=" + estadoEstoque;


                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();

                        var retorno = JsonConvert.DeserializeObject<List<Estoque>>(jsonString.Result);
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

        [Route("montadora/estoques")]
        public async Task<HttpResponseMessage> GetMontadora(int ID_Empresa, string chassi, string estadoEstoque, string placa)
        {
            try
            {
                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(ID_Empresa);

                //Passando ou não a placa irá guardar essa url sem a placa
                var url = basePath + "/api/montadora/estoques?chassi=" + chassi + "&estadoEstoque=" + estadoEstoque;

                if (!string.IsNullOrEmpty(estadoEstoque))
                    url = basePath + "/api/montadora/estoques?estadoEstoque=" + estadoEstoque;

                if (!string.IsNullOrEmpty(chassi))
                    url = basePath + "/api/montadora/estoques?chassi=" + chassi + "&estadoEstoque=" + estadoEstoque;


                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {

                        var jsonString = response.Content.ReadAsStringAsync();
                        var retorno = JsonConvert.DeserializeObject<List<EstoqueMontadora>>(jsonString.Result);

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
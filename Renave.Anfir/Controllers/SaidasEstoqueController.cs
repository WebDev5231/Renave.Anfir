using Newtonsoft.Json;
using Renave.Anfir.Business;
using Renave.Anfir.Model;
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
    /// Sair com veículo de estoque.
    /// </summary>
    public class SaidasEstoqueController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] SaidaEstoqueSolicitacao solicitacao)
        {
            try
            {
                var url = basePath + "/api/ite/saidas-estoque";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(solicitacao.ID_Empresa);

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
                            var retorno = JsonConvert.DeserializeObject<EstoqueRetorno>(jsonString.Result);

                            //Insert no banco
                            var renaveSaidaEstoque = new RenaveSaidaEstoque();

                            renaveSaidaEstoque.ID_Empresa = solicitacao.ID_Empresa;
                            renaveSaidaEstoque.Chassi = retorno.chassi;
                            renaveSaidaEstoque.CpfOperadorResponsavel = retorno.saidaEstoque.cpfOperadorResponsavel;
                            renaveSaidaEstoque.IteOuMontadora = "I";
                            renaveSaidaEstoque.DataHora = DateTime.Now;

                            var estoqueBusiness = new RenaveSaidaEstoqueBusiness();

                            if (estoqueBusiness.SaidasEstoqueIte(renaveSaidaEstoque))
                            {
                                return Request.CreateResponse(retorno);
                            }
                            else
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Saída efetuada com sucesso. Porém não foi possível gravar log.");
                            }

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
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
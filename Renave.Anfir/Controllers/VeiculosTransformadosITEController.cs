using Newtonsoft.Json;
using Renave.Anfir.Business;
using Renave.Anfir.Model;
using Renave.Anfir.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Renave.Anfir.Controllers
{
    [RoutePrefix("api")]
    public class VeiculosTransformadosITEController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        /// <summary>
        /// Autorizar a transferência de um estoque para uma ITE.
        /// </summary>
        /// <param name="envioAutorizacaoTransferenciaParaItePelaMontadora"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/autorizacoes-transferencias-para-ite")]
        public async Task<HttpResponseMessage> PostAutorizacoesTransferenciasParaIte([FromBody] EnvioAutorizacaoTransferenciaParaItePelaMontadora envioAutorizacaoTransferenciaParaItePelaMontadora)
        {
            try
            {
                var url = basePath + "/api/montadora/autorizacoes-transferencias-para-ite";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(envioAutorizacaoTransferenciaParaItePelaMontadora.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(envioAutorizacaoTransferenciaParaItePelaMontadora);

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
                            var retorno = JsonConvert.DeserializeObject<AutorizacaoTransferenciaMontadora>(jsonString.Result);

                            //INSERT DATABASE
                            var renaveOperacoes = new RenaveOperacoes();

                            renaveOperacoes.ID_Empresa = envioAutorizacaoTransferenciaParaItePelaMontadora.ID_Empresa;
                            renaveOperacoes.Chassi = retorno.chassi;
                            renaveOperacoes.CpfOperadorResponsavel = envioAutorizacaoTransferenciaParaItePelaMontadora.cpfOperadorResponsavel;
                            renaveOperacoes.CnpjEstabelecimentoDestino = envioAutorizacaoTransferenciaParaItePelaMontadora.cnpjIteDestino;
                            renaveOperacoes.SaidaOuTransferencia = "Transferencia";
                            renaveOperacoes.IteOuMontadora = "M";
                            renaveOperacoes.DataHora = DateTime.Now;

                            var renaveTransferencia = new RenaveOperacoesBusiness();

                            if (renaveTransferencia.TransferenciaEstoqueMontadora(renaveOperacoes))
                            {
                                return Request.CreateResponse(retorno);
                            }
                            else
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Transferência realizada com sucesso. Erro ao gravar log.");
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

        /// <summary>
        /// Cancelar autorização de transferência de um estoque para uma ITE.
        /// </summary>
        /// <param name="envioCancelamentoAutorizacaoTransferenciaParaIte"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/cancelamentos-autorizacoes-transferencias-para-ite")]
        public async Task<HttpResponseMessage> PostCancelamentosAutorizacoesTransferenciasParaIte([FromBody] EnvioCancelamentoAutorizacaoTransferenciaParaIte envioCancelamentoAutorizacaoTransferenciaParaIte)
        {
            try
            {
                var url = basePath + "/api/montadora/cancelamentos-autorizacoes-transferencias-para-ite";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(envioCancelamentoAutorizacaoTransferenciaParaIte.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(envioCancelamentoAutorizacaoTransferenciaParaIte);

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
                            return Request.CreateResponse(HttpStatusCode.OK);
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

        /// <summary>
        /// Cancelar saída de veículo inacabado de estoque.
        /// </summary>
        /// <param name="solicitacaoCancelamentoSaidaInacabadoPelaMontadora"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/cancelamentos-saidas-estoque-veiculo-inacabado")]
        public async Task<HttpResponseMessage> PostCancelamentosSaidasEstoqueVeiculoInacabado([FromBody] SolicitacaoCancelamentoSaidaInacabadoPelaMontadora solicitacaoCancelamentoSaidaInacabadoPelaMontadora)
        {
            try
            {
                var url = basePath + "/api/montadora/cancelamentos-saidas-estoque-veiculo-inacabado";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(solicitacaoCancelamentoSaidaInacabadoPelaMontadora.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(solicitacaoCancelamentoSaidaInacabadoPelaMontadora);

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
                            var retorno = JsonConvert.DeserializeObject<EstoqueMontadora>(jsonString.Result);

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
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Sair com veículo inacabado de estoque.
        /// </summary>
        /// <param name="solicitacaoSaidaEstoqueInacabadoPelaMontadora"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/saidas-estoque-veiculo-inacabado")]
        public async Task<HttpResponseMessage> PostSaidasEstoqueVeiculoInacabado([FromBody] SolicitacaoSaidaEstoqueInacabadoPelaMontadora solicitacaoSaidaEstoqueInacabadoPelaMontadora)
        {
            try
            {
                var url = basePath + "/api/montadora/saidas-estoque-veiculo-inacabado";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(solicitacaoSaidaEstoqueInacabadoPelaMontadora.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(solicitacaoSaidaEstoqueInacabadoPelaMontadora);

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
                            var retorno = JsonConvert.DeserializeObject<EstoqueMontadora>(jsonString.Result);

                            return Request.CreateResponse(HttpStatusCode.Created, retorno);
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

        /// <summary>
        /// Transferir estoque vindo de ITE.
        /// </summary>
        /// <param name="solicitacaoTransferenciaEstoqueVindoDeIte"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/transferencias-estoque-vindo-de-ite")]
        public async Task<HttpResponseMessage> PostTransferenciasEstoqueVindoDeIte([FromBody] SolicitacaoTransferenciaEstoqueVindoDeIte solicitacaoTransferenciaEstoqueVindoDeIte)
        {
            try
            {
                var url = basePath + "/api/montadora/transferencias-estoque-vindo-de-ite";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(solicitacaoTransferenciaEstoqueVindoDeIte.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(solicitacaoTransferenciaEstoqueVindoDeIte);

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
                            var retorno = JsonConvert.DeserializeObject<EstoqueMontadora>(jsonString.Result);

                            return Request.CreateResponse(HttpStatusCode.OK, retorno);
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

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
    [RoutePrefix("api")]
    public class EstoqueDeVeiculosZeroKmController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        /// <summary>
        /// Consultar veículos zero km pendentes de entrada em estoque. Caso o chassi não seja informado, serão retornados todos os veículos pendentes para o estabelecimento solicitante.
        /// </summary>
        /// <param name="ID_Empresa"></param>
        /// <param name="chassi"></param>
        /// <returns></returns>
        [Route("montadora/veiculos-zero-km-pendentes-entrada-estoque")]
        public async Task<HttpResponseMessage> Get(int ID_Empresa, string chassi = null)
        {
            try
            {
                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(ID_Empresa);

                var url = basePath + "/api/montadora/veiculos-zero-km-pendentes-entrada-estoque";

                if (!string.IsNullOrEmpty(chassi))
                    url = basePath + "/api/montadora/veiculos-zero-km-pendentes-entrada-estoque?chassi=" + chassi;


                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();

                        var retorno = JsonConvert.DeserializeObject<List<VeiculoZeroKmPendenteEntradaEstoque>>(jsonString.Result);
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

        /// <summary>
        /// Cancelar entrada de estoque de veículo zero km.
        /// </summary>
        /// <param name="solicitacaoCancelamentoEntradaZeroKm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/cancelamentos-entrada-estoque-zero-km")]
        public async Task<HttpResponseMessage> PostCancelamentosEntradaEstoqueZeroKm([FromBody] SolicitacaoCancelamentoEntradaZeroKm solicitacaoCancelamentoEntradaZeroKm)
        {
            try
            {
                var url = basePath + "/api/montadora/cancelamentos-entrada-estoque-zero-km";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(solicitacaoCancelamentoEntradaZeroKm.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(solicitacaoCancelamentoEntradaZeroKm);

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
        /// Cancelar saída de estoque de veículo zero km.
        /// </summary>
        /// <param name="solicitacaoCancelamentoSaidaZeroKm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/cancelamentos-saida-estoque-zero-km")]
        public async Task<HttpResponseMessage> PostCancelamentosSaidaEstoqueZeroKm([FromBody] SolicitacaoCancelamentoSaidaZeroKm solicitacaoCancelamentoSaidaZeroKm)
        {
            try
            {
                var url = basePath + "/api/montadora/cancelamentos-saida-estoque-zero-km";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(solicitacaoCancelamentoSaidaZeroKm.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(solicitacaoCancelamentoSaidaZeroKm);

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
        /// Entrar com veículo zero km em estoque.
        /// </summary>
        /// <param name="solicitacaoEntradaEstoqueZeroKmPelaMontadora"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/entradas-estoque-zero-km")]
        public async Task<HttpResponseMessage> PostEntradasEstoqueZeroKm([FromBody] SolicitacaoEntradaEstoqueZeroKmPelaMontadora solicitacaoEntradaEstoqueZeroKmPelaMontadora)
        {
            try
            {
                var url = basePath + "/api/montadora/entradas-estoque-zero-km";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(solicitacaoEntradaEstoqueZeroKmPelaMontadora.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(solicitacaoEntradaEstoqueZeroKmPelaMontadora);

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
        /// Sair com veículo zero km de estoque.
        /// </summary>
        /// <param name="solicitacaoSaidaEstoqueZeroKmPelaMontadora"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("montadora/saidas-estoque-veiculo-zero-km")]
        public async Task<HttpResponseMessage> PostSaidasEstoqueZeroKm([FromBody] SolicitacaoSaidaEstoqueZeroKmPelaMontadora solicitacaoSaidaEstoqueZeroKmPelaMontadora)
        {
            try
            {
                var url = basePath + "/api/montadora/saidas-estoque-veiculo-zero-km";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(solicitacaoSaidaEstoqueZeroKmPelaMontadora.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(solicitacaoSaidaEstoqueZeroKmPelaMontadora);

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

                            //Efetua Insert no Banco de Dados
                            var renaveOperacoes = new RenaveOperacoes();

                            renaveOperacoes.ID_Empresa = solicitacaoSaidaEstoqueZeroKmPelaMontadora.ID_Empresa;
                            renaveOperacoes.Chassi = retorno.chassi;
                            renaveOperacoes.CpfOperadorResponsavel = solicitacaoSaidaEstoqueZeroKmPelaMontadora.cpfOperadorResponsavel;
                            renaveOperacoes.CnpjEstabelecimentoDestino = null;
                            renaveOperacoes.SaidaOuTransferencia = "Saida";
                            renaveOperacoes.IteOuMontadora = "M";
                            renaveOperacoes.DataHora = DateTime.Now;

                            var estoqueBusiness = new RenaveOperacoesBusiness();

                            if (estoqueBusiness.SaidasEstoqueZeroKm(renaveOperacoes))
                            {
                                return Request.CreateResponse(retorno);
                            }
                            else
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Saída efetuada com sucesso. Erro ao gravar log.");
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

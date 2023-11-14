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
    /// Autorizar a transferência de um estoque vindo de uma ITE.
    /// </summary>
    public class AutorizacoesTransferenciasEstoqueVindoDeIteController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] EnvioAutorizacao envioAutorizacao)
        {
            try
            {

                var url = basePath + "/api/ite/autorizacoes-transferencias-estoque-vindo-de-ite";

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(envioAutorizacao.ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var json = JsonConvert.SerializeObject(envioAutorizacao);

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
                        var teste = envioAutorizacao.valorProduto;

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = response.Content.ReadAsStringAsync();
                            var retorno = JsonConvert.DeserializeObject<Estoque>(jsonString.Result);

                            try
                            {
                                //INSERT DBO
                                var renaveOperacoes = new RenaveOperacoes();

                                renaveOperacoes.ID_Empresa = envioAutorizacao.ID_Empresa;
                                renaveOperacoes.Chassi = retorno.chassi;
                                renaveOperacoes.CpfOperadorResponsavel = envioAutorizacao.cpfOperadorResponsavel;
                                renaveOperacoes.CnpjEstabelecimentoDestino = envioAutorizacao.cnpjEstabelecimentoDestino;
                                renaveOperacoes.SaidaOuTransferencia = "Transferencia";
                                renaveOperacoes.IteOuMontadora = "I";
                                renaveOperacoes.DataHora = DateTime.Now;

                                var estoqueBusiness = new RenaveOperacoesBusiness();

                                if (estoqueBusiness.TransferenciaEstoqueIte(renaveOperacoes))
                                {
                                    return Request.CreateResponse(retorno);
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Transferencia realizada com sucesso, Erro ao gravar log.");
                                }
                            }
                            catch (Exception ex)
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
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
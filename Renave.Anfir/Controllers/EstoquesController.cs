using Newtonsoft.Json;
using Renave.Anfir.Business;
using Renave.Anfir.Model;
using Renave.Anfir.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
                        if (estadoEstoque == "CONFIRMADO")
                        {
                            var dadosEntradasEstoqueIte = new RenaveOperacoesBusiness();
                            var dataEntradasEstoqueIte = dadosEntradasEstoqueIte.GetEntradasEstoqueIte(ID_Empresa);

                            //var mapaChassiCliente = dataEntradasEstoqueIte.ToDictionary(item => item.chassi);

                            var mapaChassiCliente = new Dictionary<string, EntradasEstoqueIte>();

                            var jsonString = response.Content.ReadAsStringAsync();
                            var dadosEstoque = JsonConvert.DeserializeObject<List<Estoque>>(jsonString.Result);

                            var dadosCompletos = new List<EntradasEstoqueIte>();
                            var chassisNaoEncontrados = new List<Estoque>();

                            foreach (var item in dataEntradasEstoqueIte)
                            {
                                if (!mapaChassiCliente.ContainsKey(item.chassi))
                                {
                                    mapaChassiCliente.Add(item.chassi, item);
                                }
                            }

                            if (dadosCompletos != null)
                            {
                                foreach (var estoque in dadosEstoque)
                                {
                                    if (mapaChassiCliente.TryGetValue(estoque.chassi, out var dadosCliente))
                                    {
                                        var dadosCompletosItem = new EntradasEstoqueIte();

                                        dadosCompletosItem.chassi = estoque.chassi;
                                        dadosCompletosItem.NomeCliente = dadosCliente.NomeCliente;
                                        dadosCompletosItem.Logradouro = dadosCliente.Logradouro;
                                        dadosCompletosItem.Bairro = dadosCliente.Bairro;
                                        dadosCompletosItem.Numero = dadosCliente.Numero;
                                        dadosCompletosItem.Complemento = dadosCliente.Complemento;
                                        dadosCompletosItem.Cep = dadosCliente.Cep;
                                        dadosCompletosItem.Siafi = dadosCliente.Siafi;
                                        dadosCompletosItem.ValorProduto = dadosCliente.ValorProduto;
                                        dadosCompletosItem.Email = dadosCliente.Email;
                                        dadosCompletosItem.NumeroDocumento = dadosCliente.NumeroDocumento;
                                        dadosCompletosItem.TipoDocumento = dadosCliente.TipoDocumento;
                                        dadosCompletosItem.QuilometragemHodometro = dadosCliente.QuilometragemHodometro;
                                        dadosCompletosItem.cpfOperadorResponsavel = dadosCliente.cpfOperadorResponsavel;
                                        dadosCompletosItem.ID_Empresa = dadosCliente.ID_Empresa;
                                        dadosCompletosItem.CodigoClienteMontadora = dadosCliente.CodigoClienteMontadora;

                                        dadosCompletosItem.DataEntradaEstoque = dadosCliente.DataEntradaEstoque;

                                        dadosCompletosItem.id = estoque.id;
                                        dadosCompletosItem.estado = estoque.estado;
                                        dadosCompletosItem.LeasingVeiculosInacabados = estoque.leasingVeiculoInacabado;

                                        dadosCompletos.Add(dadosCompletosItem);
                                    }
                                    else
                                    {
                                        chassisNaoEncontrados.Add(estoque);
                                    }
                                }

                                var resposta = new
                                {
                                    DadosCompletos = dadosCompletos,
                                    ChassisNaoEncontrados = chassisNaoEncontrados
                                };

                                return Request.CreateResponse(resposta);
                            }
                        }
                        else if (estadoEstoque == "FINALIZADO")
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var dadosEstoque = JsonConvert.DeserializeObject<List<Estoque>>(jsonString);

                            var resposta = new
                            {
                                DadosCompletos = dadosEstoque
                            };

                            return Request.CreateResponse(resposta);
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

            return Request.CreateResponse();
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
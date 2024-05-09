using Newtonsoft.Json;
using Renave.Anfir.Business;
using Renave.Anfir.Model;
using Renave.Anfir.Model.Models;
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
        public async Task<HttpResponseMessage> GetEstoqueIte(int ID_Empresa, string chassi, string estadoEstoque)
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

                                var resposta = new EstoqueVeiculoStatus
                                {
                                    DadosCompletos = dadosCompletos,
                                    ChassisNaoEncontrados = chassisNaoEncontrados
                                };

                                List<Veiculo> veiculosAcabados = new List<Veiculo>();
                                IEnumerable<string> chassisParaConsulta = resposta.DadosCompletos
                                    .Select(item => item.chassi)
                                    .Concat(resposta.ChassisNaoEncontrados.Select(item => item.chassi));

                                if (!string.IsNullOrEmpty(chassi) && !chassisParaConsulta.Contains(chassi))
                                {
                                    chassisParaConsulta = chassisParaConsulta.Concat(new[] { chassi });
                                }

                                foreach (var chassiConsulta in chassisParaConsulta)
                                {
                                    try
                                    {
                                        var responseConsultarVeiculos = await ConsultarVeiculosAcabados(ID_Empresa, chassiConsulta);

                                        if (responseConsultarVeiculos.IsSuccessStatusCode)
                                        {
                                            var jsonStringVeiculoAcabado = await responseConsultarVeiculos.Content.ReadAsStringAsync();
                                            var veiculoAcabado = JsonConvert.DeserializeObject<Veiculo>(jsonStringVeiculoAcabado);

                                            veiculosAcabados.Add(veiculoAcabado);
                                        }
                                        else if (responseConsultarVeiculos.StatusCode == (HttpStatusCode)422)
                                        {
                                            var jsonStringUnprocessable = await responseConsultarVeiculos.Content.ReadAsStringAsync();
                                            var retornoUnprocessable = JsonConvert.DeserializeObject<ErroRetorno>(jsonStringUnprocessable);
                                            return Request.CreateResponse((HttpStatusCode)422, retornoUnprocessable);
                                        }
                                        else
                                        {
                                            return Request.CreateResponse(responseConsultarVeiculos.StatusCode, await responseConsultarVeiculos.Content.ReadAsStringAsync());
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                                    }
                                }

                                foreach (var veiculoAcabado in veiculosAcabados)
                                {
                                    var dadoCompleto = resposta.DadosCompletos.FirstOrDefault(c => c.chassi == veiculoAcabado.chassi);
                                    if (dadoCompleto != null)
                                    {
                                        dadoCompleto.VeiculoAcabado = new Veiculos
                                        {
                                            chassi = veiculoAcabado.chassi,
                                            placa = veiculoAcabado.placa,
                                            renavam = veiculoAcabado.renavam,
                                            restricoes = veiculoAcabado.restricoes.Select(r => new Model.Models.Restricao { codigoTipo = r.codigoTipo, tipo = r.tipo }).ToList(),
                                            veiculoAcabado = veiculoAcabado.veiculoAcabado
                                        };
                                    }

                                    var chassiNaoEncontrado = resposta.ChassisNaoEncontrados.FirstOrDefault(c => c.chassi == veiculoAcabado.chassi);
                                    if (chassiNaoEncontrado != null)
                                    {
                                        chassiNaoEncontrado.VeiculoAcabado = veiculoAcabado; 
                                    }
                                } 
                                
                                return Request.CreateResponse(HttpStatusCode.OK, resposta);
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
        public async Task<HttpResponseMessage> GetEstoqueMontadora(int ID_Empresa, string chassi, string estadoEstoque)
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

        [HttpGet]
        [Route("ite/veiculos")]
        public async Task<HttpResponseMessage> ConsultarVeiculosAcabados(int ID_Empresa, string chassiConsulta)
        {
            try
            {
                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(ID_Empresa);

                var url2 = basePath + "/api/ite/veiculos?chassi=" + chassiConsulta;

                using (var clientVeiculos = new HttpClient(handler))
                {
                    var responseVeiculo = await clientVeiculos.GetAsync(url2);

                    if (responseVeiculo.IsSuccessStatusCode)
                    {
                        var jsonStringVeiculoAcabado = await responseVeiculo.Content.ReadAsStringAsync();
                        var veiculoAcabado = JsonConvert.DeserializeObject<Veiculo>(jsonStringVeiculoAcabado);

                        return Request.CreateResponse(HttpStatusCode.OK, veiculoAcabado);
                    }
                    else if (responseVeiculo.StatusCode == (HttpStatusCode)422)
                    {
                        var jsonString = responseVeiculo.Content.ReadAsStringAsync();
                        var retorno = JsonConvert.DeserializeObject<ErroRetorno>(jsonString.Result);

                        return Request.CreateResponse((HttpStatusCode)422, retorno);
                    }
                    else
                    {
                        return Request.CreateResponse(responseVeiculo.StatusCode, responseVeiculo.Content.ReadAsStringAsync());
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
﻿using Newtonsoft.Json;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace Renave.Anfir.Controllers
{
    /// <summary>
    /// Entrar com veículo de estoque.
    /// </summary>
    public class EntradasEstoqueController : ApiController
    {

        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] EntradaEstoqueSolicitacao solicitacao)
        {
            try
            {
                var url = basePath + "/api/ite/entradas-estoque";

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

                            try
                            {
                                var renaveEntradasEstoqueIte = new EntradasEstoqueIte();

                                renaveEntradasEstoqueIte.ID_Empresa = solicitacao.ID_Empresa;
                                renaveEntradasEstoqueIte.chassi = solicitacao.chassi;
                                renaveEntradasEstoqueIte.ChaveNotaFiscalRemessa = solicitacao.chaveNotaFiscalRemessa;
                                renaveEntradasEstoqueIte.cpfOperadorResponsavel = solicitacao.cpfOperadorResponsavel;
                                renaveEntradasEstoqueIte.NomeCliente = solicitacao.clienteDaIte.nome;
                                renaveEntradasEstoqueIte.Cep = solicitacao.clienteDaIte.endereco.cep;
                                renaveEntradasEstoqueIte.Logradouro = solicitacao.clienteDaIte.endereco.logradouro;
                                renaveEntradasEstoqueIte.Bairro = Regex.Replace(solicitacao.clienteDaIte.endereco.bairro, @"[^\w\sáéíóúàèìòùâêîôûãõçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕÇ]", "");
                                renaveEntradasEstoqueIte.Siafi = solicitacao.clienteDaIte.endereco.codigoMunicipio;
                                renaveEntradasEstoqueIte.Numero = solicitacao.clienteDaIte.endereco.numero;
                                renaveEntradasEstoqueIte.Complemento = solicitacao.clienteDaIte.endereco.complemento;
                                renaveEntradasEstoqueIte.Email = solicitacao.clienteDaIte.email;
                                renaveEntradasEstoqueIte.NumeroDocumento = solicitacao.clienteDaIte.numeroDocumento;
                                renaveEntradasEstoqueIte.TipoDocumento = solicitacao.clienteDaIte.tipoDocumento;
                                renaveEntradasEstoqueIte.ValorProduto = solicitacao.valorProduto;
                                renaveEntradasEstoqueIte.QuilometragemHodometro = solicitacao.quilometragemHodometro;
                                renaveEntradasEstoqueIte.DataMedicaoHodometro = solicitacao.dataHoraMedicaoHodometro;
                                renaveEntradasEstoqueIte.CodigoClienteMontadora = solicitacao.codigoClienteMontadora;
                                renaveEntradasEstoqueIte.LeasingVeiculosInacabados = false;
                                renaveEntradasEstoqueIte.estado = "CONFIRMADO";

                                DateTime dataHoraAtual = DateTime.Now;
                                string dataHoraFormatada = dataHoraAtual.ToString("yyyy-MM-dd HH:mm:ss");
                                renaveEntradasEstoqueIte.DataEntradaEstoque = dataHoraFormatada;

                                var estoqueBusiness = new RenaveOperacoesBusiness();

                                if (estoqueBusiness.EntradasEstoqueIte(renaveEntradasEstoqueIte))
                                {
                                    return Request.CreateResponse(retorno);
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Entrada de estoque efetuada com sucesso. Erro ao Gravar log.");
                                }

                            }
                            catch (Exception ex)
                            {
                                return Request.CreateResponse(ex);
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
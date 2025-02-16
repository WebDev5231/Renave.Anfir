﻿using Newtonsoft.Json;
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
    /// Consultar autorizações de transferência. Se não informar nenhum parâmetro de pesquisa, serão retornadas todas as autorizações relacionadas com a ITE/Montadora solicitante.
    /// </summary>
    [RoutePrefix("api")]
    public class AutorizacoesTransferenciasController : ApiController
    {
        private string basePath = ConfigurationManager.AppSettings["SerproRenaveApiUrl"];

        [Route("ite/autorizacoes-transferencias")]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var url = basePath + "/api/ite/autorizacoes-transferencias";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        var retorno = JsonConvert.DeserializeObject<List<AutorizacaoTransferencia>>(jsonString.Result);

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
        [Route("ite/autorizacoes-transferencias-chassi-estadoAutorizacao")]
        public async Task<HttpResponseMessage> GetITEByChassiEstadoAutorizacao(int ID_Empresa, string chassi, string estadoAutorizacao)
        {
            try
            {
                var url = basePath + "/api/ite/autorizacoes-transferencias?chassi=" + chassi + "&estadoAutorizacao=" + estadoAutorizacao;

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        var retorno = JsonConvert.DeserializeObject<List<AutorizacaoTransferenciaRetorno>>(jsonString.Result);

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

        #region Montadora

        [Route("montadora/autorizacoes-transferencias")]
        public async Task<HttpResponseMessage> GetMontadora()
        {
            try
            {
                var url = basePath + "/api/montadora/autorizacoes-transferencias";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        var retorno = JsonConvert.DeserializeObject<List<AutorizacaoTransferencia>>(jsonString.Result);

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
        [Route("montadora/autorizacoes-transferencias-chassi-estadoAutorizacao")]
        public async Task<HttpResponseMessage> GetMontadoraByChassiEstadoAutorizacao(int ID_Empresa, string chassi, string estadoAutorizacao)
        {
            try
            {
                var url = basePath + "/api/montadora/autorizacoes-transferencias?chassi=" + chassi + "&estadoAutorizacao=" + estadoAutorizacao;

                var certificadoBusiness = new CertificadoBusiness();
                var handler = certificadoBusiness.GetHandler(ID_Empresa);

                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        var retorno = JsonConvert.DeserializeObject<List<AutorizacaoTransferenciaRetorno>>(jsonString.Result);

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

        #endregion
    }
}
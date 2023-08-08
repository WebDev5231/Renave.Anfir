using Renave.Anfir.Model;
using Renave.Anfir.Business;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text.RegularExpressions;

namespace Renave.Anfir.Controllers
{
    [RoutePrefix("api/certificate")] 
    public class CertificateController : ApiController
    {
        [HttpPost]
        [Route("upload")] 
        public HttpResponseMessage UploadFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var idEmpresa = HttpContext.Current.Request.Form["empresa"];
                var password = HttpContext.Current.Request.Form["password"];

                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    var fileName = Path.GetFileName(postedFile.FileName);

                    fileName = Regex.Replace(fileName, @"[^\w\d.]", "");

                    var filePath = Path.Combine(@"C:\inetpub\wwwroot\renave.anfir\certificados", fileName);

                    postedFile.SaveAs(filePath);

                    var updateCertificate = new EmpresaRenaveCertificado();
                    updateCertificate.ID_Empresa = int.Parse(idEmpresa);
                    updateCertificate.CertificadoFileName = fileName;
                    updateCertificate.CertificadoPassword = password;
                    updateCertificate.Data_inclusao = DateTime.Now.ToString("dd/MM/yyyy");

                    var renaveOperacoesBusiness = new RenaveOperacoesBusiness();

                    bool isUpdateSuccessful = renaveOperacoesBusiness.UpdateCertificate(updateCertificate);

                    if (isUpdateSuccessful)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "O certificado foi atualizado com sucesso.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao atualizar o certificado digital.");
                    }
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nenhum arquivo foi enviado.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

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
                var ID_Empresa = HttpContext.Current.Request.Form["empresa"];
                var password = HttpContext.Current.Request.Form["password"];

                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    var fileName = Path.GetFileName(postedFile.FileName);

                    fileName = Regex.Replace(fileName, @"[^\w\d.]", "");

                    var filePath = Path.Combine(@"C:\inetpub\wwwroot\renave.anfir\certificados", fileName);

                    var renaveOperacoesBusiness = new RenaveOperacoesBusiness();

                    var existingCertificate = new EmpresaRenaveCertificado();
                    bool certificateExists = renaveOperacoesBusiness.GetCertificate(int.Parse(ID_Empresa), existingCertificate);

                    if (!certificateExists)
                    {
                        var newCertificate = new EmpresaRenaveCertificado();
                        newCertificate.CertificadoFileName = fileName;
                        newCertificate.CertificadoPassword = password;
                        newCertificate.ID_Empresa = int.Parse(ID_Empresa);
                        newCertificate.Data_inclusao = DateTime.Now.ToString("dd/MM/yyyy");

                        bool isInsertSuccessful = renaveOperacoesBusiness.InsertCertificate(newCertificate);

                        if (isInsertSuccessful)
                        {
                            postedFile.SaveAs(filePath);
                            return Request.CreateResponse(HttpStatusCode.OK, "O certificado foi inserido com sucesso.");
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao inserir o certificado digital.");
                        }
                    }
                    else
                    {
                        existingCertificate.ID_Empresa = int.Parse(ID_Empresa);
                        existingCertificate.CertificadoFileName = fileName;
                        existingCertificate.CertificadoPassword = password;
                        existingCertificate.Data_inclusao = DateTime.Now.ToString("dd/MM/yyyy");

                        bool isUpdateSuccessful = renaveOperacoesBusiness.UpdateCertificate(existingCertificate);

                        if (isUpdateSuccessful)
                        {
                            postedFile.SaveAs(filePath);
                            return Request.CreateResponse(HttpStatusCode.OK, "O certificado foi atualizado com sucesso.");
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao atualizar o certificado digital.");
                        }
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

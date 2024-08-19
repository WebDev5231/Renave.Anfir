using Renave.Anfir.Data.Repository;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Renave.Anfir.Business
{
    public class CertificadoBusiness
    {
        public HttpClientHandler GetHandler(int ID_Empresa)
        {
            var empresaRenaveCertificadoData = new EmpresaRenaveCertificadoData();

            var empresaRenaveCertificado = empresaRenaveCertificadoData.Get(ID_Empresa);

            if (empresaRenaveCertificado == null) return null;
            else
            {
                var certificadoFileName = empresaRenaveCertificado.CertificadoFileName;
                var certificadoPassword = empresaRenaveCertificado.CertificadoPassword;

                var pathCert = HttpContext.Current.Server.MapPath("~/") + @"certificados\" + certificadoFileName;

                var certificate = new X509Certificate2(pathCert, certificadoPassword);
                var handler = new HttpClientHandler();
                handler.ClientCertificates.Add(certificate);

                return handler;
            }
        }
    }
}

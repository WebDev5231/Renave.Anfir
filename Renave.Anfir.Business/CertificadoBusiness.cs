using Renave.Anfir.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
                //Testes
                //var certificadoFileName = "planalto_industria_2022.pfx";
                //var certificadoPassword = "123456789";

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

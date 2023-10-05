using Dapper;
using Dapper.Contrib.Extensions;
using Renave.Anfir.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renave.Anfir.Data
{
    public class CertificateData
    {
        public bool Update(EmpresaRenaveCertificado empresa)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                bool success = cn.Update(empresa);
                return success;
            }
        }

        public EmpresaRenaveCertificado Get(int ID_Empresa)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                var certificate = cn.Get<EmpresaRenaveCertificado>(ID_Empresa);

                return certificate;
            }
        }

        public bool Insert(EmpresaRenaveCertificado certificate)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                cn.Open();

                var sql = "INSERT INTO EmpresaRenaveCertificado (ID_Empresa, CertificadoFileName, CertificadoPassword, Data_inclusao) VALUES (@ID_Empresa, @CertificadoFileName, @CertificadoPassword, @Data_inclusao)";
                var parameters = new
                {
                    ID_Empresa = certificate.ID_Empresa,
                    CertificadoFileName = certificate.CertificadoFileName,
                    CertificadoPassword = certificate.CertificadoPassword,
                    Data_inclusao = certificate.Data_inclusao
                };

                int rowsAffected = cn.Execute(sql, parameters);

                return rowsAffected > 0;

            }
        }
    }
}

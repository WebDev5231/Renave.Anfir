using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using Renave.Anfir.Model;

namespace Renave.Anfir.Data.Repository
{
    public class UpdateCertificateData
    {
        public bool Update(EmpresaRenaveCertificado updateCertificate)
        {
            try
            {
                using (var cn = new SqlConnection(Database.ConnectionString))
                {
                    cn.Open();
                    var result = cn.Update(updateCertificate);

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Trate o erro de acordo com o que for necessário em seu aplicativo
                // Por exemplo, pode lançar uma exceção personalizada, registrar o erro, etc.
                return false;
            }
        }
    }
}

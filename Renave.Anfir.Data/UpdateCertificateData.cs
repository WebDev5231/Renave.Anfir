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
    public class UpdateCertificateData
    {
        public bool Update(EmpresaRenaveCertificado empresa)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                bool success = cn.Update(empresa);
                return success;
            }
        }
    }
}

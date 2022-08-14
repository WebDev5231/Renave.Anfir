using Dapper.Contrib.Extensions;
using Renave.Anfir.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renave.Anfir.Data.Repository
{
    public class EmpresaRenaveCertificadoData
    {
        public EmpresaRenaveCertificado Get(int id)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                var retorno = cn.Get<EmpresaRenaveCertificado>(id);

                return retorno;
            }
        }
    }
}

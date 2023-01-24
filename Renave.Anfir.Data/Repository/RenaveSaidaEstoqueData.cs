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
    public class RenaveSaidaEstoqueData
    {
        public bool Insert(RenaveSaidaEstoque renaveSaidaEstoque)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                var retorno = cn.Insert(renaveSaidaEstoque);

                return true;
            }
        }
    }
}

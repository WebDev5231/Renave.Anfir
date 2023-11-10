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
    public class RenaveOperacoesData
    {
        public bool InsertRenaveOperacoes(RenaveOperacoes renaveOperacoesEstoque)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                var retorno = cn.Insert(renaveOperacoesEstoque);

                return true;
            }
        }

        public bool InsertEntradaEstoqueIte(EntradasEstoqueIte InsertEntradaEstoqueIte)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                var retorno = cn.Insert(InsertEntradaEstoqueIte);

                return true;
              }
        }
    }
}

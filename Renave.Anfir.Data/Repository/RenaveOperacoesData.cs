using Dapper;
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

        //TESTAR
        public List<EntradasEstoqueIte> SelectEntradasEstoqueIte(int ID_Empresa)
        {
            using (var cn = new SqlConnection(Database.ConnectionString))
            {
                string query = @"SELECT * FROM EntradasEstoqueIte WHERE ID_Empresa = @ID_Empresa";
                try
                {
                    var entradasEstoqueIteList = cn.Query<EntradasEstoqueIte>(query, new { ID_Empresa }).ToList();

                    if (entradasEstoqueIteList == null || entradasEstoqueIteList.Count == 0)
                    {
                        return new List<EntradasEstoqueIte>();
                    }

                    return entradasEstoqueIteList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao buscar os dados do Cliente: {ex.Message}");
                    throw;
                }
            }
        }

    }
}

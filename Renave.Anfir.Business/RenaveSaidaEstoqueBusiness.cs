using Renave.Anfir.Data.Repository;
using Renave.Anfir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renave.Anfir.Business
{
    public class RenaveSaidaEstoqueBusiness
    {
        public bool SaidasEstoqueZeroKm(RenaveSaidaEstoque renaveSaidaEstoque)
        {
            var renaveSaidaEstoqueData = new RenaveSaidaEstoqueData();

            if (renaveSaidaEstoqueData.Insert(renaveSaidaEstoque))
            {
                return true;
            }

            return false;
        }
        public bool SaidasEstoqueIte(RenaveSaidaEstoque renaveSaidaEstoque)
        {
            var renaveSaidaEstoqueData = new RenaveSaidaEstoqueData();

            if (renaveSaidaEstoqueData.Insert(renaveSaidaEstoque))
            {
                return true;
            }

            return false;
        }
    }
}

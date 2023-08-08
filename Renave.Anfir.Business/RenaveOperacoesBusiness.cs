using Renave.Anfir.Data;
using Renave.Anfir.Data.Repository;
using Renave.Anfir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renave.Anfir.Business
{
    public class RenaveOperacoesBusiness
    {
        public bool SaidasEstoqueZeroKm(RenaveOperacoe renaveOperacoesEstoque)
        {
            var renaveSaidaEstoqueData = new RenaveSaidaEstoqueData();

            if (renaveSaidaEstoqueData.Insert(renaveOperacoesEstoque))
            {
                return true;
            }

            return false;
        }
        public bool SaidasEstoqueIte(RenaveOperacoe renaveSaidaEstoque)
        {
            var renaveSaidaEstoqueData = new RenaveSaidaEstoqueData();

            if (renaveSaidaEstoqueData.Insert(renaveSaidaEstoque))
            {
                return true;
            }

            return false;
        }

        public bool TransferenciaEstoqueIte(RenaveOperacoe renaveTransferenciaIte)
        {
            var renaveTransferenciaEstoque = new RenaveSaidaEstoqueData();

            if (renaveTransferenciaEstoque.Insert(renaveTransferenciaIte))
            {
                return true;
            }

            return false;
        }
        public bool TransferenciaEstoqueMontadora(RenaveOperacoe renaveTransferenciaMontadora)
        {
            var renaveTransferenciaEstoque = new RenaveSaidaEstoqueData();

            if (renaveTransferenciaEstoque.Insert(renaveTransferenciaMontadora))
            {
                return true;
            }

            return false;
        }
        public bool UpdateCertificate(EmpresaRenaveCertificado updateCertificate)
        {
            var renaveUpdateCertificate = new UpdateCertificateData();

            if (renaveUpdateCertificate.Update(updateCertificate))
            {
                return true;
            }

            return false;
        }
    }
}

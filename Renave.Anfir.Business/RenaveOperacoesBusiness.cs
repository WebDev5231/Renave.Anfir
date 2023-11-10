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
        public bool SaidasEstoqueZeroKm(RenaveOperacoes renaveOperacoesEstoque)
        {
            var renaveSaidaEstoqueData = new RenaveOperacoesData();

            if (renaveSaidaEstoqueData.InsertRenaveOperacoes(renaveOperacoesEstoque))
            {
                return true;
            }

            return false;
        }

        public bool SaidasEstoqueIte(RenaveOperacoes renaveSaidaEstoque)
        {
            var renaveSaidaEstoqueData = new RenaveOperacoesData();

            if (renaveSaidaEstoqueData.InsertRenaveOperacoes(renaveSaidaEstoque))
            {
                return true;
            }

            return false;
        }

        public bool TransferenciaEstoqueIte(RenaveOperacoes renaveTransferenciaIte)
        {
            var renaveTransferenciaEstoque = new RenaveOperacoesData();

            if (renaveTransferenciaEstoque.InsertRenaveOperacoes(renaveTransferenciaIte))
            {
                return true;
            }

            return false;
        }

        public bool TransferenciaEstoqueMontadora(RenaveOperacoes renaveTransferenciaMontadora)
        {
            var renaveTransferenciaEstoque = new RenaveOperacoesData();

            if (renaveTransferenciaEstoque.InsertRenaveOperacoes(renaveTransferenciaMontadora))
            {
                return true;
            }

            return false;
        }

        public bool UpdateCertificate(EmpresaRenaveCertificado updateCertificate)
        {
            var renaveUpdateCertificate = new CertificateData();

            if (renaveUpdateCertificate.Update(updateCertificate))
            {
                return true;
            }

            return false;
        }

        public bool GetCertificate(int idEmpresa, EmpresaRenaveCertificado certificate)
        {
            var renaveCertificateData = new CertificateData();
            certificate = renaveCertificateData.Get(idEmpresa);

            return certificate != null;
        }

        public bool InsertCertificate(EmpresaRenaveCertificado certificateInsert)
        {
            var renaveTransferenciaEstoque = new CertificateData();

            if (renaveTransferenciaEstoque.Insert(certificateInsert))
            {
                return true;
            }

            return false;
        }

        public bool EntradasEstoqueIte(EntradasEstoqueIte renaveEntradaEstoqueIte)
        {
            var renaveEntradaEstoqueIteData = new RenaveOperacoesData();

            if (renaveEntradaEstoqueIteData.InsertEntradaEstoqueIte(renaveEntradaEstoqueIte))
            {
                return true;
            }

            return false;
        }
    }
}

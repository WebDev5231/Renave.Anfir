using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class SolicitacaoSaidaEstoqueInacabadoPelaMontadora
    {
        public int ID_Empresa { get; set; }
        public string chaveNotaFiscal { get; set; }
        public CompradorSaidaEstoque comprador { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public string dataVenda { get; set; }
        public string emailEstabelecimento { get; set; }
        public EntregaIndicada entregaIndicada { get; set; }
        public long idEstoque { get; set; }
        public string tipoBeneficioTributario { get; set; }
        public double valorVenda { get; set; }
    }
}
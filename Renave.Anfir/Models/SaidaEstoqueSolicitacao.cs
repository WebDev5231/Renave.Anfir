using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class SaidaEstoqueSolicitacao
    {
        public int ID_Empresa { get; set; }
        public string chaveNotaFiscalProduto { get; set; }
        public string chaveNotaFiscalServico { get; set; }
        public Comprador comprador { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public string dataVenda { get; set; }
        public string emailEstabelecimento { get; set; }
        public long idEstoque { get; set; }
        public decimal valorVenda { get; set; }
    }
}
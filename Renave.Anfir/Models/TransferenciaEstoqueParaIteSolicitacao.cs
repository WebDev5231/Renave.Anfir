using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class TransferenciaEstoqueParaIteSolicitacao
    {
        public string chaveNotaFiscalRemessa { get; set; }
        public string codigoClienteMontadora { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public string dataHoraMedicaoHodometro { get; set; }
        public string dataTransferenciaEstoque { get; set; }
        public int idAutorizacao { get; set; }
        public int quilometragemHodometro { get; set; }
        public double valorProduto { get; set; }
    }
}
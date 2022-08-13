using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EntradaEstoqueSolicitacao
    {
        public string chassi { get; set; }
        public string chaveNotaFiscalRemessa { get; set; }
        public ClienteDaIte clienteDaIte { get; set; }
        public string codigoClienteMontadora { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public string dataEntradaEstoque { get; set; }
        public DateTime dataHoraMedicaoHodometro { get; set; }
        public int quilometragemHodometro { get; set; }
        public int valorProduto { get; set; }
    }
}
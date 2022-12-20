using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class SolicitacaoEntradaEstoqueZeroKmPelaMontadora
    {
        public int ID_Empresa { get; set; }
        public string chassi { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public string dataEntradaEstoque { get; set; }
        public DateTime dataHoraMedicaoHodometro { get; set; }
        public int quilometragemHodometro { get; set; }
    }
}
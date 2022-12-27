using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class SolicitacaoCancelamentoEntradaZeroKm
    {
        public int ID_Empresa { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public DateTime dataCancelamentoEntradaEstoque { get; set; }
        public long idEstoque { get; set; }
    }
}
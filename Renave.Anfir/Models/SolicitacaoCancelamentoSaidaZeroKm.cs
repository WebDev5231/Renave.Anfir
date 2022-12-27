using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class SolicitacaoCancelamentoSaidaZeroKm
    {
        public int ID_Empresa { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public DateTime dataCancelamentoSaidaEstoque { get; set; }
        public long idEstoque { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EnvioCancelamentoAutorizacaoTransferenciaParaIte
    {
        public int ID_Empresa { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public long idAutorizacao { get; set; }
    }
}
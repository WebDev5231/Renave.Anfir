using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class CancelamentoSaidaEstoque
    {
        public DateTimeOffset dataHoraCancelamentoSaidaEstoque { get; set; }
        public long idEstoqueGeradoNoCancelamentoSaida { get; set; }
    }
}
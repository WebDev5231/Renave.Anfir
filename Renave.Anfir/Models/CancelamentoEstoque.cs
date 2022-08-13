using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class CancelamentoEstoque
    {
        public DateTimeOffset dataHoraCancelamentoEstoque { get; set; }
        public string motivoCancelamentoEstoque { get; set; }
    }
}
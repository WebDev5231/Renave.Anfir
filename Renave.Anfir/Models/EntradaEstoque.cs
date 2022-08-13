using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EntradaEstoque
    {
        public string chaveNotaFiscalEntrada { get; set; }
        public string chaveNotaFiscalRemessaEntrada { get; set; }
        public string chaveNotaFiscalServicoEntrada { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public DateTimeOffset? dataHora { get; set; }
        public DateTimeOffset? dataHoraEnvioNotaFiscalEntrada { get; set; }
        public DateTimeOffset? dataHoraEnvioNotaFiscalRemessaEntrada { get; set; }
        public DateTimeOffset? dataHoraEnvioNotaFiscalServicoEntrada { get; set; }
        public long numeroTermoEntradaEstoque { get; set; }
        public Vendedor vendedor { get; set; }
    }
}
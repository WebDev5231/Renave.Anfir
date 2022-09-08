using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EntradaEstoque
    {
        public string cpfOperadorResponsavel { get; set; }
        public DateTimeOffset? dataHora { get; set; }
        public object chaveNotaFiscalEntrada { get; set; }
        public object chaveNotaFiscalSaida { get; set; }
        public object dataHoraEnvioNotaFiscalEntrada { get; set; }
        public object chaveNotaFiscalServicoEntrada { get; set; }
        public object dataHoraEnvioNotaFiscalServicoEntrada { get; set; }
        public string chaveNotaFiscalRemessaEntrada { get; set; }
        public DateTimeOffset? dataHoraEnvioNotaFiscalRemessaEntrada { get; set; }
        public long numeroTermoEntradaEstoque { get; set; }
        public Vendedor vendedor { get; set; }
    }
}
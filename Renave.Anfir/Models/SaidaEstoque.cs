using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class SaidaEstoque
    {
        public CancelamentoSaidaEstoque cancelamentoSaidaEstoque { get; set; }
        public string chaveNotaFiscalRemessaSaida { get; set; }
        public string chaveNotaFiscalSaida { get; set; }
        public string chaveNotaFiscalServicoSaida { get; set; }
        public Comprador comprador { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public DateTimeOffset? dataHora { get; set; }
        public DateTimeOffset? dataHoraEnvioNotaFiscalRemessaSaida { get; set; }
        public DateTimeOffset? dataHoraEnvioNotaFiscalSaida { get; set; }
        public DateTimeOffset? dataHoraEnvioNotaFiscalServicoSaida { get; set; }
        public string motivo { get; set; }
        public long numeroTermoSaidaEstoque { get; set; }
    }
}
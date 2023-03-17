using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class Entrega
    {
        public CancelamentoEntrega cancelamentoEntrega { get; set; }
        public string chassi { get; set; }
        public string cnpjEstabelecimentoEntregador { get;}
        public string cnpjEstabelecimentoVendedor { get; set; }
        public string estado { get; set; }
        public long? id { get; set; }
        public long? idEntregaGeradaNoCancelamentoEntrega { get; set; }
        public long? idEntregaOrigemCancelamentoEntrega { get; set; }
        public RealizacaoEntrega realizacaoEntrega { get; set; }
    }
}
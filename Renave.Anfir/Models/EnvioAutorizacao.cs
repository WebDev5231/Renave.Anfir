using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EnvioAutorizacao
    {
        public string cnpjEstabelecimentoDestino { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public int EstoqueOrigem { get; set; }
        public bool paraCancelamentoDeTransferencia { get; set; }
        public int valorProduto { get; set; }
    }
}
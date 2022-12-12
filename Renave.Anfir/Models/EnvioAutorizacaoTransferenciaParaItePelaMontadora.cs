using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EnvioAutorizacaoTransferenciaParaItePelaMontadora
    {
        public int ID_Empresa { get; set; }
        public string chaveNotaFiscalProdutoSaida { get; set; }
        public string cnpjIteDestino { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public long idEstoqueOrigem { get; set; }
        public string tipoBeneficioTributario { get; set; }
    }
}
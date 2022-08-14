using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class CancelamentosEntradaEstoqueIteSolicitacao
    {
        public int ID_Empresa { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public string dataCancelamentoEntradaEstoque { get; set; }
        public int idEstoque { get; set; }
    }
}
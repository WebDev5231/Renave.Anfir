using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class TransferenciaVindoDeIteParaIteSolicitacao
    {
        public int ID_Empresa { get; set; }
        public string chaveNotaFiscalProduto { get; set; }
        public string chaveNotaFiscalRemessa { get; set; }
        public string chaveNotaFiscalServico { get; set; }
        public string codigoClienteMontadora { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public DateTime dataHoraMedicaoHodometro { get; set; }
        public string dataTransferenciaEstoque { get; set; }
        public long idAutorizacao { get; set; }
        public long quilometragemHodometro { get; set; }
        public decimal valorProduto { get; set; }
    }
}
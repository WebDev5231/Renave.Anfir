using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class CompradorSaidaEstoque
    {
        public string email { get; set; }
        public EnderecoCompradorSaidaEstoque endereco { get; set; }
        public string nome { get; set; }
        public string numeroDocumento { get; set; }
        public string tipoDocumento { get; set; }
    }
}
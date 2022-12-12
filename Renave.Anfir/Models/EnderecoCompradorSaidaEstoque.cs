using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EnderecoCompradorSaidaEstoque
    {
        public string bairro { get; set; }
        public string cep { get; set; }
        public int codidoMunicipio { get; set; }
        public string complemento { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
    }
}
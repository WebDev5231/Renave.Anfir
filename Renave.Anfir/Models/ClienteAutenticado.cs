using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class ClienteAutenticado
    {
        public string cnpj { get; set; }
        public Endereco endereco { get; set; }
        public string nome { get; set; }
    }
}
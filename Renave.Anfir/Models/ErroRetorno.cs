using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class ErroRetorno
    {
        public DateTimeOffset dataHora { get; set; }
        public string detalhe { get; set; }
        public string mensagemParaUsuarioFinal { get; set; }
        public string titulo { get; set; }
    }
}
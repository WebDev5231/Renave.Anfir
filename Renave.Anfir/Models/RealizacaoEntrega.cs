using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class RealizacaoEntrega
    {
        public string cpfResponsavel { get; set; }
        public DateTimeOffset dataHoraRegistro { get; set; }
    }
}
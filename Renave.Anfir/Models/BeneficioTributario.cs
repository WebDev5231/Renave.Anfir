using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class BeneficioTributario
    {
        public DateTimeOffset dataFimVigencia { get; set; }
        public DateTimeOffset dataInicioVigencia { get; set; }
        public string tipo { get; set; }
    }
}
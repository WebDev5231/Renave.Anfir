using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class VeiculoZeroKmPendenteEntradaEstoque
    {
        public string chassi { get; set; }
        public DateTimeOffset dataHoraCadastro { get; set; }
        public bool veiculoAcabado { get; set; }
    }
}
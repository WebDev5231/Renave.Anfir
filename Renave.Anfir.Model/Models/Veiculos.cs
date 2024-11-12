using Renave.Anfir.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renave.Anfir.Model.Models
{
    public class Veiculos
    {
        public string chassi { get; set; }
        public string placa { get; set; }
        public int? renavam { get; set; }
        public List<Restricao> restricoes { get; set; }
        public bool veiculoAcabado { get; set; }

    }
}

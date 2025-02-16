﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class Veiculo
    {
        public string chassi { get; set; }
        public string placa { get; set; }
        public int? renavam { get; set; }
        public List<Restricao> restricoes { get; set; }
        public bool veiculoAcabado { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class CancelamentosSaidaEstoqueIteSolicitacao
    {
        public int ID_Empresa { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public string dataCancelamentoSaidaEstoque { get; set; }
        public int idEstoque { get; set; }
    }
}
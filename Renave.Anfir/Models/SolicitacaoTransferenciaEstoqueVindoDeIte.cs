﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class SolicitacaoTransferenciaEstoqueVindoDeIte
    {
        public int ID_Empresa { get; set; }
        public string chaveNotaFiscalProduto { get; set; }
        public string chaveNotaFiscalRemessa { get; set; }
        public string chaveNotaFiscalServico { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public DateTime dataHoraMedicaoHodometro { get; set; }
        public string dataTransferenciaEstoque { get; set; }
        public long idAutorizacao { get; set; }
        public int quilometragemHodometro { get; set; }
        public double valorProduto { get; set; }
    }
}
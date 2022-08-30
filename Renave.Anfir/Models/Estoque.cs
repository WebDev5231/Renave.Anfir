using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class Estoque
    {
        public long id { get; set; }
        public string estado { get; set; }
        public object codigoSegurancaCrv { get; set; }
        public object numeroCrv { get; set; }
        public object placa { get; set; }
        public object renavam { get; set; }
        public string chassi { get; set; }
        public object tipoCrv { get; set; }
        public long quilometragemHodometro { get; set; }
        public DateTimeOffset dataHoraMedicaoHodometro { get; set; }
        public EntradaEstoque entradaEstoque { get; set; }
        public object saidaEstoque { get; set; }
        public object cancelamentoEstoque { get; set; }
        public RestricoesVeiculo[] restricoesVeiculo { get; set; }
        public object origemPorCancelamentoEstoque { get; set; }
        public bool estoqueEmIte { get; set; }
        public object idEstoqueGeradoPorTransferencia { get; set; }
        public object idEstoqueOrigemTransferencia { get; set; }
    }
}
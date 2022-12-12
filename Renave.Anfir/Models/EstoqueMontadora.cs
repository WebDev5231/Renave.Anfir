using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EstoqueMontadora
    {
        public BeneficioTributario beneficioTributario { get; set; }
        public CancelamentoEstoque cancelamentoEstoque { get; set; }
        public string chassi { get; set; }
        public string codigoSegurancaCrv { get; set; }
        public DateTimeOffset dataHoraMedicaoHodometro { get; set; }
        public EntradaEstoque entradaEstoque { get; set; }
        public List<Entrega> entregas { get; set; }
        public string estado { get; set; }
        public bool estoqueEmIte { get; set; }
        public long id { get; set; }
        public long idEstoqueGeradoPorTransferencia { get; set; }
        public long idEstoqueOrigemTransferencia { get; set; }
        public string numeroCrv { get; set; }
        public OrigemPorCancelamentoEstoque origemPorCancelamentoEstoque { get; set; }
        public string placa { get; set; }
        public int quilometragemHodometro { get; set; }
        public string renavam { get; set; }
        public List<RestricoesVeiculo> restricoesVeiculos { get; set; }
        public SaidaEstoque saidaEstoque { get; set; }
        public string tipoCrv { get; set; }
    }
}
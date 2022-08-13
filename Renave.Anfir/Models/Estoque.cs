using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class Estoque
    {
        public CancelamentoEstoque CancelamentoEstoque { get; set; }
        public string Chassi { get; set; }
        public string CodigoSegurancaCrv { get; set; }
        public DateTimeOffset DataHoraMedicaoHodometro { get; set; }
        public EntradaEstoque EntradaEstoque { get; set; }
        public string Estado { get; set; }
        public bool EstoqueEmIte { get; set; }
        public long Id { get; set; }
        public long IdEstoqueGeradoPorTransferencia { get; set; }
        public long IdEstoqueOrigemTransferencia { get; set; }
        public string NumeroCrv { get; set; }
        public OrigemPorCancelamentoEstoque OrigemPorCancelamentoEstoque { get; set; }
        public string Placa { get; set; }
        public long QuilometragemHodometro { get; set; }
        public string Renavam { get; set; }
        public RestricoesVeiculo[] RestricoesVeiculo { get; set; }
        public SaidaEstoque SaidaEstoque { get; set; }
        public string TipoCrv { get; set; }
    }

    public partial class CancelamentoEstoque
    {
        public DateTimeOffset DataHoraCancelamentoEstoque { get; set; }
        public string MotivoCancelamentoEstoque { get; set; }
    }

    public partial class EntradaEstoque
    {
        public string ChaveNotaFiscalEntrada { get; set; }
        public string ChaveNotaFiscalRemessaEntrada { get; set; }
        public string ChaveNotaFiscalServicoEntrada { get; set; }
        public string CpfOperadorResponsavel { get; set; }
        public DateTimeOffset DataHora { get; set; }
        public DateTimeOffset DataHoraEnvioNotaFiscalEntrada { get; set; }
        public DateTimeOffset DataHoraEnvioNotaFiscalRemessaEntrada { get; set; }
        public DateTimeOffset DataHoraEnvioNotaFiscalServicoEntrada { get; set; }
        public long NumeroTermoEntradaEstoque { get; set; }
        public Vendedor Vendedor { get; set; }
    }

    public partial class Vendedor
    {
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
    }

    public partial class OrigemPorCancelamentoEstoque
    {
        public long IdEstoqueOrigem { get; set; }
    }

    public partial class RestricoesVeiculo
    {
        public string CodigoTipoRestricao { get; set; }
        public string TipoRestricao { get; set; }
    }

    public partial class SaidaEstoque
    {
        public CancelamentoSaidaEstoque CancelamentoSaidaEstoque { get; set; }
        public string ChaveNotaFiscalRemessaSaida { get; set; }
        public string ChaveNotaFiscalSaida { get; set; }
        public string ChaveNotaFiscalServicoSaida { get; set; }
        public Comprador Comprador { get; set; }
        public string CpfOperadorResponsavel { get; set; }
        public DateTimeOffset DataHora { get; set; }
        public DateTimeOffset DataHoraEnvioNotaFiscalRemessaSaida { get; set; }
        public DateTimeOffset DataHoraEnvioNotaFiscalSaida { get; set; }
        public DateTimeOffset DataHoraEnvioNotaFiscalServicoSaida { get; set; }
        public string Motivo { get; set; }
        public long NumeroTermoSaidaEstoque { get; set; }
    }

    public partial class CancelamentoSaidaEstoque
    {
        public DateTimeOffset DataHoraCancelamentoSaidaEstoque { get; set; }
        public long IdEstoqueGeradoNoCancelamentoSaida { get; set; }
    }
}
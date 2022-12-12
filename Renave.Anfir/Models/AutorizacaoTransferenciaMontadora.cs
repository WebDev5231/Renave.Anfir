using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class AutorizacaoTransferenciaMontadora
    {
        public string chassi { get; set; }
        public string cnpjEstabelecimentoAutorizador { get; set; }
        public string cnpjEstabelecimentoDestino { get; set; }
        public string cpfOperadorResponsavelAutorizacao { get; set; }
        public string cpfOperadorResponsavelCancelamento { get; set; }
        public string cpfOperadorResponsavelTransferencia { get; set; }
        public DateTimeOffset dataHoraAutorizacao { get; set; }
        public DateTimeOffset dataHoraCancelamento { get; set; }
        public DateTimeOffset dataHoraTransferencia { get; set; }
        public string estadoAutorizacaoTransferencia { get; set; }
        public bool estoqueOrigemDeVeiculoZeroKm { get; set; }
        public bool estoqueOrigemEmIte { get; set; }
        public long id { get; set; }
        public long idEstoque { get; set; }
        public long idEstoqueGerado { get; set; }
        public string nomeEstabelecimentoAutorizador { get; set; }
        public bool paraCancelamentoDeTransferencia { get; set; }
        public string placaVeiculo { get; set; }
    }
}
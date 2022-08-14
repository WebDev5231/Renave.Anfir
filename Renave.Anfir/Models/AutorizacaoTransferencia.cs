using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class AutorizacaoTransferencia
    {
        public string chassi { get; set; }
        public string cnpjEstabelecimentoAutorizador { get; set; }
        public string cnpjEstabelecimentoDestino { get; set; }
        public string cpfOperadorResponsavelAutorizacao { get; set; }
        public string cpfOperadorResponsavelCancelamento { get; set; }
        public string cpfOperadorResponsavelTransferencia { get; set; }
        public DateTimeOffset? dataHoraAutorizacao { get; set; }
        public DateTimeOffset? dataHoraCancelamento { get; set; }
        public DateTimeOffset? dataHoraTransferencia { get; set; }
        public string estadoAutorizacaoTransferencia { get; set; }
        public bool estoqueOrigemDeVeiculoZeroKm { get; set; }
        public bool estoqueOrigemEmIte { get; set; }
        public int id { get; set; }
        public int idEstoque { get; set; }
        public int idEstoqueGerado { get; set; }
        public string nomeEstabelecimentoAutorizador { get; set; }
        public bool paraCancelamentoDeTransferencia { get; set; }
        public string placaVeiculo { get; set; }
    }
}
using Renave.Anfir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class EstoqueVeiculoStatus
    {
        public List<EntradasEstoqueIte> DadosCompletos { get; set; }
        public List<Estoque> ChassisNaoEncontrados { get; set; }
    }

}
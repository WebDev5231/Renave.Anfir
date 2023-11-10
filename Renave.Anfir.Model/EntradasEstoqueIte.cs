using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Model
{
    [Table("EntradasEstoqueIte")]
    public class EntradasEstoqueIte
    {
        [Key]
        public short Id { get; set; }
        public long ID_Empresa { get; set; }
        public string Chassi { get; set; }
        public string ChaveNotaFiscalRemessa { get; set; }
        public string CpfOperadorResponsavel { get; set; }
        public string NomeCliente { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Siafi { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public decimal ValorProduto { get; set; }
        public int QuilometragemHodometro { get; set; }
        public DateTime DataMedicaoHodometro { get; set; }
        public string CodigoClienteMontadora { get; set; }
        public string DataEntradaEstoque { get; set; }
    }
}

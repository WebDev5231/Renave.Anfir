using System;

namespace Renave.Anfir.Models
{
    public class Cadastro
    {
        public int ID_Empresa { get; set; }
        public int? ID_Grupo { get; set; }
        public int ID_Publico { get; set; }
        public int ID_Sub_Publico { get; set; }
        public bool Filial { get; set; }
        public bool Homologada { get; set; }
        public string CNPJ { get; set; }
        public string Niev { get; set; }
        public string Razao { get; set; }
        public string Fantasia { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public int? ID_Cidade { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string Fax { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public string ProdutosHomologados { get; set; }
        public string Contato1 { get; set; }
        public string Departamento1 { get; set; }
        public string Telefone1 { get; set; }
        public string Email1 { get; set; }
        public string Contato2 { get; set; }
        public string Departamento2 { get; set; }
        public string Telefone2 { get; set; }
        public string Email2 { get; set; }
        public decimal? Taxa_Assoc { get; set; }
        public int? Insert_Grat { get; set; }
        public decimal? Taxa_Extra { get; set; }
        public string Data_Assoc { get; set; }
        public decimal Saldo { get; set; }
        public DateTime? Atualizacao { get; set; }
        public int? porteempresa { get; set; }
        public int? situacao { get; set; }
        public string contatorenavam { get; set; }
        public string departamentorenavam { get; set; }
        public string telefonerenavam { get; set; }
        public string emailrenavam { get; set; }
        public string email_solicitacoes { get; set; }
        public int? quant_funcio { get; set; }
        public string cpf { get; set; }
        public string emailfinanceiro { get; set; }
        public string contatofinanceiro { get; set; }
        public string pedidoinsercao { get; set; }
        public decimal? txtvalorpi { get; set; }
        public string status_homologacao { get; set; }
        public string diavencto { get; set; }
        public string foto_empresa { get; set; }
        public int? id_cat_assoc { get; set; }
        public DateTime? data_cadastro { get; set; }
        public int? cod_selo { get; set; }
        public string Data_Desliga { get; set; }
        public string NomeResponsavelLegal { get; set; }
        public string EmailResponsavelLegal { get; set; }
        public string RGResponsavelLegal { get; set; }
        public string CPFResponsavelLegal { get; set; }
        public int? ID_sub_publico_negociacao { get; set; }
        public int? diaBoleto { get; set; }
        public string contatoExportador { get; set; }
        public string departamentoExportador { get; set; }
        public string telefoneExportador { get; set; }
        public string emailExportador { get; set; }
        public string DataFundacao { get; set; }
    }
}

using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renave.Anfir.Model
{
    [Table("empresaRenaveCertificado")]
    public class EmpresaRenaveCertificado
    {
        [ExplicitKey]
        public int ID_Empresa { get; set; }
        public string Data_inclusao { get; set; }
        public string CertificadoFileName { get; set; }
        public string CertificadoPassword { get; set; }
    }
}

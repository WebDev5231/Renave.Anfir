using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class TransformadoCancelamento
    {
        public int ID_Empresa { get; set; }
        public string cpfOperadorResponsavel { get; set; }
        public int idAutorizacao { get; set; }
    }
}
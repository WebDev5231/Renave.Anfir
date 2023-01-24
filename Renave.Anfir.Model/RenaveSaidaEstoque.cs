using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renave.Anfir.Model
{
    public class RenaveSaidaEstoque
    {
        public long Id { get; set; }
        public int ID_Empresa { get; set; }
        public string Chassi { get; set; }
        public string CpfOperadorResponsavel { get; set; }
        public string IteOuMontadora { get; set; }
        public DateTime DataHora { get; set; }
    }
}

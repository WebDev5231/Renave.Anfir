using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renave.Anfir.Models
{
    public class PdfAtpv
    {
        public string numeroAtpv { get; set; }
        public string pdfAtpvBase64 { get; set; }
        public string xmlAtpvBase64 { get; set; }
    }
}
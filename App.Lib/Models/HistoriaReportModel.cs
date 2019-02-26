using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.Models
{
    public class HistoriaReportModel
    {
        public Historia Historia { get; set; }
        public bool AceitarHistoria { get; set; }
        public List<HistoriaReporteAbuso> Reportes { get; set; }
    }
}
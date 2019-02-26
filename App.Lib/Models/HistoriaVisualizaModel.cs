using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.Models
{
    public class HistoriaVizualizaModel
    {
        public Historia Historia { get; set; }
        public IList<HistoriaMidia> Midias { get; set; }
    }
}

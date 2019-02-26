using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Models
{
    public class InfoSlotsDecisaoModel
    {
        public int SlotsOcupados { get; set; }
        public int SlostsOcupadosInternos { get; set; }
        public int SlotsTotais { get; set; }
        public int Menor18 { get; set; }
        public int Entre18_35 { get; set; }
        public int Entre35_60 { get; set; }
        public int Maior60 { get; set; }
        public int Homens { get; set; }
        public int Mulheres { get; set; }
        public decimal Media { get; set; }
        public int NotaMinima { get; set; }
    }
}

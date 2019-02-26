using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.Models
{
    public class InformacoesIndicadoModel
    {
        public Indicado Indicado { get; set; }
        public int TotalIndicacoes { get; set; }
        public int Aprovadas { get; set; }
        public int Recusadas { get; set; }
        public int Moderadas { get; set; }
        public List<HistoriaCategoria> Qualidades { get; set; }
        public string LinkGaleria { get; set; }
        public List<CidadeParticipante> CidadesParticipantes { get; set; }
    }
}

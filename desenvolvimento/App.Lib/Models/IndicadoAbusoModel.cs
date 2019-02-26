using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.Models
{
    public class IndicadoAbusoModel
    {
        public List<HistoriaReportModel> HistoriasReportadas { get; set; }
        public bool RemoverGaleria { get; set; }
        public InformacoesIndicadoModel Informacoes { get; set; }
    }
}
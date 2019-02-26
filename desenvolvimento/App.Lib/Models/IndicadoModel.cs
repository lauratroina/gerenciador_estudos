using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.Models
{
    public class IndicadoModel
    {
        public IList<Estado> Estados { get; set; }
        public IList<Parentesco> Parentesco { get; set; }
        public Indicado Indicado { get; set; }
        public Historia Historia { get; set; }
        public HistoriaCategoria Categoria { get; set; }
        public bool Estrangeiro { get; set; }
        public bool Responsavel { get; set; }
        public string Codigo { get; set; }
        public string UrlResponsavel = "autorizacao";
        public UploadFotoModel IndicadoFotoInfo { get; set; }
    }
}

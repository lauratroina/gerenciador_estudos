using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class HistoriaModel
    {
        public Historia Historia { get; set; }

        public IList<HistoriaMidia> HistoriaMidia { get; set; }

        public IList<RecusaMotivoModel> RecusaMotivos { get; set; }

        public HistoriaModeracao HistoriaModeracao { get; set; }
        public IList<HistoriaCategoriaModel> Categorias { get; set; }
        public IList<ModeracaoPerguntaModel> Perguntas { get; set; }
        public InformacoesIndicadoModel InfoIndicado { get; set; }
    }
}
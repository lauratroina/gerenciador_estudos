using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class IndicadoInternoModel
    {
        public IndicadoInterno IndicadoInterno { get; set; }

        public IList<DocumentoTipo> DocumentoTipos { get; set; }
        public IList<Estado> DocumentoEstados { get; set; }

        public IList<Cidade> Cidades { get; set; }

        public IList<Estado> Estados { get; set; }

        public IList<Pais> Paises { get; set; }

        public IList<CidadeParticipante> CidadesParticipante { get; set; }

        public IList<HistoriaCategoria> HistoriasCategoria { get; set; }

        public IList<IndicadoInternoCategoria> Categorias { get; set; }
    }
}
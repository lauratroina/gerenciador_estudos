using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class RealocacaoModel
    {
        public CidadeParticipante Cidade { get; set; }

        public IList<TipoRealocacaoModel> Tipos { get; set; }
    }

    public class TipoRealocacaoModel
    {
        public int Quantidade { get; set; }
        public int Lugares { get; set; }
        public bool Interno { get; set; }
        public IList<CidadeRealocacaoModel> Cidades { get; set; }
    }

    public class CidadeRealocacaoModel
    {
        public CidadeParticipante Cidade { get; set; }

        public int Quantidade { get; set; }
        public int Lugares { get; set; }

        public bool Realoca { get; set; }

        public IList<IndicadoRealocacaoModel> Indicados { get; set; }
    }

    public class IndicadoRealocacaoModel
    {
        public int ID { get; set; }
        public bool Realoca { get; set; }
        public bool Condutor { get; set; }
        public string Nome { get; set; }
    }
}
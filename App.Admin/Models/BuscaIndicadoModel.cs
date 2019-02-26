using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class BuscaIndicadoModel
    {
        public int Condutor { get; set; }
        public string DocumentoNumero { get; set; }
        public decimal Nota { get; set; }
        public int CidadeParticipanteID { get; set; }
        public bool mostraTodos { get; set; }
        public IList<CidadeParticipante> CidadesParticipantes { get; set; }
    }
}
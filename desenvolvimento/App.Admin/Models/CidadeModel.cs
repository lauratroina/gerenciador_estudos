using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class CidadeModel
    {
        public CidadeParticipante CidadeParticipante { get; set; }

        public bool AtivoOriginal{ get; set; }

        public IList<Cidade> Cidades { get; set; }

        public IList<Estado> Estados { get; set; }
    }
}
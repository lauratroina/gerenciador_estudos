using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Admin.Models
{
    public class HistoriaRecusaModel
    {
        public Historia Historia { get; set; }

        public IList<HistoriaMidia> HistoriaMidia { get; set; }

        public IList<RecusaMotivoModel> RecusaMotivos { get; set; }
    }
}
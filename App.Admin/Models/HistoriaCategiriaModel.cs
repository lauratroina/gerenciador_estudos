using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class HistoriaCategoriaModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Checked { get; set; }
        public decimal Valor { get; set; }
        public int Ordem { get; set; }
    }
}
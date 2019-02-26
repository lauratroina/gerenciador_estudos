using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class ModeracaoRespostaModel
    {
        public int ID { get; set; }
        public int ModeracaoPerguntaID { get; set; }
        public string Texto { get; set; }
        public decimal Valor { get; set; }
        public bool Checked { get; set; }
    }
}
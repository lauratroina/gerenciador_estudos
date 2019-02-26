using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class ModeracaoPerguntaModel
    {
        public int ID { get; set; }
        public string Texto { get; set; }
        public char Tipo { get; set; }
        public int ModeracaoRespostaID { get; set; }
        public IList<ModeracaoRespostaModel> Respostas { get; set; }
    }
}
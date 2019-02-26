using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Admin.Models
{
    public class RecusaMotivoModel
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public bool Checked { get; set; }
    }
}
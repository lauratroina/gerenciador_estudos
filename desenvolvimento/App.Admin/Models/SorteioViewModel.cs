using App.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Models
{
    public class SorteioViewModel
    {
        public IList<int> MateriasIDs { get; set; }
        public bool Favoritas;
        public IList<Materia> Materias { get; set; }
        public IList<SelectListItem> MateriasAsSelectList
        {
            get
            {
                return Materias.Select(x => new SelectListItem { Text = x.Nome, Value = x.ID.ToString() }).ToList();
            }
        }

    }
}
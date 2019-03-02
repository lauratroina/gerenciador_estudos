using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Lib.Entity
{
    public class Carta
    {
        public int ID { get; set; }
        public int MateriaID { get; set; }
        public string TextoFrente { get; set; }
        public string TextoVerso { get; set; }
        public bool Status { get; set; }
        public bool Favorita { get; set; }
        public Materia Materia { get; set; }
        public IList<Materia> Materias { get; set; }
        public IList<SelectListItem> MateriasAsSelectList
        {
            get
            {
                return Materias != null? Materias.Select(x => new SelectListItem { Text = x.Nome, Value = x.ID.ToString(), Selected = MateriaID == x.ID }).ToList():null;
            }
        }
        public bool InserirProxima { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Admin.Models
{
    public class InfoExportacaoModel
    {
        private bool _SomenteAtivos;
        private string _Area;
        private List<string> _Areas;

        public InfoExportacaoModel()
        {
            //adiciona as áreas
            List<string> areas = new List<string>();
            areas.Add("Site");
            areas.Add("Rota");
            areas.Add("Participantes");
            Areas = areas;

            SomenteAtivos = true;
        }

        public bool SomenteAtivos
        {
            set { _SomenteAtivos = value; }
            get { return _SomenteAtivos; }
        }

        public List<string> Areas
        {
            set { _Areas = value; }
            get { return _Areas; }
        } 
        public string Area
        {
            set { _Area = value; }
            get { return _Area; }
        } 
    }
}
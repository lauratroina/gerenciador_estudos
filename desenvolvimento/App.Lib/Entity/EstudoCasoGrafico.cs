using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BradescoNext.Lib.Entity
{
    public class EstudoCasoGrafico
    {
        public int ID { get; set; }
        public int EstudoCasoID { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string Widget { get; set; }
        public string Q1Titulo { get; set; }
        public string Q1SubTitulo { get; set; }
        public string Q1Descricao { get; set; }
        public string Q1Imagem { get; set; }
        public int Q2Percentagem { get; set; }
        public string Q2Descricao { get; set; }
        public string Q3Titulo { get; set; }
        public string Q3SubTitulo { get; set; }
        public string Q3Descricao { get; set; }
        public string Q4Titulo { get; set; }
        public string Q4SubTitulo { get; set; }
        public string Q4Descricao { get; set; }


        public HttpPostedFileBase Q1ImagemArquivo { get; set; }
    }
}

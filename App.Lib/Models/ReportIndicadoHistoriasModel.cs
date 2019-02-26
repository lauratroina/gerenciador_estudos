using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Util.Enumerator;
using BradescoNext.Lib.Util.Models;

namespace BradescoNext.Lib.Models
{
    public class ReportIndicadoHistoriasModel
    {
        public class ReportIndicado
        {
            public int ID { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string CidadeUF { get; set; }

            public int qtdIndicacoes { get; set; }
            public int qtdHistoriasAprovadas { get; set; }
            public int qtdHistoriasRecusadas { get; set; }
            public int qtdHistoriasModeradas { get; set; }

            public DateTime DataNascimento { get; set; }
            public DateTime DataCadastro { get; set; }
            public DateTime DataModificacao { get; set; }
            public string ModificadoPor { get; set; }
            public string ParticiparEm { get; set; }
        }
        public class ReportIndicadoQualidade
        {
            public int QualidadeID { get; set; }
            public int IndicadoID { get; set; }
            public string QualidadeNome { get; set; }
        }
        public class reportIndicadoHistoria
        {
            public int HistoriaID { get; set; }
            public int IndicadoID { get; set; }
            public string TituloHistoria { get; set; }
            public string Categoria { get; set; }
            public string IndicadorNome { get; set; }
            public string TextoHistoria { get; set; }
            public string NotaHistoria { get; set; }
            public string StatusIndicado { get; set; }
            public string StatusTriagem { get; set; }
            public string StatusModeracao { get; set; }
        }

        public List<ReportIndicado> rptIndicado { get; set; }
        public List<ReportIndicadoQualidade> rptIndicadoQualidade { get; set; }
        public List<reportIndicadoHistoria> rptIndicadoHistoria { get; set; }
    }
}

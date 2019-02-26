using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Util;
using BradescoNext.Lib.Util.Enumerator;

namespace BradescoNext.Lib.Models
{
    public class OverviewModel
    {
        public List<DadoGraficoModel> Historias { get; set; }
        public List<DadoGraficoModel> Triadas { get; set; }
        public DateTime? DataMin { get; set; }

        public string HistoriasGrafico
        {
            get
            {   
                string retorno = "";
                if (DataMin != null)
                {
                    DateTime cont = DataMin.Value.AddDays(-DataMin.Value.Day + 1);
                    DateTime max = DateTime.Now;

                    if (max > ConfiguracaoAppUtil.GetAsDateTime(enumConfiguracaoGeral.dataEvento))
                    {
                        max = ConfiguracaoAppUtil.GetAsDateTime(enumConfiguracaoGeral.dataEvento);
                    }
                    if (Historias.Max(p => p.Data) > max)
                    {
                        max = Historias.Max(p => p.Data);
                    }
                    if (Triadas.Max(p => p.Data) > max)
                    {
                        max = Triadas.Max(p => p.Data);
                    }

                    if (max.Day > 15)
                    {
                        max = max.AddMonths(1);
                        max = new DateTime(max.Year, max.Month, 1);
                    }
                    else
                    {
                        max = new DateTime(max.Year, max.Month, 15);
                    }

                    while (cont <= max)
                    {
                        retorno += "{ day: \'" + cont.ToString("yyyy-MM-dd") + "\', historias: " + Historias.Sum(p => (p.Data <= cont) ? p.Quantidade : 0) + ", triadas: " + Triadas.Sum(p => (p.Data <= cont) ? p.Quantidade : 0) + " }";
                        if (cont < max)
                        {
                            retorno += ",";
                        }

                        if (cont.Day == 1)
                            cont = new DateTime(cont.Year, cont.Month, 15);
                        else
                        {
                            cont = cont.AddMonths(1);
                            cont = new DateTime(cont.Year, cont.Month, 1);
                        }
                    }
                }
                return retorno;
            }
        }

        public int TotalHistorias { get; set; }
        public int TotalIndicados { get; set; }
        public int AprovadasTriagem { get; set; }
        public int RessalvasTriagem { get; set; }
        public int RecusadasTriagem { get; set; }
        public int PendentesTriagem { get; set; }
        public int PendentesModeracao { get; set; }
        public int Galeria { get; set; }
        public int Moderadas { get; set; }
        public int TotalHistoriasIndicados { get; set; }
        public int TotalHistoriasRecusadas { get; set; }
        public int TotalHistoriasPendentes { get; set; }
        public double PercentualModeracao { get; set; }
        public int QuantidadeMax { get; set; }
        public int QtdeUsuarioTriagem { get; set; }
        public int QtdeUsuarioModerador { get; set; }
        public int QtdeUsuarioSupervisor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.DAL
{
    public interface IHistoriaReporteAbusoDAL
    {
        Int32 TotalRegistros { get; set; }
        void Atualizar(HistoriaReporteAbuso entidade);
        int Inserir(HistoriaReporteAbuso entidade);
        IList<HistoriaReporteAbuso> ListarReportesHistoria(int historiaID);
        void InativarReporte(HistoriaReporteAbuso entidade);
        HistoriaReporteAbuso Carregar(int historiaID, string email);
    }
}

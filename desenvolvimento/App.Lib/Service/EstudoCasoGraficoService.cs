using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BradescoNext.Lib.Service
{
    public class EstudoCasoGraficoService
    {        
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IEstudoCasoGraficoDAL dal = new EstudoCasoGraficoADO();

        internal EstudoCasoGrafico CarregarParaEstudo(int estudoCasoID)
        {
            return dal.CarregarParaEstudo(estudoCasoID);
        }
    }
}

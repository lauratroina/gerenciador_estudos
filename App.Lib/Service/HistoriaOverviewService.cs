using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Models;

namespace BradescoNext.Lib.Service
{
    public class HistoriaOverviewService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IHistoriaDAL dal = new HistoriaADO();

        public OverviewModel CarregarDadosOverview()
        {
            return dal.CarregarDadosOverview();
        }


    }
}

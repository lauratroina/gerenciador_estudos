using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;

namespace BradescoNext.Lib.Service
{
    public class ParentescoService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IParentescoDAL dal = new ParentescoADO();

        public IList<Parentesco> Listar()
        {
            return dal.Listar();
        }
        public IList<Parentesco> Listar(bool somenteAtivos)
        {
            return dal.Listar(somenteAtivos);
        } 
    }
}

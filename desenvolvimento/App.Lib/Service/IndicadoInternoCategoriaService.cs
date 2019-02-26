using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Util;

namespace BradescoNext.Lib.Service
{
    public class IndicadoInternoCategoriaService
    {
        private IIndicadoInternoCategoriaDAL dal = new IndicadoInternoCategoriaADO();
        public IList<IndicadoInternoCategoria> Listar()
        {
            return dal.Listar();
        }
    }
}

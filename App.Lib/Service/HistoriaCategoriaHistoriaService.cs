using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Lib.Service
{
    public class HistoriaCategoriaHistoriaService
    {

        private IHistoriaCategoriaHistoriaDAL dal = new HistoriaCategoriaHistoriaADO();

        public void Inserir(int HistoriaID, int HistoriaCategoriaID)
        {
            dal.Inserir(HistoriaID, HistoriaCategoriaID);
        }

        public void Inserir(int HistoriaID, string ListaHistoriaCategoriaID)
        {
            dal.Inserir(HistoriaID, ListaHistoriaCategoriaID);
        }

        public void Excluir(int HistoriaID, int HistoriaCategoriaID)
        {
            dal.Excluir(HistoriaID, HistoriaCategoriaID);
        }

        public void Excluir(int historiaID)
        {
            dal.Excluir(historiaID);
        }

        public IList<HistoriaCategoriaHistoria> Listar(int historiaID)
        {
            return dal.Listar(historiaID);
        }
    }
}

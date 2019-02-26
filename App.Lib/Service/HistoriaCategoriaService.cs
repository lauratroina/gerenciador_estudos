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
    public class HistoriaCategoriaService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IHistoriaCategoriaDAL dal = new HistoriaCategoriaADO();

        public RetornoModel Salvar(HistoriaCategoria historiaCat)
        {
            if (historiaCat.ID > 0)
            {
                dal.Atualizar(historiaCat);
            }
            else
            {
                dal.Inserir(historiaCat);
            }

            return new RetornoModel() { Sucesso = true, Mensagem = "OK!" };
        }

        public HistoriaCategoria Carregar(int id)
        {
            return dal.Carregar(id);
        }

        public IList<HistoriaCategoria> Listar()
        {
            return dal.Listar();
        }

        public IList<HistoriaCategoria> Listar(bool somenteAtivos)
        {
            return dal.Listar(somenteAtivos);
        }

        public IList<HistoriaCategoria> Listar(int indicadoID)
        {
            return dal.Listar(indicadoID);
        }

        public IList<HistoriaCategoria> Listar(Int32 skip, Int32 take, string palavraChave)
        {
            IList<HistoriaCategoria> lista = dal.Listar(skip, take, palavraChave);
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }
    }
}

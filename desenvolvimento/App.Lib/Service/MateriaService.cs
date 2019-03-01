using App.Lib.DAL.ADO;
using App.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Lib.Service
{
    public class MateriaService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private MateriaADO _dao = new MateriaADO();

        public Materia Carregar(int id)
        {
            return _dao.Carregar(id);
        }

        public void Salvar(Materia entidade)
        {
            if (entidade.ID > 0)
            {
                _dao.Atualizar(entidade);
            }
            else
            {
                _dao.Inserir(entidade);
            }
        }

        public IList<Materia> Listar()
        {
            return _dao.Listar();
        }

        public IList<Materia> Listar(Int32 skip, Int32 take, string palavraChave)
        {
            IList<Materia> lista = _dao.Listar(skip, take, palavraChave);
            TotalRegistros = _dao.TotalRegistros;
            return lista;
        }

        public void Deletar(int id)
        {
            _dao.Deletar(id);
        }
    }
}

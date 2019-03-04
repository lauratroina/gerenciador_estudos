using App.Lib.DAL.ADO;
using App.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Lib.Service
{
    public class CartaService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private CartaADO _dao = new CartaADO();

        public Carta Carregar(int id)
        {
            return _dao.Carregar(id);
        }

        public void Salvar(Carta entidade)
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

        public IList<Carta> Listar()
        {
            return _dao.Listar();
        }

        public IList<Carta> Listar(Int32 skip, Int32 take, string palavraChave)
        {
            IList<Carta> lista = _dao.Listar(skip, take, palavraChave);
            TotalRegistros = _dao.TotalRegistros;
            return lista;
        }

        public void Deletar(int id)
        {
            _dao.Deletar(id);
        }

        public void Favoritar(int id, bool favorito)
        {
            _dao.Favoritar(id, favorito);
        }

        public void MudarStatus(int id, bool status)
        {
            _dao.MudarStatus(id, status);
        }

        public Guid GerarSorteio(IList<int> ids, bool favoritas)
        {
            return _dao.GerarSorteio(ids, favoritas);
        }

        public Sorteio Carregar(Guid Identificador)
        {
            return _dao.Carregar(Identificador);
        }

        public void ApagaSorteio(int id)
        {
            _dao.ApagaSorteio(id);
        }


    }
}

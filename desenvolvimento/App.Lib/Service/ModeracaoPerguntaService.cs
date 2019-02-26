using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.DAL; 
using BradescoNext.Lib.DAL.ADO; 
using BradescoNext.Lib.Entity; 

namespace BradescoNext.Lib.Service 
{ 
    public class ModeracaoPerguntaService 
    { 

		private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IModeracaoPerguntaDAL dal = new ModeracaoPerguntaADO();

        public IList<ModeracaoPergunta> Listar()
        {
            IList<ModeracaoPergunta> lista = dal.Listar();
            return lista;
        }

        public ModeracaoPergunta Carregar(int id) 
        { 
            return dal.Carregar(id);
        }
    } 
} 

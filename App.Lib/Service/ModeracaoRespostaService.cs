using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.DAL; 
using BradescoNext.Lib.DAL.ADO; 
using BradescoNext.Lib.Entity; 

namespace BradescoNext.Lib.Service 
{ 
    public class ModeracaoRespostaService 
    { 

		private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IModeracaoRespostaDAL dal = new ModeracaoRespostaADO();

        public IList<ModeracaoResposta> Listar(int moderacaoPerguntaID)
        {
            IList<ModeracaoResposta> lista = dal.Listar(moderacaoPerguntaID);
            return lista;
        }

        public ModeracaoResposta Carregar(int id) 
        { 
            return dal.Carregar(id);
        }
    } 
} 

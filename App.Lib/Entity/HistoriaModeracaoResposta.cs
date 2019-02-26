using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Entity
{
    [Serializable] 
    public class HistoriaModeracaoResposta
    {
        private int _ID;
        private decimal _Valor;
        private int _HistoriaModeracaoID;
        private int _ModeracaoPerguntaID;
        private int? _HistoriaCategoriaID;
        private int? _ModeracaoRespostaID;

        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        public int HistoriaModeracaoID
        {
            set { _HistoriaModeracaoID = value; }
            get { return _HistoriaModeracaoID; }
        }

        public int ModeracaoPerguntaID
        {
            set { _ModeracaoPerguntaID = value; }
            get { return _ModeracaoPerguntaID; }
        }

        public int? HistoriaCategoriaID
        {
            set { _HistoriaCategoriaID = value; }
            get { return _HistoriaCategoriaID; }
        }

        public int? ModeracaoRespostaID
        {
            set { _ModeracaoRespostaID = value; }
            get { return _ModeracaoRespostaID; }
        }

        public decimal Valor
        {
            set { _Valor = value; }
            get { return _Valor; }
        }
    }
}

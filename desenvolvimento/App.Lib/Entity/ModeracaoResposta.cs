using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Entity
{
    [Serializable] 
    public class ModeracaoResposta
    {
        private int _ID;
        private int _ModeracaoPerguntaID;
        private string _Texto;
        private decimal _Valor;
        private int _Ordem;
        private bool _Inativo;

        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        public int ModeracaoPerguntaID
        {
            set { _ModeracaoPerguntaID = value; }
            get { return _ModeracaoPerguntaID; }
        }

        public string Texto
        {
            set { _Texto = value; }
            get { return _Texto; }
        }

        public int Ordem
        {
            set { _Ordem = value; }
            get { return _Ordem; }
        }

        public bool Inativo
        {
            set { _Inativo = value; }
            get { return _Inativo; }
        }
        public decimal Valor
        {
            set { _Valor = value; }
            get { return _Valor; }
        }
    }
}

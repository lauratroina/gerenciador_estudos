using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity.Enumerator;

namespace BradescoNext.Lib.Entity
{
    [Serializable] 
    public class ModeracaoPergunta
    {
        private int _ID;
        private char _Tipo;
        private string _Texto;
        private int _Ordem;
        private bool _Inativo;

        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }
        public string Texto
        {
            set { _Texto = value; }
            get { return _Texto; }
        }

        public bool Inativo
        {
            set { _Inativo = value; }
            get { return _Inativo; }
        }

        public int Ordem
        {
            set { _Ordem = value; }
            get { return _Ordem; }
        }
        public char TipoDB
        {
            set { _Tipo = value; }
            get { return _Tipo; }
        }
        public enumTipoPerguntaModeracao Tipo
        {
            set { _Tipo = value.ValueAsChar(); }
            get { try { return EnumExtensions.FromInt<enumTipoPerguntaModeracao>(_Tipo); } catch { return enumTipoPerguntaModeracao.simples; } }
        }
        
    }
}

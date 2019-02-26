using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Entity
{
    [Serializable] 
    public class HistoriaModeracao
    {
        private int _ID;
        private decimal _Nota;
        private decimal _NotaPonderada;
        private int _HistoriaID;
        private int _UsuarioID;
        private DateTime _DataInicioAvaliacao;
        private DateTime _DataFimAvaliacao;


        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        public decimal NotaPonderada
        {
            set { _NotaPonderada = value; }
            get { return _NotaPonderada; }
        }

        public decimal Nota
        {
            set { _Nota = value; }
            get { return _Nota; }
        }

        public int HistoriaID
        {
            set { _HistoriaID = value; }
            get { return _HistoriaID; }
        }


        public int UsuarioID
        {
            set { _UsuarioID = value; }
            get { return _UsuarioID; }
        }


        public DateTime DataInicioAvaliacao
        {
            set { _DataInicioAvaliacao = value; }
            get { return _DataInicioAvaliacao; }
        }


        public DateTime DataFimAvaliacao
        {
            set { _DataFimAvaliacao = value; }
            get { return _DataFimAvaliacao; }
        }


    }
}

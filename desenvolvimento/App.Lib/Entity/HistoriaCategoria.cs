using System;
using System.Text;
using System.Collections.Generic;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class HistoriaCategoria
    {
        private int _ID;
        private string _Nome;
        private int _Ordem;
        private bool _Inativo;
        private string _CorLabel;
        private string _CorFont;
        private decimal _Valor;


        public string CorLabel
        {
            set { _CorLabel = value; }
            get { return _CorLabel; }
        }
        public string CorFont
        {
            set { _CorFont = value; }
            get { return _CorFont; }
        }
        public decimal Valor
        {
            set { _Valor = value; }
            get { return _Valor; }
        }


        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }


        public string Nome
        {
            set { _Nome = value; }
            get { return _Nome; }
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

        public bool Ativo
        {
            set { _Inativo = !value; }
            get { return !_Inativo; }
        }

    }
}
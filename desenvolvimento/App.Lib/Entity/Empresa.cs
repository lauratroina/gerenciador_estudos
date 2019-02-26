using System;
using System.Text;
using System.Collections.Generic;

namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class Empresa
    {
        private int _ID;
        private string _Nome;
        private bool _Inativo;


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

        public bool Inativo
        {
            get { return _Inativo; }
            set { _Inativo = value; }
        }

        public bool Ativo
        {
            get { return !_Inativo; }
            set { _Inativo = !value; }
        }
    }
}
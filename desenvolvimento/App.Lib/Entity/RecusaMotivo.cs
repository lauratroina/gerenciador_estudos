using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class RecusaMotivo
    {
        private int _ID;
        private string _Descricao;
        private bool _Inativo;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public bool Inativo
        {
            get { return _Inativo; }
            set { _Inativo = value; }
        }
    }
}

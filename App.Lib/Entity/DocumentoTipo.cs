using System;
using System.Text;
using System.Collections.Generic;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class DocumentoTipo
    {
        private int _ID;
        private string _Nome;



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



    }
}
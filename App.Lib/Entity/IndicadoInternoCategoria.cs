using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class IndicadoInternoCategoria
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
            get { return _Nome; }
            set { _Nome = value; }
        }
    }
}

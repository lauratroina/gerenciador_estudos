using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Entity
{
    [Serializable] 
    public class FaleConoscoDestino
    {
        private int _ID;
        private string _Nome;
        private string _Email;

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
        public string Email
        {
            set { _Email = value; }
            get { return _Email; }
        }
    }
}

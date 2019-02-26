using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class HistoriaRecusaMotivo
    {
        private int _ID;
        private int _RecusaMotivoID;
        private int _HistoriaID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int RecusaMotivoID
        {
            get { return _RecusaMotivoID; }
            set { _RecusaMotivoID = value; }
        }

        public int HistoriaID
        {
            get { return _HistoriaID; }
            set { _HistoriaID = value; }
        }
    }
}

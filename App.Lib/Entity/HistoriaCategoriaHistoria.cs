using System;
using System.Text;
using System.Collections.Generic;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class HistoriaCategoriaHistoria
    {
        private int _ID;
        private string _HistoriaID;
        private HistoriaCategoria _Categoria;

        private int _HistoriaCategoriaID;

        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        public int HistoriaCategoriaID
        {
            get { return _HistoriaCategoriaID; }
            set { _HistoriaCategoriaID = value; }
        }

        public string HistoriaID
        {
            get { return _HistoriaID; }
            set { _HistoriaID = value; }
        }

        public HistoriaCategoria Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; }
        }
    }
}
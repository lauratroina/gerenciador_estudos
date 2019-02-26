using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Entity
{
    [Serializable] 
    public class HistoriaReporteAbuso
    {
        private int _ID;
        private string _Nome;
        private string _Email;
        private int _HistoriaID;
        private bool _Inativo;
        private string _Mensagem;
        private DateTime _DataCadastro;

        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }
        public int HistoriaID
        {
            set { _HistoriaID = value; }
            get { return _HistoriaID; }
        }
        public bool Inativo
        {
            set { _Inativo = value; }
            get { return _Inativo; }
        }
        public string Mensagem
        {
            set { _Mensagem = value; }
            get { return _Mensagem; }
        }
        public DateTime DataCadastro
        {
            set { _DataCadastro = value; }
            get { return _DataCadastro; }
        }
        public string Email
        {
            set { _Email = value; }
            get { return _Email; }
        }
        public string Nome
        {
            set { _Nome = value; }
            get { return _Nome; }
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class FaleConosco
    {
        private int _ID;
        private string _Nome;
        private string _Email;
        private int _FaleConoscoAssuntoID;
        private string _Mensagem;
        private DateTime _DataCadastro;
        private FaleConoscoAssunto _FaleConoscoAssunto;

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


        public int FaleConoscoAssuntoID
        {
            set { _FaleConoscoAssuntoID = value; }
            get { return _FaleConoscoAssuntoID; }
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

        public FaleConoscoAssunto FaleConoscoAssunto
        {
            set { _FaleConoscoAssunto = value; if (_FaleConoscoAssunto.ID > 0) { FaleConoscoAssuntoID = _FaleConoscoAssunto.ID; } }
            get { return _FaleConoscoAssunto; }
        }

    }
}
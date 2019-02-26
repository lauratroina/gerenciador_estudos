using System;
using System.Text;
using System.Collections.Generic;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Util;
using BradescoNext.Lib.Enumerator;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class Newsletter
    {
        private int _ID;
        private string _Email;
        private string _Origem;
        private bool _Inativo;
        private DateTime _DataCadastro;


        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }


        public string Email
        {
            set { _Email = value; }
            get { return _Email; }
        }

        public string OrigemDB
        {
            set { _Origem = value; }
            get { return _Origem; }
        }

        public enumNewsletterOrigem Origem
        {
            set { _Origem = value.ValueAsString(); }
            get { try { return EnumExtensions.FromChar<enumNewsletterOrigem>(_Origem); } catch { return enumNewsletterOrigem.todas; } }
        }

        public bool Inativo
        {
            set { _Inativo = value; }
            get { return _Inativo; }
        }

        public DateTime DataCadastro
        {
            set { _DataCadastro = value; }
            get { return _DataCadastro; }
        }
        
    }
}
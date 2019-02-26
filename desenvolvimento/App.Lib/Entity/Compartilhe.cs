using System;
using System.Text;
using System.Collections.Generic;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class Compartilhe
    {
        private string _DeNome;
        private string _DeEmail;
        private string _ParaNome;
        private string _ParaEmail;
        private string _Url;

        public string DeNome
        {
            set { _DeNome = value; }
            get { return _DeNome; }
        }


        public string DeEmail
        {
            set { _DeEmail = value; }
            get { return _DeEmail; }
        }

        public string ParaNome
        {
            set { _ParaNome = value; }
            get { return _ParaNome; }
        }

        public string ParaEmail
        {
            set { _ParaEmail = value; }
            get { return _ParaEmail; }
        }

        public string Url
        {
            set { _Url = value; }
            get { return _Url; }
        }

        public string Uri
        {
            get 
            {
                if (string.IsNullOrEmpty(_Url)) 
                    return _Url; 
                var aux = _Url.RightOfIndexOf("://");
                if (aux != _Url)
                {
                    return aux.RightOfIndexOf("/");
                }
                else
                {
                    return _Url; 
                }
                
            }
        }

    }
}
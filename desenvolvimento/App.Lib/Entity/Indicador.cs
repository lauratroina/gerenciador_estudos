using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradescoNext.Lib.Entity
{
    public class Indicador
    {
        private int _ID;
        private string _FacebookID;
        private string _FacebookToken;
        private string _Email; 
        private string _Nome;
        private DateTime? _DataEnvioEmailContinuacao;
        private int _QuantidadeEmailContinuacao;
        private DateTime _DataCadastro;
        private DateTime _DataModificacao;


        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }
        public string FacebookID
        {
            set { _FacebookID = value; }
            get { return _FacebookID; }
        }
        public string FacebookToken
        {
            set { _FacebookToken = value; }
            get { return _FacebookToken; }
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
        public int QuantidadeEmailContinuacao
        {
            set { _QuantidadeEmailContinuacao = value; }
            get { return _QuantidadeEmailContinuacao; }
        }
        public DateTime? DataEnvioEmailContinuacao
        {
            set { _DataEnvioEmailContinuacao = value; }
            get { return _DataEnvioEmailContinuacao; }
        }
        public DateTime DataCadastro
        {
            set { _DataCadastro = value; }
            get { return _DataCadastro; }
        }
        public DateTime DataModificacao
        {
            set { _DataModificacao = value; }
            get { return _DataModificacao; }
        }
    }
}

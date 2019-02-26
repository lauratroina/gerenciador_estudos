using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class EditorialBlocoArquivo
    {
        private int _ID;
        private int _EditorialBlocoID;
        private string _Tipo;
        private string _Descricao;
        private string _NomeArquivo;
        private HttpPostedFileBase _NomeArquivoFile;
        private string _NomeVideo;
        private HttpPostedFileBase _NomeVideoFile;
        private string _YouTubeID;
        private int _UsuarioID;
        private Usuario _Usuario;
        private DateTime _DataCadastro;
        private DateTime _DataModificacao;
        private bool _Inativo;
        private int _Ordem;


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int EditorialBlocoID
        {
            get { return _EditorialBlocoID; }
            set { _EditorialBlocoID = value; }
        }
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string NomeArquivo
        {
            get { return _NomeArquivo; }
            set { _NomeArquivo = value; }
        }
        public HttpPostedFileBase NomeArquivoFile
        {
            get { return _NomeArquivoFile; }
            set { _NomeArquivoFile = value; }
        }
        public string NomeVideo
        {
            get { return _NomeVideo; }
            set { _NomeVideo = value; }
        }
        public HttpPostedFileBase NomeVideoFile
        {
            get { return _NomeVideoFile; }
            set { _NomeVideoFile = value; }
        }
        public string YouTubeID
        {
            get { return _YouTubeID; }
            set { _YouTubeID = value; }
        }
        public int UsuarioID
        {
            get { return _UsuarioID; }
            set { _UsuarioID = value; }
        }
        public Usuario Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public DateTime DataCadastro
        {
            get { return _DataCadastro; }
            set { _DataCadastro = value; }
        }
        public DateTime DataModificacao
        {
            get { return _DataModificacao; }
            set { _DataModificacao = value; }
        }
        public bool Inativo
        {
            get { return _Inativo; }
            set { _Inativo = value; }
        }
        public bool Ativo
        {
            set { _Inativo = !value; }
            get { return !_Inativo; }
        }
        public int Ordem
        {
            set { _Ordem = value; }
            get { return _Ordem; }
        }
    }
}

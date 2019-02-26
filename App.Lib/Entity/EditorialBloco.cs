using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class EditorialBloco
    {
        private int _ID;
        private int _EditorialID;
        private int _Tipo;
        private int _Disposicao;
        private int _Ordem;
        private string _Nome;
        private string _Texto;
        private string _TextoResumido;
        private int _Alinhamento;
        private string _NomeArquivo;
        private string _NomeArquivo1366;
        private string _NomeArquivo1280;
        private string _NomeArquivo1024;
        private string _NomeArquivo640;
        private string _NomeArquivo480;
        private HttpPostedFileBase _NomeArquivoFile;
        private HttpPostedFileBase _NomeArquivo1366File;
        private HttpPostedFileBase _NomeArquivo1280File;
        private HttpPostedFileBase _NomeArquivo1024File;
        private HttpPostedFileBase _NomeArquivo640File;
        private HttpPostedFileBase _NomeArquivo480File;
        private string _BotaoTitulo;
        private string _BotaoLink;
        private int _UsuarioID;
        private Usuario _Usuario;
        private DateTime _DataCadastro;
        private DateTime _DataModificacao;
        private bool _Inativo;
        private IList<EditorialBlocoArquivo> _ListaArquivos;


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int EditorialID
        {
            get { return _EditorialID; }
            set { _EditorialID = value; }
        }
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public int Disposicao
        {
            get { return _Disposicao; }
            set { _Disposicao = value; }
        }
        public int Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public string Texto
        {
            get { return _Texto; }
            set { _Texto = value; }
        }
        public string TextoResumido
        {
            get { return _TextoResumido; }
            set { _TextoResumido = value; }
        }
        public int Alinhamento
        {
            get { return _Alinhamento; }
            set { _Alinhamento = value; }
        }
        public string NomeArquivo
        {
            get { return _NomeArquivo; }
            set { _NomeArquivo = value; }
        }
        public string NomeArquivo1366
        {
            get { return _NomeArquivo1366; }
            set { _NomeArquivo1366 = value; }
        }
        public string NomeArquivo1280
        {
            get { return _NomeArquivo1280; }
            set { _NomeArquivo1280 = value; }
        }
        public string NomeArquivo1024
        {
            get { return _NomeArquivo1024; }
            set { _NomeArquivo1024 = value; }
        }
        public string NomeArquivo640
        {
            get { return _NomeArquivo640; }
            set { _NomeArquivo640 = value; }
        }
        public string NomeArquivo480
        {
            get { return _NomeArquivo480; }
            set { _NomeArquivo480 = value; }
        }
        public HttpPostedFileBase NomeArquivoFile
        {
            get { return _NomeArquivoFile; }
            set { _NomeArquivoFile = value; }
        }
        public HttpPostedFileBase NomeArquivo1366File
        {
            get { return _NomeArquivo1366File; }
            set { _NomeArquivo1366File = value; }
        }
        public HttpPostedFileBase NomeArquivo1280File
        {
            get { return _NomeArquivo1280File; }
            set { _NomeArquivo1280File = value; }
        }
        public HttpPostedFileBase NomeArquivo1024File
        {
            get { return _NomeArquivo1024File; }
            set { _NomeArquivo1024File = value; }
        }
        public HttpPostedFileBase NomeArquivo640File
        {
            get { return _NomeArquivo640File; }
            set { _NomeArquivo640File = value; }
        }
        public HttpPostedFileBase NomeArquivo480File
        {
            get { return _NomeArquivo480File; }
            set { _NomeArquivo480File = value; }
        }
        public string BotaoTitulo
        {
            get { return _BotaoTitulo; }
            set { _BotaoTitulo = value; }
        }
        public string BotaoLink
        {
            get { return _BotaoLink; }
            set { _BotaoLink = value; }
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

        public IList<EditorialBlocoArquivo> ListaArquivos
        {
            set { _ListaArquivos = value; }
            get { return _ListaArquivos; }
        }
    }
}

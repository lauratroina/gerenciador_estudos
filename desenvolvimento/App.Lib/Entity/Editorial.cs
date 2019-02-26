using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class Editorial
    {
        private int _ID;
        private string _IndicadoNome;
        private HttpPostedFileBase _IndicadoFotoFile;
        private string _IndicadoFoto;
        private DateTime _DataExibicao;
        private string _TopoTitulo;
        private string _TopoTexto;
        private string _TopoBotaoYouTubeID;
        private string _BaseTexto;
        private string _ConteudoHTML;
        private int _UsuarioID;
        private Usuario _Usuario;
        private DateTime _DataCadastro;
        private DateTime _DataModificacao;
        private bool _Inativo;
        private string _TopoImagem1920;
        private string _TopoImagem1366;
        private string _TopoImagem1280;
        private string _TopoImagem1024;
        private string _TopoImagem640;
        private string _TopoImagem480;
        private string _TopoVideo;
        private string _TopoBotaoVideo;
        private string _BaseImagem1920;
        private string _BaseImagem1366;
        private string _BaseImagem1280;
        private string _BaseImagem1024;
        private string _BaseImagem640;
        private string _BaseImagem480;
        private Editorial _EditorialAnterior;
        private string _Url;
        private string _MetaDescription;
        private string _MetaKeywords;
        private string _ImagemRedesSociais;
        private string _FacebookMetaTitle;
        private string _FacebookMetaDescription;
        private string _TwitterMetaTitle;
        private string _TwitterMetaDescription;

        private HttpPostedFileBase _TopoImagem1920File;
        private HttpPostedFileBase _TopoImagem1366File;
        private HttpPostedFileBase _TopoImagem1280File;
        private HttpPostedFileBase _TopoImagem1024File;
        private HttpPostedFileBase _TopoImagem640File;
        private HttpPostedFileBase _TopoImagem480File;
        private HttpPostedFileBase _TopoVideoMP4File;
        private HttpPostedFileBase _TopoVideoWEBMFile;
        private HttpPostedFileBase _TopoVideoOGVFile;
        private HttpPostedFileBase _TopoBotaoVideoFile;
        private HttpPostedFileBase _BaseImagem1920File;
        private HttpPostedFileBase _BaseImagem1366File;
        private HttpPostedFileBase _BaseImagem1280File;
        private HttpPostedFileBase _BaseImagem1024File;
        private HttpPostedFileBase _BaseImagem640File;
        private HttpPostedFileBase _BaseImagem480File;
        private HttpPostedFileBase _ImagemRedesSociaisFile;


        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string IndicadoNome
        {
            get { return _IndicadoNome; }
            set { _IndicadoNome = value; }
        }
        public string IndicadoFoto
        {
            get { return _IndicadoFoto; }
            set { _IndicadoFoto = value; }
        }
        public HttpPostedFileBase IndicadoFotoFile
        {
            get { return _IndicadoFotoFile; }
            set { _IndicadoFotoFile = value; }
        }
        public DateTime DataExibicao
        {
            get { return _DataExibicao; }
            set { _DataExibicao = value; }
        }
        public string TopoTitulo
        {
            get { return _TopoTitulo; }
            set { _TopoTitulo = value; }
        }
        public string TopoTexto
        {
            get { return _TopoTexto; }
            set { _TopoTexto = value; }
        }
        public string TopoBotaoYouTubeID
        {
            get { return _TopoBotaoYouTubeID; }
            set { _TopoBotaoYouTubeID = value; }
        }
        public string BaseTexto
        {
            get { return _BaseTexto; }
            set { _BaseTexto = value; }
        }
        public string ConteudoHTML
        {
            get { return _ConteudoHTML; }
            set { _ConteudoHTML = value; }
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

        public string TopoImagem1920
        {
            get { return _TopoImagem1920; }
            set { _TopoImagem1920 = value; }
        }
        public string TopoImagem1366
        {
            get { return _TopoImagem1366; }
            set { _TopoImagem1366 = value; }
        }
        public string TopoImagem1280
        {
            get { return _TopoImagem1280; }
            set { _TopoImagem1280 = value; }
        }
        public string TopoImagem1024
        {
            get { return _TopoImagem1024; }
            set { _TopoImagem1024 = value; }
        }
        public string TopoImagem640
        {
            get { return _TopoImagem640; }
            set { _TopoImagem640 = value; }
        }
        public string TopoImagem480
        {
            get { return _TopoImagem480; }
            set { _TopoImagem480 = value; }
        }
        public string TopoVideo
        {
            get { return _TopoVideo; }
            set { _TopoVideo = value; }
        }
        public string TopoVideoMP4
        {
            get { return string.IsNullOrWhiteSpace(_TopoVideo) ? "" : _TopoVideo + ".mp4"; }
        }
        public string TopoVideoOGV
        {
            get { return string.IsNullOrWhiteSpace(_TopoVideo) ? "" : _TopoVideo + ".ogv"; }
        }
        public string TopoVideoWEBM
        {
            get { return string.IsNullOrWhiteSpace(_TopoVideo) ? "" : _TopoVideo + ".webm"; }
        }
        public string TopoBotaoVideo
        {
            get { return _TopoBotaoVideo; }
            set { _TopoBotaoVideo = value; }
        }
        public string BaseImagem1920
        {
            get { return _BaseImagem1920; }
            set { _BaseImagem1920 = value; }
        }
        public string BaseImagem1366
        {
            get { return _BaseImagem1366; }
            set { _BaseImagem1366 = value; }
        }
        public string BaseImagem1280
        {
            get { return _BaseImagem1280; }
            set { _BaseImagem1280 = value; }
        }
        public string BaseImagem1024
        {
            get { return _BaseImagem1024; }
            set { _BaseImagem1024 = value; }
        }
        public string BaseImagem640
        {
            get { return _BaseImagem640; }
            set { _BaseImagem640 = value; }
        }
        public string BaseImagem480
        {
            get { return _BaseImagem480; }
            set { _BaseImagem480 = value; }
        }
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        public string MetaDescription
        {
            get { return _MetaDescription; }
            set { _MetaDescription = value; }
        }
        public string MetaKeywords
        {
            get { return _MetaKeywords; }
            set { _MetaKeywords = value; }
        }
        public string ImagemRedesSociais
        {
            get { return _ImagemRedesSociais; }
            set { _ImagemRedesSociais = value; }
        }
        public string FacebookMetaTitle
        {
            get { return _FacebookMetaTitle; }
            set { _FacebookMetaTitle = value; }
        }
        public string FacebookMetaDescription
        {
            get { return _FacebookMetaDescription; }
            set { _FacebookMetaDescription = value; }
        }
        public string TwitterMetaTitle
        {
            get { return _TwitterMetaTitle; }
            set { _TwitterMetaTitle = value; }
        }
        public string TwitterMetaDescription
        {
            get { return _TwitterMetaDescription; }
            set { _TwitterMetaDescription = value; }
        }

        public Editorial EditorialAnterior
        {
            get { return _EditorialAnterior; }
            set { _EditorialAnterior = value; }
        }

        public HttpPostedFileBase TopoImagem1920File
        {
            get { return _TopoImagem1920File; }
            set { _TopoImagem1920File = value; }
        }
        public HttpPostedFileBase TopoImagem1366File
        {
            get { return _TopoImagem1366File; }
            set { _TopoImagem1366File = value; }
        }
        public HttpPostedFileBase TopoImagem1280File
        {
            get { return _TopoImagem1280File; }
            set { _TopoImagem1280File = value; }
        }
        public HttpPostedFileBase TopoImagem1024File
        {
            get { return _TopoImagem1024File; }
            set { _TopoImagem1024File = value; }
        }
        public HttpPostedFileBase TopoImagem640File
        {
            get { return _TopoImagem640File; }
            set { _TopoImagem640File = value; }
        }
        public HttpPostedFileBase TopoImagem480File
        {
            get { return _TopoImagem480File; }
            set { _TopoImagem480File = value; }
        }
        public HttpPostedFileBase TopoVideoMP4File
        {
            get { return _TopoVideoMP4File; }
            set { _TopoVideoMP4File = value; }
        }
        public HttpPostedFileBase TopoVideoWEBMFile
        {
            get { return _TopoVideoWEBMFile; }
            set { _TopoVideoWEBMFile = value; }
        }
        public HttpPostedFileBase TopoVideoOGVFile
        {
            get { return _TopoVideoOGVFile; }
            set { _TopoVideoOGVFile = value; }
        }
        public HttpPostedFileBase TopoBotaoVideoFile
        {
            get { return _TopoBotaoVideoFile; }
            set { _TopoBotaoVideoFile = value; }
        }
        public HttpPostedFileBase BaseImagem1920File
        {
            get { return _BaseImagem1920File; }
            set { _BaseImagem1920File = value; }
        }
        public HttpPostedFileBase BaseImagem1366File
        {
            get { return _BaseImagem1366File; }
            set { _BaseImagem1366File = value; }
        }
        public HttpPostedFileBase BaseImagem1280File
        {
            get { return _BaseImagem1280File; }
            set { _BaseImagem1280File = value; }
        }
        public HttpPostedFileBase BaseImagem1024File
        {
            get { return _BaseImagem1024File; }
            set { _BaseImagem1024File = value; }
        }
        public HttpPostedFileBase BaseImagem640File
        {
            get { return _BaseImagem640File; }
            set { _BaseImagem640File = value; }
        }
        public HttpPostedFileBase BaseImagem480File
        {
            get { return _BaseImagem480File; }
            set { _BaseImagem480File = value; }
        }
        public HttpPostedFileBase ImagemRedesSociaisFile
        {
            get { return _ImagemRedesSociaisFile; }
            set { _ImagemRedesSociaisFile = value; }
        }
    }
}

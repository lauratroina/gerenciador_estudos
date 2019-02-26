using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
namespace BradescoNext.Lib.Entity
{
    [Serializable]
    public class IndicadoInterno
    {
        private int _ID;
        private string _PrimeiroNome;
        private string _NomeMeio;
        private string _Sobrenome;
        private string _NomeMae;
        private DateTime _DataNascimento;
        private string _Genero;
        private int _DocumentoTipoID;
        private string _DocumentoNumeroCrt;
        private string _DocumentoOrgaoEmissor;
        private int? _DocumentoEstadoID;
        private DateTime? _DocumentoDataExpedicao;
        private string _EnderecoCrt;
        private string _EnderecoComplementoCrt;
        private int _EnderecoPaisID;
        private int? _EnderecoCidadeID;
        private string _EnderecoCidadeEstadoNaoBR;
        private string _EnderecoCEP;
        private string _EnderecoBairro;
        private string _EnderecoPais;
        private string _TelefoneResidencialCrt;
        private string _TelefoneComercialCrt;
        private string _EmailCrt;
        private int _CidadeParticipanteID;
        private bool _Condutor;
        private string _HistoriaTitulo;
        private string _HistoriaTexto;
        private int? _HistoriaCategoriaID;
        private string _HistoriaArquivoNome;
        private int _UsuarioID;
        private DateTime _DataModificacao;
        private DateTime _DataCadastro;
        private Cidade _Cidade;
        private CidadeParticipante _CidadeParticipante;
        private Pais _Pais;
        private HistoriaCategoria _HistoriaCategoria;
        private HttpPostedFileBase _NomeArquivo;
        private bool _Inativo;
        private int _IndicadoInternoCategoriaID;
        private IndicadoInternoCategoria _IndicadoInternoCategoria;
        private bool _RemoverGaleria;


        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        public string PrimeiroNome
        {
            get { return _PrimeiroNome; }
            set { _PrimeiroNome = value.Trim(); }
        }

        public string NomeMeio
        {
            get { return _NomeMeio; }
            set 
            {
                if (value == null)
                    value = "";

                _NomeMeio = value.Trim();
            }
        }

        public string Sobrenome
        {
            get { return _Sobrenome; }
            set { _Sobrenome = value.Trim(); }
        }

        public string NomeMae
        {
            get { return _NomeMae; }
            set { _NomeMae = value; }
        }

        public DateTime DataNascimento
        {
            get { return _DataNascimento; }
            set { _DataNascimento = value; }
        }

        public string Genero
        {
            get { return _Genero; }
            set { _Genero = value; }
        }

        public int DocumentoTipoID
        {
            get { return _DocumentoTipoID; }
            set { _DocumentoTipoID = value; }
        }

        public string DocumentoNumero
        {
            get { return _DocumentoNumeroCrt; }
            set { _DocumentoNumeroCrt = value.Replace("-", "").Replace(".", ""); }
        }

        public string DocumentoOrgaoEmissor
        {
            get { return _DocumentoOrgaoEmissor; }
            set { _DocumentoOrgaoEmissor = value; }
        }

        public int? DocumentoEstadoID
        {
            get { return _DocumentoEstadoID; }
            set
            {
                if (value == 0)
                    _DocumentoEstadoID = null;
                else
                    _DocumentoEstadoID = value;
            }
        }

        public DateTime? DocumentoDataExpedicao
        {
            get { return _DocumentoDataExpedicao; }
            set { _DocumentoDataExpedicao = value; }
        }

        public string Endereco
        {
            get { return _EnderecoCrt; }
            set { _EnderecoCrt = value; }
        }

        public string EnderecoComplemento
        {
            get { return _EnderecoComplementoCrt; }
            set { _EnderecoComplementoCrt = value; }
        }

        public int EnderecoPaisID
        {
            get { return _EnderecoPaisID; }
            set { _EnderecoPaisID = value; }
        }

        public int? EnderecoCidadeID
        {
            get { return _EnderecoCidadeID; }
            set
            {
                if (value == 0)
                    _EnderecoCidadeID = null;
                else
                    _EnderecoCidadeID = value;
            }
        }

        public string EnderecoCidadeEstadoNaoBR
        {
            get { return _EnderecoCidadeEstadoNaoBR; }
            set { _EnderecoCidadeEstadoNaoBR = value; }
        }

        public string EnderecoCEP
        {
            get { return _EnderecoCEP; }
            set { _EnderecoCEP = value; }
        }

        public string EnderecoBairro
        {
            get { return _EnderecoBairro; }
            set { _EnderecoBairro = value; }
        }

        public string EnderecoPais
        {
            get { return _EnderecoPais; }
            set { _EnderecoPais = value; }
        }

        public string TelefoneResidencial
        {
            get { return _TelefoneResidencialCrt; }
            set { _TelefoneResidencialCrt = value; }
        }

        public string TelefoneComercial
        {
            get { return _TelefoneComercialCrt; }
            set { _TelefoneComercialCrt = value; }
        }

        public string Email
        {
            get { return _EmailCrt; }
            set { _EmailCrt = value; }
        }

        public int CidadeParticipanteID
        {
            get { return _CidadeParticipanteID; }
            set { _CidadeParticipanteID = value; }
        }

        public bool Condutor
        {
            get { return _Condutor; }
            set { _Condutor = value; }
        }

        public string HistoriaTitulo
        {
            get { return _HistoriaTitulo; }
            set { _HistoriaTitulo = value; }
        }

        public string HistoriaTexto
        {
            get { return _HistoriaTexto; }
            set { _HistoriaTexto = value; }
        }

        public int? HistoriaCategoriaID
        {
            get { return _HistoriaCategoriaID; }
            set
            {
                if (value == 0)
                    _HistoriaCategoriaID = null;
                else
                    _HistoriaCategoriaID = value;
            }
        }

        public HistoriaCategoria HistoriaCategoria
        {
            get { return _HistoriaCategoria; }
            set { _HistoriaCategoria = value;  }
        }

        public string HistoriaArquivoNome
        {
            get { return _HistoriaArquivoNome; }
            set { _HistoriaArquivoNome = value; }
        }

        public int UsuarioID
        {
            get { return _UsuarioID; }
            set { _UsuarioID = value; }
        }

        public DateTime DataModificacao
        {
            get { return _DataModificacao; }
            set { _DataModificacao = value; }
        }

        public DateTime DataCadastro
        {
            get { return _DataCadastro; }
            set { _DataCadastro = value; }
        }

        public DateTime? DataNascimentoOp
        {
            get { if (_DataNascimento == DateTime.MinValue) return null; else return _DataNascimento; }
            set { if (value != null) _DataNascimento = value.Value;  }
        }

        public Cidade Cidade
        {
            get { return _Cidade; }
            set { _Cidade = value; }
        }

        public Pais Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }

        public string NomeCompleto
        {
            get
            {
                return (_PrimeiroNome + ' ' + _NomeMeio + ' ' + _Sobrenome).Trim();
            }
        }

        public CidadeParticipante CidadeParticipante
        {
            get { return _CidadeParticipante; }
            set { _CidadeParticipante = value; }
        }

        public HttpPostedFileBase NomeArquivo
        {
            get { return _NomeArquivo; }
            set { _NomeArquivo = value; }
        }

        public bool Inativo
        {
            get { return _Inativo; }
            set { _Inativo = value; }
        }

        public int IndicadoInternoCategoriaID
        {
            get { return _IndicadoInternoCategoriaID; }
            set { _IndicadoInternoCategoriaID = value; }
        }

        public IndicadoInternoCategoria IndicadoInternoCategoria
        {
            get { return _IndicadoInternoCategoria; }
            set { _IndicadoInternoCategoria = value; }
        }
        public bool RemoverGaleria
        {
            set { _RemoverGaleria = value; }
            get { return _RemoverGaleria; }
        }
    }
}
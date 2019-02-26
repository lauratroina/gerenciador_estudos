using System; 
using System.Text; 
using System.Collections.Generic;
using BradescoNext.Lib.Entity.Enumerator; 
namespace BradescoNext.Lib.Entity  
{  
     [Serializable] 
     public class Indicado
     {
         private int _ID;
         private int _CidadeParticipanteID;
         private int _CidadeID;
         private Cidade _Cidade;
         private CidadeParticipante _CidadeParticipante;
         private Estado _Estado;
         private int? _UsuarioID;
         private int? _ParentescoID;
         private int _DocumentoTipoID;
         private int? _DocumentoEstadoID;
         private int? _HistoriaID;
         private int? _HistoriaIDConcluido;
         private string _Nome;
         private string _Email;
         private string _DocumentoNumero;
         private string _DocumentoOrgaoEmissor;
         private string _Genero;
         private string _FotoArquivoNome;
         private string _ResponsavelNome;
         private string _ResponsavelEmail;
         private string _ResponsavelTelefone;
         private string _AlteracaoOrigem;
         private string _DocumentoNumeroComplemento;
         private DateTime? _DocumentoDataExpedicao;
         private DateTime _DataModificacao;
         private DateTime _DataNascimento;
         private DateTime _DataCadastro;
         private bool _Condutor;
         private bool _RemoverGaleria;
         private int _TotalHistorias;
         private bool _PossuiRessalva;
         private Usuario _Usuario;
         private decimal _MaiorNotaConcluida;
         private decimal _MaiorNota;
         private IList<Historia> _Historias;
         private Historia _Historia;
         private bool _Interno;

         public int ID
         {
             set { _ID = value; }
             get { return _ID; }
         }
         public decimal MaiorNotaConcluida
         {
             set { _MaiorNotaConcluida = value; }
             get { return _MaiorNotaConcluida; }
         }

         public decimal MaiorNota
         {
             set { _MaiorNota = value; }
             get { return _MaiorNota; }
         }
         public string DocumentoNumeroComplemento
         {
             set { _DocumentoNumeroComplemento = value; }
             get { return _DocumentoNumeroComplemento; }
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

         public int DocumentoTipoIDDB
         {
             set { _DocumentoTipoID = value; }
             get { return _DocumentoTipoID; }
         }
         public enumDocumentoTipoNome DocumentoTipoID
         {
             set { _DocumentoTipoID = value.ValueAsInt(); }
             get { try { return EnumExtensions.FromInt<enumDocumentoTipoNome>(_DocumentoTipoID); } catch { return enumDocumentoTipoNome.cpf; } }
         }

         public int? DocumentoEstadoID
         {
             set { _DocumentoEstadoID = value; }
             get { return _DocumentoEstadoID; }
         }

         public string Genero
         {
             set { _Genero = value; }
             get { return _Genero; }
         }


         public DateTime DataNascimento
         {
             set { _DataNascimento = value; }
             get { return _DataNascimento; }
         }

         public string DataNascimentoAsString
         {
             set { _DataNascimento = value.ToDateTime(); }
             get { return _DataNascimento.ToString("yyyy-MM-dd"); }
         }

         public string FotoArquivoNome
         {
             set { _FotoArquivoNome = value; }
             get { return _FotoArquivoNome; }
         }

         public string DocumentoNumero
         {
             set { _DocumentoNumero = value; }
             get { return _DocumentoNumero; }
         }

         public string DocumentoOrgaoEmissor
         {
             set { _DocumentoOrgaoEmissor = value; }
             get { return _DocumentoOrgaoEmissor; }
         }

         public int? ParentescoID
         {
             set { _ParentescoID = value; }
             get { return _ParentescoID; }
         }
         public int? HistoriaID
         {
             set { _HistoriaID = value; }
             get { return _HistoriaID; }
         }
         public int? HistoriaIDConcluido
         {
             set { _HistoriaIDConcluido = value; }
             get { return _HistoriaIDConcluido; }
         }
         public string ResponsavelNome
         {
             set { _ResponsavelNome = value; }
             get { return _ResponsavelNome; }
         }

         public string ResponsavelEmail
         {
             set { _ResponsavelEmail = value; }
             get { return _ResponsavelEmail; }
         }

         public string ResponsavelTelefone
         {
             set { _ResponsavelTelefone = value; }
             get { return _ResponsavelTelefone; }
         }

         public int CidadeParticipanteID
         {
             set { _CidadeParticipanteID = value; }
             get { return _CidadeParticipanteID; }
         }

         public int CidadeID
         {
             set { _CidadeID = value; }
             get { return _CidadeID; }
         }

         public Cidade Cidade
         {
             set { _Cidade = value; }
             get { return _Cidade; }
         }

         public Estado Estado
         {
             set { _Estado = value; }
             get { return _Estado; }
         }

         public bool Condutor
         {
             set { _Condutor = value; }
             get { return _Condutor; }
         }

         public bool RemoverGaleria
         {
             set { _RemoverGaleria  = value; }
             get { return _RemoverGaleria; }
         }

         public int? UsuarioID
         {
             set { _UsuarioID = value; }
             get { return _UsuarioID; }
         }

         public string AlteracaoOrigemDB
         {
             set { _AlteracaoOrigem = value; }
             get { return _AlteracaoOrigem; }
         }
         public enumAlteracaoOrigem AlteracaoOrigem
         {
             set { _AlteracaoOrigem = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAlteracaoOrigem>(_AlteracaoOrigem); } catch { return enumAlteracaoOrigem.Nenhum; } }
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

         public DateTime? DocumentoDataExpedicao
         {
             set { _DocumentoDataExpedicao = value; }
             get { return _DocumentoDataExpedicao; }
         }
     
         public int QuantidadeEmailAguardandoAprovacao { get; set; }
     
         public int NumeroReportsAbuso { get; set; }
         public DateTime UltimoReportAbuso { get; set; }
         public int TotalHistorias
         {
             get { return _TotalHistorias; }
             set { _TotalHistorias = value; }
         }

         public bool PossuiRessalva
         {
             get { return _PossuiRessalva; }
             set { _PossuiRessalva = value; }
         }

         public CidadeParticipante CidadeParticipante
         {
             get { return _CidadeParticipante; }
             set { _CidadeParticipante = value; }
         }

         public Usuario Usuario
         {
             set { _Usuario = value; }
             get { return _Usuario; }
         }

         public Historia Historia
         {
             set { _Historia = value; }
             get { return _Historia; }
         }

         public IList<Historia> Historias
         {
             set { _Historias = value; }
             get { return _Historias; }
         }

         public bool Interno
         {
             set { _Interno = value; }
             get { return _Interno; }
         }
     } 
} 
using System; 
using System.Text; 
using System.Collections.Generic;
using BradescoNext.Lib.Entity.Enumerator; 
namespace BradescoNext.Lib.Entity  
{  
     [Serializable] 
     public class Historia
     {
         private int _ID;
         private int? _IndicadoID;
         private int _HistoriaCategoriaID;
         private int? _UsuarioID;
         private int _QuantidadeAbuso;
         private int _QuantidadeEmailAguardandoAprovacao;
         private decimal? _Nota;
         private string _IndicadorFacebookID;
         private string _IndicadorFacebookToken;
         private string _IndicadorNome;
         private string _IndicadorEmail;
         private string _IndicadorFotoArquivoNome;
         private string _IndicadoNome;
         private string _IndicadoEmail;
         private string _ComentarioIndicadoResponsavel;
         private string _ComentarioIndicado;
         private string _ComentarioTriagem;
         private string _ComentarioTriagemAbuso;
         private string _ComentarioTriagemNormal;
         private string _ComentarioTriagemSupervisor;
         private string _Titulo;
         private string _Texto;
         private string _CodigoIndicado;
         private string _CodigoIndicadoResponsavel;
         private string _ComentarioIndicadoOriginal;
         private string _IndicadoAprovacao;
         private string _IndicadoAprovacaoOriginal;
         private string _TriagemAprovacao;
         private string _TriagemAprovacaoNormal;
         private string _TriagemAprovacaoSupervisor;
         private string _TriagemAprovacaoAbuso;
         private string _IndicadoAprovacaoResponsavel;
         private string _AlteracaoOrigem;
         private DateTime _DataCadastro;
         private DateTime _DataModificacao;
         private DateTime _DataEmailAguardandoAprovacao;
         private IList<HistoriaMidia> _Midias;
         private HistoriaMidia _Midia;
         private HistoriaCategoria _HistoriaCategoria;
         private Usuario _Usuario;
         private bool _ModeracaoEncerrada;
         private bool _EmailGaleria;

         
         public DateTime DataEmailAguardandoAprovacao
         {
             set { _DataEmailAguardandoAprovacao = value; }
             get { return _DataEmailAguardandoAprovacao; }
         }
         
         public int QuantidadeEmailAguardandoAprovacao
         {
             set { _QuantidadeEmailAguardandoAprovacao = value; }
             get { return _QuantidadeEmailAguardandoAprovacao; }
         }
         public decimal? Nota
         {
             set { _Nota = value; }
             get { return _Nota; }
         }
         public string AlteracaoOrigemDB
         {
             set { _AlteracaoOrigem = value; }
             get { return _AlteracaoOrigem; }
         }
         public enumAlteracaoOrigem AlteracaoOrigem
         {
             set { _AlteracaoOrigem = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAlteracaoOrigem>(_AlteracaoOrigem); } catch { return enumAlteracaoOrigem.Indicacao; } }
         }
         public string IndicadoAprovacaoResponsavelDB
         {
             set { _IndicadoAprovacaoResponsavel = value; }
             get { return _IndicadoAprovacaoResponsavel; }
         }
         public enumAprovacao IndicadoAprovacaoResponsavel
         {
             set { _IndicadoAprovacaoResponsavel = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAprovacao>(_IndicadoAprovacaoResponsavel); } catch { return enumAprovacao.semNecessidade; } }
         }
         public enumAprovacao IndicadoAprovacaoOriginal
         {
             set { _IndicadoAprovacaoOriginal = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAprovacao>(_IndicadoAprovacaoOriginal); } catch { return enumAprovacao.semNecessidade; } }
         }
         public string IndicadoAprovacaoOriginalDB
         {
             set { _IndicadoAprovacaoOriginal = value; }
             get { return _IndicadoAprovacaoOriginal; }
         }
         public enumAprovacao TriagemAprovacaoAbuso
         {
             set { _TriagemAprovacaoAbuso = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAprovacao>(_TriagemAprovacaoAbuso); } catch { return enumAprovacao.semNecessidade; } }
         }
         public string TriagemAprovacaoAbusoDB
         {
             set { _TriagemAprovacaoAbuso = value; }
             get { return _TriagemAprovacaoAbuso; }
         }
         public string TriagemAprovacaoSupervisorDB
         {
             set { _TriagemAprovacaoSupervisor = value; }
             get { return _TriagemAprovacaoSupervisor; }
         }
         public enumAprovacao TriagemAprovacaoSupervisor
         {
             set { _TriagemAprovacaoSupervisor = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAprovacao>(_TriagemAprovacaoSupervisor); } catch { return enumAprovacao.semNecessidade; } }
         }
         public string TriagemAprovacaoNormalDB
         {
             set { _TriagemAprovacaoNormal = value; }
             get { return _TriagemAprovacaoNormal; }
         }
         public enumAprovacao TriagemAprovacaoNormal
         {
             set { _TriagemAprovacaoNormal = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAprovacao>(_TriagemAprovacaoNormal); } catch { return enumAprovacao.semNecessidade; } }
         }
         public string TriagemAprovacaoDB
         {
             set { _TriagemAprovacao = value; }
             get { return _TriagemAprovacao; }
         }
         public enumAprovacao TriagemAprovacao
         {
             set { _TriagemAprovacao = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAprovacao>(_TriagemAprovacao); } catch { return enumAprovacao.semNecessidade; } }
         }
         public enumAprovacao IndicadoAprovacao
         {
             set { _IndicadoAprovacao = value.ValueAsString(); }
             get { try { return EnumExtensions.FromChar<enumAprovacao>(_IndicadoAprovacao); } catch { return enumAprovacao.semNecessidade; } }
         }
         public string IndicadoAprovacaoDB
         {
             set { _IndicadoAprovacao = value; }
             get { return _IndicadoAprovacao; }
         }
         public string CodigoIndicadoResponsavel
         {
             set { _CodigoIndicadoResponsavel = value; }
             get { return _CodigoIndicadoResponsavel; }
         }
         public string CodigoIndicado
         {
             set { _CodigoIndicado = value; }
             get { return _CodigoIndicado; }
         }
         public string Texto
         {
             set { _Texto = value; }
             get { return _Texto; }
         }
         public string Titulo
         {
             set { _Titulo = value; }
             get { return _Titulo; }
         }
         public string ComentarioTriagemSupervisor
         {
             set { _ComentarioTriagemSupervisor = value; }
             get { return _ComentarioTriagemSupervisor; }
         }
         public string ComentarioTriagemNormal
         {
             set { _ComentarioTriagemNormal = value; }
             get { return _ComentarioTriagemNormal; }
         }
         public string ComentarioTriagemAbuso
         {
             set { _ComentarioTriagemAbuso = value; }
             get { return _ComentarioTriagemAbuso; }
         }
         public string ComentarioTriagem
         {
             set { _ComentarioTriagem = value; }
             get { return _ComentarioTriagem; }
         }
         public string ComentarioIndicado
         {
             set { _ComentarioIndicado = value; }
             get { return _ComentarioIndicado; }
         }
         public string ComentarioIndicadoResponsavel
         {
             set { _ComentarioIndicadoResponsavel = value; }
             get { return _ComentarioIndicadoResponsavel; }
         }
         public string ComentarioIndicadoOriginal
         {
             set { _ComentarioIndicadoOriginal = value; }
             get { return _ComentarioIndicadoOriginal; }
         }
         public string IndicadorEmail
         {
             set { _IndicadorEmail = value; }
             get { return _IndicadorEmail; }
         }
         public string IndicadorNome
         {
             set { _IndicadorNome = value; }
             get { return _IndicadorNome; }
         }
         public string IndicadoEmail
         {
             set { _IndicadoEmail = value; }
             get { return _IndicadoEmail; }
         }
         public string IndicadoNome
         {
             set { _IndicadoNome = value; }
             get { return _IndicadoNome; }
         }
         public string IndicadorFacebookToken
         {
             set { _IndicadorFacebookToken = value; }
             get { return _IndicadorFacebookToken; }
         }
         public string IndicadorFacebookID
         {
             set { _IndicadorFacebookID = value; }
             get { return _IndicadorFacebookID; }
         }
         public int? IndicadoID
         {
             set { _IndicadoID = value; }
             get { return _IndicadoID; }
         }
         public int HistoriaCategoriaID
         {
             set { _HistoriaCategoriaID = value; }
             get { return _HistoriaCategoriaID; }
         }
         public int QuantidadeAbuso
         {
             set { _QuantidadeAbuso = value; }
             get { return _QuantidadeAbuso; }
         }
         public int ID
         {
             set { _ID = value; }
             get { return _ID; }
         }
         public int? UsuarioID
         {
             set { _UsuarioID = value; }
             get { return _UsuarioID; }
         }

         public DateTime DataModificacao
         {
             set { _DataModificacao = value; }
             get { return _DataModificacao; }
         }

         public DateTime DataCadastro
         {
             set { _DataCadastro = value; }
             get { return _DataCadastro; }
         }

         public string IndicadorFotoArquivoNome
         {
             set { _IndicadorFotoArquivoNome = value; }
             get { return _IndicadorFotoArquivoNome; }
         }

         public IList<HistoriaMidia> Midias
         {
             set { _Midias = value; }
             get { return _Midias; }
         }

         public HistoriaMidia Midia
         {
             set { _Midia = value; }
             get { return _Midia; }
         }


         public HistoriaCategoria HistoriaCategoria
         {
             get { return _HistoriaCategoria; }
             set { _HistoriaCategoria = value; }
         }

         public bool ModeracaoEncerrada
         {
             set { _ModeracaoEncerrada = value; }
             get { return _ModeracaoEncerrada; }
         }

         public Usuario Usuario
         {
             set { _Usuario = value; }
             get { return _Usuario; }
         }

         public bool EmailGaleria
         {
             set { _EmailGaleria = value; }
             get { return _EmailGaleria; }
         }
         
     } 
} 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity.Enumerator;

namespace BradescoNext.Lib.Models
{
    public class ExportaHistoriaModel
    {
        public int IndicadoID { get; set; }
        public int ID { get; set; }
        public int HistoriaCategoriaID { get; set; }
        public string CategoriaNome { get; set; }
        public int? UsuarioID { get; set; }
        public string UsuarioNome { get; set; }
        public int QuantidadeAbuso { get; set; }
        public int QuantidadeEmailAguardandoAprovacao { get; set; }
        public decimal? Nota { get; set; }
        public string IndicadorFacebookID { get; set; }
        public string IndicadorFacebookToken { get; set; }
        public string IndicadorNome { get; set; }
        public string IndicadorEmail { get; set; }
        public string IndicadorFotoArquivoNome { get; set; }
        public string IndicadoNome { get; set; }
        public string IndicadoEmail { get; set; }
        public string ComentarioIndicadoResponsavel { get; set; }
        public string ComentarioIndicado { get; set; }
        public string ComentarioTriagem { get; set; }
        public string ComentarioTriagemAbuso { get; set; }
        public string ComentarioTriagemNormal { get; set; }
        public string ComentarioTriagemSupervisor { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string CodigoIndicado { get; set; }
        public string CodigoIndicadoResponsavel { get; set; }
        public string ComentarioIndicadoOriginal { get; set; }
        public string IndicadoAprovacao { get; set; }
        public string IndicadoAprovacaoTabela
        {
            get
            {
                try
                {
                    return EnumExtensions.FromChar<enumAprovacao>(IndicadoAprovacao).Description();
                }
                catch
                {
                    return enumAprovacao.semNecessidade.Description();
                }
            }
        }
        public string IndicadoAprovacaoOriginal { get; set; }
        public string IndicadoAprovacaoOriginalTabela
        {
            get
            {
                try
                {
                    return EnumExtensions.FromChar<enumAprovacao>(IndicadoAprovacaoOriginal).Description();
                }
                catch
                {
                    return enumAprovacao.semNecessidade.Description();
                }
            }
        }
        public string TriagemAprovacao { get; set; }
        public string TriagemAprovacaoTabela
        {
            get
            {
                try
                {
                    return EnumExtensions.FromChar<enumAprovacao>(TriagemAprovacao).Description();
                }
                catch
                {
                    return enumAprovacao.semNecessidade.Description();
                }
            }
        }
        public string TriagemAprovacaoNormal { get; set; }
        public string TriagemAprovacaoNormalTabela
        {
            get
            {
                try
                {
                    return EnumExtensions.FromChar<enumAprovacao>(TriagemAprovacaoNormal).Description();
                }
                catch
                {
                    return enumAprovacao.semNecessidade.Description();
                }
            }
        }
        public string TriagemAprovacaoSupervisor { get; set; }
        public string TriagemAprovacaoSupervisorTabela
        {
            get
            {
                try
                {
                    return EnumExtensions.FromChar<enumAprovacao>(TriagemAprovacaoSupervisor).Description();
                }
                catch
                {
                    return enumAprovacao.semNecessidade.Description();
                }
            }
        }
        public string TriagemAprovacaoAbuso { get; set; }
        public string TriagemAprovacaoAbusoTabela
        {
            get
            {
                try
                {
                    return EnumExtensions.FromChar<enumAprovacao>(TriagemAprovacaoAbuso).Description();
                }
                catch
                {
                    return enumAprovacao.semNecessidade.Description();
                }
            }
        }
        public string IndicadoAprovacaoResponsavel { get; set; }
        public string IndicadoAprovacaoResponsavelTabela
        {
            get
            {
                try
                {
                    return EnumExtensions.FromChar<enumAprovacao>(IndicadoAprovacaoResponsavel).Description();
                }
                catch
                {
                    return enumAprovacao.semNecessidade.Description();
                }
            }
        }
        public string AlteracaoOrigem { get; set; }
        public string AlteracaoOrigemTabela
        {
            get 
            {
                try 
                {
                    return EnumExtensions.FromChar<enumAlteracaoOrigem>(AlteracaoOrigem).Description(); 
                } 
                catch 
                { 
                    return enumAlteracaoOrigem.Indicacao.Description(); 
                } 
            }
        }
        public DateTime DataCadastro { get; set; }
        public string DataCadastroTabela
        {
            get
            {
                return DataCadastro.ToString("dd/MM/yyyy");
            }
        }
        public DateTime DataModificacao { get; set; }
        public string DataModificacaoTabela
        {
            get
            {
                if (DataModificacao == DateTime.MinValue)
                    return "";
                return DataModificacao.ToString("dd/MM/yyyy");
            }
        }
        public DateTime DataEmailAguardandoAprovacao { get; set; }
        public string DataEmailAguardandoAprovacaoTabela
        {
            get
            {
                if (DataEmailAguardandoAprovacao == DateTime.MinValue)
                    return "";
                return DataEmailAguardandoAprovacao.ToString("dd/MM/yyyy");
            }
        }
        public bool ModeracaoEncerrada { get; set; }
        public string ModeracaoEncerradaTabela
        {
            get
            {
                if (ModeracaoEncerrada)
                    return "Sim";
                else
                    return "Não";
            }
        }
        public bool EmailGaleria { get; set; }
        public string EmailGaleriaTabela
        {
            get
            {
                if (EmailGaleria)
                    return "Sim";
                else
                    return "Não";
            }
        }

    }
}

using Facebook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Util;
using BradescoNext.Lib.Util.Enumerator;

namespace BradescoNext.Lib.Service
{
    public class HistoriaService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IHistoriaDAL dal = new HistoriaADO();

        public RetornoModel SalvarIndicacao(Historia historia)
        {
            RetornoModel retorno = null;
            retorno = Validar(historia);
            if (retorno.Sucesso)
            {
                historia.IndicadoAprovacao = enumAprovacao.pendente;
                historia.IndicadoAprovacaoOriginal = enumAprovacao.pendente;
                historia.IndicadoAprovacaoResponsavel = enumAprovacao.semNecessidade;
                historia.TriagemAprovacao = enumAprovacao.semNecessidade;
                historia.TriagemAprovacaoAbuso = enumAprovacao.semNecessidade;
                historia.TriagemAprovacaoNormal = enumAprovacao.semNecessidade;
                historia.TriagemAprovacaoSupervisor = enumAprovacao.semNecessidade;
                historia.AlteracaoOrigem = enumAlteracaoOrigem.Indicacao;
                historia.DataCadastro = DateTime.Now;
                historia.DataModificacao = DateTime.Now;

                if (!string.IsNullOrEmpty(historia.IndicadorFacebookToken))
                {
                    historia.IndicadorFotoArquivoNome = Guid.NewGuid().ToString() + ".jpg";
                }
                historia.DataEmailAguardandoAprovacao = DateTime.Now;
                historia.CodigoIndicado = GeraCodigo();

                EnviarEmailConfirmacao(historia);
                EnviarEmailIndicado(historia);

                historia.ID = Salvar(historia);

                HistoriaMidiaService service = new HistoriaMidiaService();
                foreach (var midia in historia.Midias) //salvando as midias
                {
                    midia.AdicionadoOrigem = enumAlteracaoOrigem.Indicacao;
                    midia.HistoriaID = historia.ID;
                    midia.DataCadastro = DateTime.Now;
                    midia.InativoOrigem = enumAlteracaoOrigem.Nenhum;
                    service.Salvar(midia);
                }

                if (!string.IsNullOrEmpty(historia.IndicadorFacebookToken))
                {
                    Task.Run(() => FacebookGetImagem(historia));
                }
            }

            return retorno;
        }
        public int Salvar(Historia historia)
        {
            if (historia.ID > 0)
            {
                dal.InserirLog(historia.ID);
                dal.Atualizar(historia);
            }
            else
            {
                historia.ID = dal.Inserir(historia);
            }
            return historia.ID;
        }

        private RetornoModel Validar(Historia model)
        {
            if (model == null)
            {
                return new RetornoModel { Mensagem = "Dados obrigatórios não fornecidos.", Sucesso = false };
            }
            if (string.IsNullOrEmpty(model.IndicadorNome))
            {
                return new RetornoModel { Mensagem = "Nome inválido.", Sucesso = false };
            }
            if (!EmailUtil.IsValidMailAdress(model.IndicadorEmail))
            {
                return new RetornoModel { Mensagem = "Email inválido.", Sucesso = false };
            }
            if (string.IsNullOrEmpty(model.IndicadoNome))
            {
                return new RetornoModel { Mensagem = "Nome do indicado inválido.", Sucesso = false };
            }
            if (!EmailUtil.IsValidMailAdress(model.IndicadoEmail))
            {
                return new RetornoModel { Mensagem = "Email do indicado inválido.", Sucesso = false };
            }
            if (model.HistoriaCategoriaID <= 0)
            {
                return new RetornoModel { Mensagem = "Selecione uma categoria de história.", Sucesso = false };
            }
            if (string.IsNullOrEmpty(model.Texto))
            {
                return new RetornoModel { Mensagem = "Escreva uma história.", Sucesso = false };
            }
            if (model.IndicadorEmail == model.IndicadoEmail)
            {
                return new RetornoModel { Mensagem = "Você não pode se indicar.", Sucesso = false };
            }
            if (model.Midias != null && model.Midias.Count > 0)
            {
                foreach (var midia in model.Midias)
                {
                    if (string.IsNullOrEmpty(midia.ArquivoNome))
                    {
                        return new RetornoModel { Mensagem = "Mídia não encontrada.", Sucesso = false };
                    }
                }
            }
            return new RetornoModel { Mensagem = "OK", Sucesso = true };
        }

        public string GeraCodigo()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string result = null;
            do
            {
                result = new string(Enumerable.Repeat(chars, 7).Select(s => s[random.Next(s.Length)]).ToArray());
            } while (CheckCodigo(result) > 0);
            return result;
        }

        private void EnviarEmailConfirmacao(Historia entidade)
        {
            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "IndicadorIndicacao.cshtml");
            string email = RazorUtil.Render(path, EmailModel.GetModel(entidade));
            EmailUtil.Send(entidade.IndicadorEmail, "Bradesco | Tocha Olímpica | Indicação registrada", email);

        }

        private void EnviarEmailIndicado(Historia entidade)
        {
            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "IndicadoIndicacao.cshtml");
            string email = RazorUtil.Render(path, EmailModel.GetModel(entidade));
            EmailUtil.Send(entidade.IndicadoEmail, "Bradesco | Tocha Olímpica | Sua indicação", email);
        }

        public bool SalvarTriagem(Historia historia, int usuarioID, enumPerfilNome perfil, enumAprovacao operacao)
        {
            if (!TriagemBloqueada(historia, usuarioID, perfil))
            {
                dal.InserirLog(historia.ID);
                dal.AtualizarTriagemModeracao(historia, perfil, operacao);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool SalvarModeracao(Historia historia)
        {
            dal.InserirLog(historia.ID);
            dal.AtualizarTriagemModeracao(historia, enumPerfilNome.moderadores);

            return true;
        }

        public Historia Carregar(int id)
        {
            return dal.Carregar(id);
        }

        public Historia Carregar(string codigo)
        {
            return dal.Carregar(codigo);
        }
        public int UltimaHistoriaModerada(int indicadoID)
        {
            return dal.CarregaIDUltimaHistoriaModerada(indicadoID);
        }
        public int CheckCodigo(string codigoIndicado)
        {
            return dal.CheckCodigo(codigoIndicado);
        }

        public Historia CarregarTriagemModeracao(int id)
        {
            return dal.CarregarTriagemModeracao(id);
        }

        public bool TriagemBloqueada(Historia historia, int usuarioID, enumPerfilNome perfil)
        {
            if (historia.UsuarioID == usuarioID)
            {
                return false;
            }
            else
            {
                return dal.TriagemBloqueada(historia.ID, perfil);
            }
            
        }

        public void BloquearTriagem(int id, int usuarioID, enumPerfilNome perfil)
        {
            dal.BloquearTriagem(id, usuarioID, perfil);
        }

        public IList<Historia> Listar(int indicadoID)
        {
            return dal.Listar(indicadoID);
        }


        public IList<Historia> Listar(Int32 skip, Int32 take, int indicadoID)
        {
            IList<Historia> lista = dal.Listar(skip, take, indicadoID);
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }

        public IList<Historia> Listar(Int32 skip, Int32 take, int usuarioID, enumPerfilNome perfil, string palavraChave = "")
        {
            IList<Historia> lista = dal.Listar(skip, take, usuarioID, perfil, palavraChave);
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }

        public void FacebookGetImagem(Historia historia)
        {
            try
            {
                FacebookClient facebookClient = new FacebookClient();
                facebookClient.AppId = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.facebookAppID);
                facebookClient.AppSecret = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.facebookAppSecret);
                facebookClient.AccessToken = historia.IndicadorFacebookToken;
                dynamic picture = facebookClient.Get("me/picture?redirect=false&width=800");
                if ((picture != null) && (picture.data != null) && (picture.data.url != null))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile(picture.data.url, ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.indicadorFotoDiretorio, historia.IndicadorFotoArquivoNome));
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                try
                {
                    dal.RemoverIndicadorFoto(historia.ID);
                }
                catch (Exception e)
                {
                    LogUtil.Error(e);
                }
            }
        }


        public void AtualizarAguardandoAprovacao(Historia entidade)
        {
            // Metodo usado pelo robo do admin por isso não insere no log
            dal.AtualizarAguardandoAprovacao(entidade);
        }

        public void EnviarEmailsRecusaHistoria(IndicadoModel entidade)
        {
            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "AvisoRecusaTriagemParaIndicador.cshtml");
            string email = RazorUtil.Render(path, EmailModel.GetModel(entidade));
            EmailUtil.Send(entidade.Historia.IndicadorEmail, "Bradesco | Tocha Olímpica | História recusada", email);

            path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "AvisoRecusaTriagemParaIndicado.cshtml");
            email = RazorUtil.Render(path, EmailModel.GetModel(entidade));
            EmailUtil.Send(entidade.Indicado.Email, "Bradesco | Tocha Olímpica | História recusada", email);
        }

        public IList<HistoriaReportModel> ListarHistoriasReportadas(int indicadoID)
        {
            return dal.ListarHistoriasReportadas(indicadoID).ToList();
        }

        public RetornoModel RealizarTriagemAbuso(HistoriaReportModel model, int usuarioID)
        {
            RetornoModel retorno = new RetornoModel { Mensagem = "Ok", Sucesso = true };
            if (model != null)
            {
                // salvando log..
                dal.InserirLog(model.Historia.ID);

                Historia historia = dal.Carregar(model.Historia.ID);
                historia.ComentarioTriagemAbuso = model.Historia.ComentarioTriagemAbuso;
                historia.ComentarioTriagem = model.Historia.ComentarioTriagemAbuso;
                historia.DataModificacao = DateTime.Now;
                historia.AlteracaoOrigem = enumAlteracaoOrigem.TriagemAbuso;
                historia.UsuarioID = usuarioID;

                if (!model.AceitarHistoria)
                {
                    //atualizando historia
                    historia.TriagemAprovacaoAbuso = enumAprovacao.naoAprovado;
                    historia.TriagemAprovacao = enumAprovacao.naoAprovado;
                }
                else
                {
                    //atualizando historia
                    historia.TriagemAprovacaoAbuso = enumAprovacao.aprovado;
                    historia.QuantidadeAbuso = 0;

                    //inativando reportes da história
                    HistoriaReporteAbusoService service = new HistoriaReporteAbusoService();
                    service.InativarReportes(model.Reportes);

                }
                dal.AtualizarTriagemAbuso(historia);
            }
            else
            {
                retorno = new RetornoModel { Mensagem = "Erro ao obter os dados", Sucesso = false };
            }
            return retorno;
        }
        public List<HistoriaVizualizaModel> ListarHistoriasComMidias(int indicadoID, bool mostraTodos)
        {
            return dal.ListarHistoriasComMidias(indicadoID, mostraTodos).ToList();
        }

        public OverviewModel CarregarDadosOverview()
        {
            return dal.CarregarDadosOverview();
        }
        public List<ExportaHistoriaModel> ListarExcel()
        {
            return dal.ListarExcel().ToList();
        }
    }
}

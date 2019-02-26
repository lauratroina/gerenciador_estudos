using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
    public class IndicadoService
    {
        private Int32 _totalRegistros = 0;
        private static int indicadoMaxWidth = 159;
        private static int indicadoMaxHeight = 145;
        private static int indicadoMaxWidthImage = 400;
        private static int indicadoMaxHeightImage = 400;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IIndicadoDAL dal = new IndicadoADO();

        public Indicado CarregarIndicadoPorDocumento(Indicado indicado)
        {
            return dal.Carregar(indicado.DocumentoNumero, indicado.DocumentoTipoIDDB); //verificando se ja existe no banco um indicado com mesmo numero de documento
        }
        public int SalvarConfirmarIndicacaoIndicado(Indicado indicado)
        {
            if (indicado.ID > 0)
            {
                dal.InserirLog(indicado.ID);
                dal.AtualizarConfirmarIndicacao(indicado, MenorIdade(indicado));
            }
            else
            {
                indicado.ID = dal.Inserir(indicado);
            }
            return indicado.ID;
        }

        public RetornoModel RecusarIndicacao(IndicadoModel model)
        {

            HistoriaService historiaService = new HistoriaService();
            Historia historiaTela = model.Historia;
            Indicado indicadoTela = model.Indicado;

            model.Historia = historiaService.Carregar(model.Codigo);
            model.Indicado = dal.Carregar(model.Historia.IndicadoID ?? 0);
            if (model.Historia.ID != historiaTela.ID)
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Você não pode manipular um ID diferente do seu código." };
            }


            //se for responsavel
            if (model.Historia.CodigoIndicadoResponsavel == model.Codigo)
            {
                model.Historia.IndicadoAprovacaoResponsavel = enumAprovacao.naoAprovado;
                model.Historia.IndicadoAprovacao = enumAprovacao.naoAprovado;

                EnviarEmailRecusaResponsavel(model);
            }
            else
            {
                model.Historia.IndicadoAprovacao = enumAprovacao.naoAprovado;
                model.Historia.IndicadoAprovacaoOriginal = enumAprovacao.naoAprovado;
                EnviarEmailRecusaIndicado(model);
            }
            model.Historia.IndicadoID = null;
            HistoriaService service = new HistoriaService();
            service.Salvar(model.Historia);

            return new RetornoModel() { Sucesso = true, Mensagem = "OK!" };
        }

        public void EnviarEmailRecusaIndicado(IndicadoModel model) //quando o indicado nega
        {
            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "IndicadorRecusa.cshtml");
            string email = RazorUtil.Render(path, EmailModel.GetModel(model.Historia));
            EmailUtil.Send(model.Historia.IndicadorEmail, "Bradesco | Tocha Olímpica | Indicação recusada", email);
        }

        public void EnviarEmailRecusaResponsavel(IndicadoModel model) //quando o responsavel nega
        {
            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "RecusaResponsavelParaIndicador.cshtml");
            string email = RazorUtil.Render(path, EmailModel.GetModel(model));
            EmailUtil.Send(model.Historia.IndicadorEmail, "Bradesco | Tocha Olímpica | Autorização recusada", email);

            path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "RecusaResponsavelParaIndicado.cshtml");
            email = RazorUtil.Render(path, EmailModel.GetModel(model));
            EmailUtil.Send(model.Historia.IndicadoEmail, "Bradesco | Tocha Olímpica | Autorização recusada", email);
        }

        public RetornoModel<IndicadoModel> CarregarConfirmarIndicacao(string Codigo)
        {
            IndicadoModel model = new IndicadoModel();
            HistoriaService sc = new HistoriaService();
            HistoriaMidiaService scMidia = new HistoriaMidiaService();

            model.Codigo = Codigo;
            model.Historia = sc.Carregar(Codigo); //buscando historia a partir do codigo do indicado OU RESPONSAVEL
            if (model.Historia == null) // se a historia existe, monta o model
            {
                return new RetornoModel<IndicadoModel> { Mensagem = "Código inválido!", Sucesso = false };
            }
            model.Historia.Midias = scMidia.Listar(model.Historia.ID, true);
            model.Responsavel = (Codigo == model.Historia.CodigoIndicadoResponsavel);
            if (model.Responsavel)
            {


                if ((model.Historia.IndicadoAprovacaoOriginal != enumAprovacao.aprovado) || (model.Historia.IndicadoAprovacaoResponsavel != enumAprovacao.pendente))
                {
                    string mensagem = "Não existe nenhuma pendência vinculada a este código.";
                    if (model.Historia.IndicadoAprovacaoResponsavel == enumAprovacao.aprovado)
                    {
                        mensagem = "Essa indicação já foi aprovada.";
                    }
                    else if (model.Historia.IndicadoAprovacaoResponsavel == enumAprovacao.naoAprovado)
                    {
                        mensagem = "Essa indicação já foi recusada.";
                    }
                    return new RetornoModel<IndicadoModel> { Mensagem = mensagem, Sucesso = false };
                }
                model.Indicado = dal.Carregar(model.Historia.IndicadoID.Value);
                model.Estrangeiro = (model.Indicado.DocumentoTipoID != enumDocumentoTipoNome.cpf);
                model.Indicado.UsuarioID = 0;
            }
            else
            {
                if (model.Historia.IndicadoAprovacaoOriginal != enumAprovacao.pendente)
                {
                    string mensagem = "Não existe nenhuma pendência vinculada a este código.";
                    if (model.Historia.IndicadoAprovacaoOriginal == enumAprovacao.aprovado)
                    {
                        mensagem = "Essa indicação já foi aprovada.";
                    }
                    else if (model.Historia.IndicadoAprovacaoOriginal == enumAprovacao.naoAprovado)
                    {
                        mensagem = "Essa indicação já foi recusada.";
                    }
                    return new RetornoModel<IndicadoModel> { Mensagem = mensagem, Sucesso = false };
                }
                model.Indicado = new Indicado();
                model.Indicado.Nome = model.Historia.IndicadoNome;
                model.Indicado.Email = model.Historia.IndicadoEmail;
            }
            model.Historia.CodigoIndicado = null;
            model.Historia.CodigoIndicadoResponsavel = null;

            HistoriaCategoriaService categoriaService = new HistoriaCategoriaService();
            model.Categoria = categoriaService.Carregar(model.Historia.HistoriaCategoriaID);
            //buscando os estados
            EstadoService service = new EstadoService();
            model.Estados = service.Listar();

            ParentescoService parentescoService = new ParentescoService();
            model.Parentesco = parentescoService.Listar(true);

            model.IndicadoFotoInfo = CarregarFotoInfo(model.Indicado.FotoArquivoNome);
            if (model.IndicadoFotoInfo == null)
            {
                model.IndicadoFotoInfo = new UploadFotoModel();
            }

            return new RetornoModel<IndicadoModel> { Retorno = model, Mensagem = "Código confirmado com sucesso!", Sucesso = true };
        }

        private void ConfirmarIndicacaoUpdateIndicado(Indicado origem, Indicado destino)
        {
            destino.Nome = origem.Nome;
            destino.Email = origem.Email;
            destino.CidadeID = origem.CidadeID;
            destino.Genero = origem.Genero;
            destino.DataNascimento = origem.DataNascimento;
            destino.DocumentoDataExpedicao = origem.DocumentoDataExpedicao;
            destino.DocumentoEstadoID = origem.DocumentoEstadoID;
            destino.DocumentoNumero = origem.DocumentoNumero;
            destino.DocumentoNumeroComplemento = origem.DocumentoNumeroComplemento;
            destino.DocumentoTipoID = origem.DocumentoTipoID;
            destino.ParentescoID = origem.ParentescoID;
            destino.ResponsavelNome = origem.ResponsavelNome;
            destino.ResponsavelEmail = origem.ResponsavelEmail;
            destino.ResponsavelTelefone = origem.ResponsavelTelefone;
            destino.FotoArquivoNome = origem.FotoArquivoNome;
        }

        public bool PossuiHistoriaPendente(int id)
        {
            return dal.PossuiHistoriaPendente(id);
        }

        public RetornoModel ConfirmarIndicacao(IndicadoModel model)
        {
            RetornoModel retorno = new RetornoModel();

            retorno = ValidaDados(model);
            if (retorno.Sucesso)
            {
                HistoriaService historiaService = new HistoriaService();
                HistoriaMidiaService historiaMidiaService = new HistoriaMidiaService();
                Historia historiaTela = model.Historia;
                Indicado indicadoTela = model.Indicado;
                model.Historia = historiaService.Carregar(model.Codigo);
                model.Historia.Midias = historiaMidiaService.Listar(model.Historia.ID, true);
                model.Responsavel = (model.Historia.CodigoIndicadoResponsavel == model.Codigo);

                if (model.Historia.ID != historiaTela.ID)
                {
                    return new RetornoModel() { Sucesso = false, Mensagem = "Você não pode manipular um ID diferente do seu código." };
                }
                model.Historia.ComentarioIndicado = historiaTela.ComentarioIndicado;
                if (model.Responsavel)
                {
                    if ((model.Historia.IndicadoAprovacaoOriginal != enumAprovacao.aprovado) || (model.Historia.IndicadoAprovacaoResponsavel != enumAprovacao.pendente))
                    {
                        return new RetornoModel { Mensagem = "Não existe nenhuma pendência vinculada a este código.", Sucesso = false };
                    }
                    model.Historia.ComentarioIndicadoResponsavel = historiaTela.ComentarioIndicado;
                }
                else
                {
                    if (model.Historia.IndicadoAprovacaoOriginal != enumAprovacao.pendente)
                    {
                        return new RetornoModel { Mensagem = "Não existe nenhuma pendência vinculada a este código.", Sucesso = false };
                    }
                    model.Historia.ComentarioIndicadoOriginal = historiaTela.ComentarioIndicado;
                }
                if (historiaTela.Midias != null)
                {
                    if (model.Historia.Midias == null)
                    {
                        model.Historia.Midias = new List<HistoriaMidia>();
                    }
                    foreach (var htm in historiaTela.Midias)
                    {

                        if (htm.ID == 0)
                        {
                            htm.AdicionadoOrigem = ((model.Responsavel) ? enumAlteracaoOrigem.IndicadoResponsavel : enumAlteracaoOrigem.Indicado);
                            htm.InativoOrigem = enumAlteracaoOrigem.Nenhum;
                            htm.DataCadastro = DateTime.Now;
                            htm.HistoriaID = model.Historia.ID;
                            htm.ArquivoTipo = htm.ArquivoTipo;
                            model.Historia.Midias.Add(htm);
                            historiaMidiaService.Salvar(htm);
                        }
                        else if (htm.Inativo)
                        {
                            var midia = model.Historia.Midias.FirstOrDefault(t => t.ID == htm.ID);
                            if (midia != null)
                            {
                                midia.InativoOrigem = ((model.Responsavel) ? enumAlteracaoOrigem.IndicadoResponsavel : enumAlteracaoOrigem.Indicado);
                                midia.Inativo = true;
                                historiaMidiaService.Salvar(midia);
                                model.Historia.Midias.Remove(midia);
                            }
                        }
                    }
                }
                if (model.Responsavel)
                {
                    model.Indicado = dal.Carregar(model.Historia.IndicadoID.Value);

                    if (model.Indicado.ID != indicadoTela.ID)
                    {
                        return new RetornoModel() { Sucesso = false, Mensagem = "Você não pode manipular um ID diferente do seu código" };
                    }

                    ConfirmarIndicacaoUpdateIndicado(indicadoTela, model.Indicado);

                    model.Historia.TriagemAprovacao = enumAprovacao.pendente;
                    model.Historia.TriagemAprovacaoNormal = enumAprovacao.pendente;
                    model.Historia.IndicadoAprovacaoResponsavel = enumAprovacao.aprovado;
                    model.Historia.IndicadoAprovacao = enumAprovacao.aprovado;
                    model.Historia.AlteracaoOrigem = enumAlteracaoOrigem.IndicadoResponsavel;

                    model.Indicado.AlteracaoOrigem = enumAlteracaoOrigem.IndicadoResponsavel;
                }
                else
                {
                    if (model.Estrangeiro)
                    {
                        // Salva o RNE no DocumentoNumero e o Passaport no DocumentoNumeroComplemento
                        indicadoTela.DocumentoTipoID = enumDocumentoTipoNome.outro;
                    }
                    else
                    {
                        indicadoTela.DocumentoTipoID = enumDocumentoTipoNome.cpf;
                    }

                    model.Indicado = CarregarIndicadoPorDocumento(indicadoTela);
                    model.Historia.AlteracaoOrigem = enumAlteracaoOrigem.Indicado;

                    if (model.Indicado == null)
                    {
                        model.Indicado = indicadoTela;
                        model.Indicado.DataCadastro = DateTime.Now;
                    }
                    else
                    {
                        ConfirmarIndicacaoUpdateIndicado(indicadoTela, model.Indicado);
                    }

                    model.Indicado.AlteracaoOrigem = enumAlteracaoOrigem.Indicado;

                    if (MenorIdade(model.Indicado))
                    {
                        model.Historia.TriagemAprovacao = enumAprovacao.semNecessidade;
                        model.Historia.TriagemAprovacaoNormal = enumAprovacao.semNecessidade;
                        model.Historia.IndicadoAprovacaoResponsavel = enumAprovacao.pendente;
                        model.Historia.IndicadoAprovacaoOriginal = enumAprovacao.aprovado;
                        model.Historia.IndicadoAprovacao = enumAprovacao.pendente;
                        model.Historia.CodigoIndicadoResponsavel = historiaService.GeraCodigo();
                        //zera os dados do robo para disparo para o responsavel
                        model.Historia.DataEmailAguardandoAprovacao = DateTime.Now;
                        model.Historia.QuantidadeEmailAguardandoAprovacao = 0;
                    }
                    else // indicado maior de idade
                    {
                        model.Historia.TriagemAprovacao = enumAprovacao.pendente;
                        model.Historia.TriagemAprovacaoNormal = enumAprovacao.pendente;
                        model.Historia.IndicadoAprovacaoResponsavel = enumAprovacao.semNecessidade;
                        model.Historia.IndicadoAprovacaoOriginal = enumAprovacao.aprovado;
                        model.Historia.IndicadoAprovacao = enumAprovacao.aprovado;
                    }
                }

                //calculando cidade onde irá carregar a tocha
                CidadeParticipanteService cpService = new CidadeParticipanteService();
                model.Indicado.CidadeParticipanteID = cpService.CarregarPorCidade(model.Indicado.CidadeID).ID;
                model.Indicado.DataModificacao = DateTime.Now;
                model.Historia.DataModificacao = DateTime.Now;

                if (!String.IsNullOrEmpty(model.Indicado.FotoArquivoNome))
                {
                    model.IndicadoFotoInfo.file = model.Indicado.FotoArquivoNome;
                    model.Indicado.FotoArquivoNome = GerarFotoArquivoNome(model.IndicadoFotoInfo);
                }

                //insere Indicado no banco se nao existir com mesmo documento senao atualiza
                if (model.Responsavel)
                {
                    SalvarResponsavelAprovacao(model.Indicado);
                }
                else
                {
                    model.Indicado.ID = SalvarConfirmarIndicacaoIndicado(model.Indicado);
                }
                model.Historia.IndicadoID = model.Indicado.ID;
                historiaService.Salvar(model.Historia);

                try
                {
                    //enviar emails
                    if (model.Responsavel)
                    {
                        EnviarEmailsConfirmacaoResponsavel(model);
                    }
                    else
                    {
                        EnviarEmailsConfirmacaoIndicado(model);
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex);
                }
            }
            else
            {
                return retorno;
            }

            return new RetornoModel { Sucesso = true, Mensagem = "Ok" };
        }


        private void SalvarResponsavelAprovacao(Indicado indicado)
        {
            dal.InserirLog(indicado.ID);
            dal.AtualizarConfirmarIndicacaoResponsavel(indicado);
        }


        private bool MenorIdade(Indicado indicado)
        {
            var value = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.dataEvento);
            var dataEvento = DateTime.Parse(value);
            if (indicado.DataNascimento.AddYears(18) > dataEvento && indicado.DataNascimento.AddYears(12) < dataEvento)
            {
                return true;
            }
            return false;
        }

        private void EnviarEmailsConfirmacaoResponsavel(IndicadoModel model)
        {
            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "ConfirmacaoResponsavelParaIndicado.cshtml");
            string email = RazorUtil.Render(path, EmailModel.GetModel(model));
            EmailUtil.Send(model.Historia.IndicadoEmail, "Bradesco | Tocha Olímpica | Indicação autorizada", email);

            path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "ConfirmacaoResponsavelParaIndicador.cshtml");
            email = RazorUtil.Render(path, EmailModel.GetModel(model));
            EmailUtil.Send(model.Historia.IndicadorEmail, "Bradesco | Tocha Olímpica | Indicação autorizada", email);
        }

        private void EnviarEmailsConfirmacaoIndicado(IndicadoModel model)
        {
            if (!MenorIdade(model.Indicado))
            {
                string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "IndicadorConfirmacao.cshtml");
                string email = RazorUtil.Render(path, EmailModel.GetModel(model));
                EmailUtil.Send(model.Historia.IndicadorEmail, "Bradesco | Tocha Olímpica | Indicação aceita", email);
            }
            else
            {
                string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "Autorizacao.cshtml");
                string email = RazorUtil.Render(path, EmailModel.GetModel(model));
                EmailUtil.Send(model.Indicado.ResponsavelEmail, "Bradesco | Tocha Olímpica | Autorização", email);

                path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "IndicadoAguardandoResponsavelParaIndicado.cshtml");
                email = RazorUtil.Render(path, EmailModel.GetModel(model.Indicado));
                EmailUtil.Send(model.Historia.IndicadoEmail, "Bradesco | Tocha Olímpica | Aguardando autorização", email);

                path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "IndicadoMenorConfirmacaoParaIndicador.cshtml");
                email = RazorUtil.Render(path, EmailModel.GetModel(model));
                EmailUtil.Send(model.Historia.IndicadorEmail, "Bradesco | Tocha Olímpica | Aguardando autorização", email);
            }

        }

        private RetornoModel ValidaDados(IndicadoModel model)
        {
            if ((model != null) && (model.Indicado != null) && (model.Historia != null))
            {

                if (!model.Estrangeiro || model.Indicado.DocumentoTipoID == enumDocumentoTipoNome.cpf)
                {
                    model.Indicado.DocumentoNumero = model.Indicado.DocumentoNumero.Replace(new string[] { "-", "." }, "");
                }
                if (string.IsNullOrEmpty(model.Indicado.Nome))
                {
                    return new RetornoModel { Mensagem = "Nome inválido", Sucesso = false };
                }
                if (string.IsNullOrEmpty(model.Indicado.Genero))
                {
                    return new RetornoModel { Mensagem = "Selecione um gênero", Sucesso = false };
                }
                if (model.Indicado.CidadeID <= 0)
                {
                    return new RetornoModel { Mensagem = "Selecione uma cidade", Sucesso = false };
                }
                if (!EmailUtil.IsValidMailAdress(model.Indicado.Email))
                {
                    return new RetornoModel { Mensagem = "Email inválido", Sucesso = false };
                }

                var value = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.dataEvento);
                var dataEvento = DateTime.Parse(value);

                // ------------------- VALIDAÇÃO IDADE ------------------
                if (model.Indicado.DataNascimento == DateTime.MinValue)
                {
                    return new RetornoModel { Mensagem = "Você precisa informar sua data de nascimento.", Sucesso = false };
                }
                if (model.Indicado.DataNascimento.AddYears(12) > dataEvento)
                {
                    return new RetornoModel { Mensagem = "O participante deve ter no mínimo 12 anos.", Sucesso = false };
                }
                if (MenorIdade(model.Indicado))
                {
                    if (model.Indicado.ParentescoID <= 0)
                        return new RetornoModel { Mensagem = "Você precisa selecionar um parentesco.", Sucesso = false };

                    RetornoModel retorno = ValidaResponsavel(model.Indicado.ResponsavelEmail, model.Indicado.ResponsavelNome, model.Indicado.ResponsavelTelefone);
                    if (!retorno.Sucesso)
                        return retorno;
                }
                if (model.Estrangeiro)
                {
                    if (String.IsNullOrEmpty(model.Indicado.DocumentoNumeroComplemento))
                        return new RetornoModel { Mensagem = "Número de Passaporte inválido.", Sucesso = false };
                    if (String.IsNullOrEmpty(model.Indicado.DocumentoNumero))
                        return new RetornoModel { Mensagem = "Número do Registro Nacional de Estrangeiros inválido.", Sucesso = false };
                }
                else if (!model.Indicado.DocumentoNumero.IsValidCPF())
                {
                    return new RetornoModel { Mensagem = "Número de CPF inválido.", Sucesso = false };
                }

            }
            else
            {
                return new RetornoModel { Mensagem = "Dados obrigatórios não foram preenchidos.", Sucesso = false };
            }
            return new RetornoModel { Mensagem = "OK!", Sucesso = true };
        }

        private RetornoModel ValidaResponsavel(string email, string nome, string telefone)
        {

            if (String.IsNullOrEmpty(nome))
                return new RetornoModel { Mensagem = "Nome do responsável inválido.", Sucesso = false };
            if (String.IsNullOrEmpty(telefone))
                return new RetornoModel { Mensagem = "Telefone do responsável inválido.", Sucesso = false };
            if (!EmailUtil.IsValidMailAdress(email))
                return new RetornoModel { Mensagem = "Email do responsável inválido.", Sucesso = false };

            return new RetornoModel { Mensagem = "OK", Sucesso = true };
        }

        public static UploadFotoModel CarregarFotoInfo(string arquivoNome, int widthCustom = 0, int heightCustom = 0)
        {
            if (string.IsNullOrEmpty(arquivoNome))
            {
                return null;
            }
            UploadFotoModel model = new UploadFotoModel();
            model.file = arquivoNome;

            var auxNomeExt = arquivoNome.Split('.');
            if (auxNomeExt.Length > 1)
            {
                string nomeNovo = auxNomeExt[0];
                var auxNomePos = auxNomeExt[0].Split(new string[] { "__" }, StringSplitOptions.None);
                if (auxNomePos.Length > 2)
                {
                    var auxTam = auxNomePos[1].Split('x');
                    var auxPos = auxNomePos[2].Split('x');
                    model.width = Convert.ToInt32(auxTam[0]);
                    model.height = Convert.ToInt32(auxTam[1]);
                    model.x = Convert.ToInt32(auxPos[0]);
                    model.y = Convert.ToInt32(auxPos[1]);

                    double percentual = 1.00;
                    if (model.height > indicadoMaxHeightImage)
                    {
                        percentual = Convert.ToDouble(indicadoMaxHeightImage) / Convert.ToDouble(model.height);
                        model.height = indicadoMaxHeightImage;
                        model.width = Convert.ToInt32(model.width * percentual);

                    }
                    else if (model.width > indicadoMaxWidthImage)
                    {
                        percentual = Convert.ToDouble(indicadoMaxWidthImage) / Convert.ToDouble(model.width);
                        model.width = indicadoMaxWidthImage;
                        model.height = Convert.ToInt32(model.height * percentual);

                    }
                }
                else
                {
                    var arquivo = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.indicadoFotoDiretorio, model.file);
                    if (File.Exists(arquivo))
                    {
                        var aux = Image.FromFile(arquivo);
                        model.width = aux.Width;
                        model.height = aux.Height;
                        if (Convert.ToDouble(model.width) / Convert.ToDouble(model.height) < Convert.ToDouble(indicadoMaxWidth) / Convert.ToDouble(indicadoMaxHeight))
                        {
                            model.height = Convert.ToInt32(Convert.ToDouble(indicadoMaxWidth) * Convert.ToDouble(model.height) / Convert.ToDouble(model.width));
                            model.width = indicadoMaxWidth;
                            model.x = 0;
                            model.y = Convert.ToInt32((indicadoMaxHeight - model.height) / 2);
                        }
                        else
                        {
                            model.width = Convert.ToInt32(Convert.ToDouble(indicadoMaxHeight) * Convert.ToDouble(model.width) / Convert.ToDouble(model.height));
                            model.height = indicadoMaxHeight;
                            model.x = Convert.ToInt32((indicadoMaxWidth - model.width) / 2);
                            model.y = 0;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

                if ((widthCustom > 0) && (heightCustom > 0))
                {
                    double percentual = 1.00;
                    if (Convert.ToDouble(widthCustom) / Convert.ToDouble(heightCustom) < Convert.ToDouble(indicadoMaxWidth) / Convert.ToDouble(indicadoMaxHeight))
                    {
                        percentual = Convert.ToDouble(widthCustom) / Convert.ToDouble(indicadoMaxWidth);
                    }
                    else
                    {
                        percentual = Convert.ToDouble(heightCustom) / Convert.ToDouble(indicadoMaxHeight);
                    }
                    model.width = Convert.ToInt32(model.width * percentual);
                    model.height = Convert.ToInt32(model.height * percentual);
                    model.y = Convert.ToInt32(model.y * percentual);
                    model.x = Convert.ToInt32(model.x * percentual);
                }
            }
            return model;
        }

        public string GerarFotoArquivoNome(UploadFotoModel model)
        {
            var auxNomeExt = model.file.Split('.');
            if (auxNomeExt.Length > 1)
            {
                string nomeNovo = auxNomeExt[0];
                var auxNomePos = auxNomeExt[0].Split(new string[] { "__" }, StringSplitOptions.None);
                if (auxNomePos.Length > 1)
                {
                    nomeNovo = auxNomePos[0];
                }
                nomeNovo += "__" + model.width + "x" + model.height + "__" + model.x + "x" + model.y + "." + auxNomeExt[1];
                if (model.file != nomeNovo)
                {
                    RenomearImagemPerfil(model.file, nomeNovo);
                    model.file = nomeNovo;
                }
            }
            return model.file;
        }


        public RetornoModel<UploadFotoModel> Upload(HttpPostedFileBase arquivo)
        {
            string nomeArq = null;
            UploadFotoModel retorno = null;
            try
            {
                if ((arquivo != null) && (arquivo.ContentLength > 0))
                {
                    string content = arquivo.ContentType.Split('/')[0];
                    if (content == "image")
                    {
                        if (arquivo.ContentLength > ConfiguracaoAppUtil.GetAsInt(enumConfiguracaoLib.uploadImagemMaxSize))
                        {
                            int maxKb = Convert.ToInt32(ConfiguracaoAppUtil.GetAsInt(enumConfiguracaoLib.uploadImagemMaxSize) / 1024);
                            return new RetornoModel<UploadFotoModel> { Mensagem = "O tamanho máximo para upload de imagem é de " + ((maxKb > 1024) ? Convert.ToInt32(maxKb / 1024) + "Mb" : maxKb + "Kb") + ".", Sucesso = false };
                        }
                        //Salva o arquivo 
                        nomeArq = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
                        string caminhoArquivo = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.indicadoFotoDiretorio, nomeArq);
                        arquivo.SaveAs(caminhoArquivo);

                        if (!AntivirusUtil.Verificar(caminhoArquivo))
                        {
                            return new RetornoModel<UploadFotoModel> { Mensagem = "Este arquivo contém vírus, favor tentar outro arquivo.", Sucesso = false };
                        }
                        Image img = Image.FromFile(caminhoArquivo);
                        retorno = new UploadFotoModel
                        {
                            file = nomeArq,
                            width = img.Width,
                            height = img.Height,
                            x = Convert.ToInt32((indicadoMaxWidth - img.Width) / 2),
                            y = Convert.ToInt32((indicadoMaxHeight - img.Height) / 2)
                        };
                    }
                    else
                    {
                        return new RetornoModel<UploadFotoModel> { Mensagem = "Tipo de arquivo inválido.", Sucesso = false };
                    }
                }
                else
                {
                    return new RetornoModel<UploadFotoModel> { Mensagem = "Nenhum arquivo enviado.", Sucesso = false };
                }

            }
            catch (Exception e)
            {
                LogUtil.Error(e);
                return new RetornoModel<UploadFotoModel> { Mensagem = "Falha no upload, tente novamente!", Sucesso = false };
            }
            return new RetornoModel<UploadFotoModel> { Retorno = retorno, Mensagem = "Upload realizado com sucesso.", Sucesso = true };
        }

        public Indicado Carregar(int id)
        {
            return dal.Carregar(id);
        }

        public IList<Indicado> Listar(Int32 skip, Int32 take, bool mostraTodos, string palavraChave, int cidadeParticipante, string documentoNumero, int condutor, decimal nota)
        {
            IList<Indicado> lista = dal.Listar(skip, take, mostraTodos, palavraChave, cidadeParticipante, documentoNumero, condutor, nota);
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }

        public IList<ReportIndicadoPublicoModel> Listar()
        {
            IList<ReportIndicadoPublicoModel> lista = dal.ListarExcel();
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }

        public int UltimaHistoriaCadastrada(int indicadoID)
        {
            return dal.CarregaIDUltimaHistoriaCadastrada(indicadoID);
        }

        public int UltimaHistoriaModerada(int indicadoID)
        {
            return dal.CarregaIDUltimaHistoriaModerada(indicadoID);
        }
        public void AtualizaIDUltimaHistoriaModeracao(int indicadoID, int historiaID)
        {
            dal.AtualizaIDUltimaHistoriaModeracao(indicadoID, historiaID);
        }

        public IList<Indicado> ListarCidadeParticipante(CidadeParticipante cidadeParticipante, bool realocacao = false, bool condutor = false)
        {
            bool diferente = false;
            if (realocacao)
            {
                diferente = !cidadeParticipante.Inativo;
            }
            return dal.ListarCidadeParticipante(cidadeParticipante.ID, diferente, condutor);
        }

        public IList<Indicado> ListarIndicadosComReportsAbuso(Int32 skip, Int32 take, string palavraChave)
        {
            IList<Indicado> indicados = dal.ListarIndicadosComReports(skip, take, palavraChave);
            TotalRegistros = dal.TotalRegistros;
            return indicados;
        }

        public RetornoModel RealizarTriagemAbuso(IndicadoAbusoModel model)
        {
            RetornoModel retorno = new RetornoModel { Mensagem = "Ok", Sucesso = true };
            //inserindo log
            dal.InserirLog(model.Informacoes.Indicado.ID);

            HistoriaService service = new HistoriaService();

            Indicado indicado = dal.Carregar(model.Informacoes.Indicado.ID);
            if (indicado != null)
            {
                indicado.DataModificacao = DateTime.Now;
                indicado.AlteracaoOrigem = enumAlteracaoOrigem.TriagemAbuso;
                indicado.UsuarioID = model.Informacoes.Indicado.UsuarioID;
                indicado.RemoverGaleria = model.Informacoes.Indicado.RemoverGaleria;

                if (model != null)
                {
                    if (model.HistoriasReportadas != null)
                    {
                        foreach (HistoriaReportModel item in model.HistoriasReportadas)
                        {
                            retorno = service.RealizarTriagemAbuso(item, model.Informacoes.Indicado.UsuarioID ?? 0);
                            if (!retorno.Sucesso)
                            {
                                return retorno;
                            }
                        }
                    }
                }
                //atualizando dados do indicado
                dal.AtualizarIndicadoReportadoAbuso(indicado);
            }
            else
            {
                retorno = new RetornoModel { Mensagem = "Erro ao carregar os dados do indicado", Sucesso = false };
            }
            return retorno;
        }

        public IndicadoAbusoModel CarregaDadosTriagemAbuso(int indicadoID)
        {
            HistoriaService service = new HistoriaService();
            IndicadoAbusoModel retorno = new IndicadoAbusoModel();

            //carregando informações do indicado
            retorno.Informacoes = CarregaInformacoesIndicado(indicadoID);

            //carregando histórias reportadas do indicado e seus reports
            retorno.HistoriasReportadas = service.ListarHistoriasReportadas(retorno.Informacoes.Indicado.ID).ToList();

            return retorno;
        }

        public ListaGaleriaModel BuscarGaleria(GaleriaModel model, Configuracao configuracao)
        {
            var retorno = new ListaGaleriaModel();
            int nota = 0;
            int skip = configuracao.GaleriaQuantidadeHistorias * (model.Pagina - 1);
            int take = configuracao.GaleriaQuantidadeHistorias + 1;
            if (model.RetornarPaginasAnteriores)
            {
                skip = 0;
                take = (configuracao.GaleriaQuantidadeHistorias * model.Pagina) + 1;
            }
            if (!model.TemBusca)
            {
                nota = configuracao.GaleriaNotaCorte;
            }
            List<Indicado> indicados = dal.BuscarGaleria(skip, take, model.BuscaPalavraChave, nota, model.BuscaTipo);
            if (indicados.Count == take)
            {
                retorno.ProximaPagina = model.Pagina + 1;
            }
            else
            {
                retorno.ProximaPagina = 0;
            }
            retorno.Indicados = indicados.Take(take - 1).Select(t => t.ToIndicadoGaleriaModel(true)).ToList();
            if (configuracao.GaleriaRandonize)
            {
                retorno.Indicados = retorno.Indicados.Randomize().ToList();
            }
            return retorno;
        }

        public IndicadoGaleriaModel CarregarGaleria(string id)
        {
            string idStr = id.LeftOfIndexOf("-");
            IndicadoGaleriaModel model = null;
            if (idStr[0] == 'I')
            {
                int indicadoID = Convert.ToInt32(idStr.Substring(1));
                IIndicadoInternoDAL indicadoInternoDal = new IndicadoInternoADO();
                IndicadoInterno indicado = indicadoInternoDal.Carregar(indicadoID);
                if (indicado != null)
                {
                    model = indicado.ToIndicadoGaleriaModel();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                int indicadoID = Convert.ToInt32(idStr);
                Indicado indicado = dal.CarregarGaleria(indicadoID);
                if (indicado != null)
                {
                    model = indicado.ToIndicadoGaleriaModel(false);
                }
                else
                {
                    return null;
                }
            }
            if ((model != null) && (model.ID != id))
            {
                model = null;
            }
            return model;
        }

        private void RenomearImagemPerfil(string NomeOriginal, string NomeNovo)
        {

            string caminhoArquivo = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.indicadoFotoDiretorio, NomeOriginal);
            string caminhoArquivoNovo = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.indicadoFotoDiretorio, NomeNovo);

            if ((!File.Exists(caminhoArquivoNovo)) && (File.Exists(caminhoArquivo)))
                File.Copy(caminhoArquivo, caminhoArquivoNovo);
        }

        public InformacoesIndicadoModel CarregaInformacoesIndicado(int indicadoID)
        {
            InformacoesIndicadoModel retorno = new InformacoesIndicadoModel();

            retorno.Indicado = dal.Carregar(indicadoID);

            retorno.TotalIndicacoes = dal.QtdeHistorias(indicadoID);
            retorno.Aprovadas = dal.QtdeHistoriasAprovadas(indicadoID);
            retorno.Recusadas = dal.QtdeHistoriasNaoAprovadas(indicadoID);
            retorno.Moderadas = dal.QtdeHistoriasModeradas(indicadoID);

            HistoriaCategoriaService servHistoriaCategoria = new HistoriaCategoriaService();
            retorno.Qualidades = servHistoriaCategoria.Listar(indicadoID).ToList();

            CidadeParticipanteService servCidadeParticipante = new CidadeParticipanteService();
            retorno.CidadesParticipantes = servCidadeParticipante.ListarAtivas().ToList();
            //if (dal.EstaNaGaleria(indicadoID, historiaID))
            //    retorno.LinkGaleria = "http://google.com";

            return retorno;
        }

        public void Realocar(int indicadoID, int cidadeParticipanteID, bool condutor)
        {
            dal.Realocar(indicadoID, cidadeParticipanteID, condutor);
        }

        public void SalvarCondutor(Indicado indicado)
        {
            //inserindo log
            dal.InserirLog(indicado.ID);
            //salvando flag de condutor
            dal.SalvarCondutor(indicado);

        }
        public void SalvarRemoverGaleria(Indicado indicado)
        {
            //inserindo log
            dal.InserirLog(indicado.ID);
            //salvando flag de condutor
            dal.SalvarRemoverGaleria(indicado);
        }

        public VizualizarIndicadoModel VisualizarIndicado(int id, bool mostraTodos)
        {
            VizualizarIndicadoModel model = new VizualizarIndicadoModel();
            model.Infos = CarregaInformacoesIndicado(id);
            HistoriaService service = new HistoriaService();
            model.Historias = service.ListarHistoriasComMidias(id, mostraTodos);

            return model;
        }

        public void AtualizarIndicadoModeracao(int indicadoID, int usuarioID)
        {
            dal.InserirLog(indicadoID);
            dal.AtualizarIndicadoModeracao(indicadoID, usuarioID);
        }

        public void AlterarCidadeParticipante(Indicado indicado)
        {
            if (indicado != null)
            {
                dal.InserirLog(indicado.ID);
                dal.AtualizarCidadeParticipante(indicado);
            }
        }

        public IList<ExportaIndicadoModel> ExportaIndicados()
        {
            return dal.ExportaIndicados();
        }


        public ReportIndicadoHistoriasModel ExportarIndicadoHistorias(int id)
        {
            ReportIndicadoHistoriasModel retorno = null;

            //Carrega os dados do Indicado e hstorias
            VizualizarIndicadoModel model = new VizualizarIndicadoModel();
            model.Infos = CarregaInformacoesIndicado(id);
            HistoriaService service = new HistoriaService();
            model.Historias = service.ListarHistoriasComMidias(id, true);

            //converte os dados do indicado para a model do report
            retorno = new ReportIndicadoHistoriasModel();
            retorno.rptIndicado = new List<ReportIndicadoHistoriasModel.ReportIndicado>();
            retorno.rptIndicado.Add(new ReportIndicadoHistoriasModel.ReportIndicado()
                {
                    ID = model.Infos.Indicado.ID,
                    Nome = model.Infos.Indicado.Nome,
                    CidadeUF = model.Infos.Indicado.Cidade.Nome + " - " + model.Infos.Indicado.Estado.UF,
                    DataCadastro = model.Infos.Indicado.DataCadastro,
                    DataModificacao = model.Infos.Indicado.DataModificacao,
                    DataNascimento = model.Infos.Indicado.DataNascimento,
                    Email = model.Infos.Indicado.Email,
                    ModificadoPor = model.Infos.Indicado.AlteracaoOrigem.Description(),
                    ParticiparEm = model.Infos.Indicado.CidadeParticipante.Cidade.Nome + " - " + model.Infos.Indicado.CidadeParticipante.Cidade.Estado.UF,
                    qtdHistoriasAprovadas = model.Infos.Aprovadas,
                    qtdHistoriasModeradas = model.Infos.Moderadas,
                    qtdHistoriasRecusadas = model.Infos.Recusadas,
                    qtdIndicacoes = model.Infos.TotalIndicacoes

                });

            retorno.rptIndicadoQualidade = new List<ReportIndicadoHistoriasModel.ReportIndicadoQualidade>();
            foreach (var item in model.Infos.Qualidades)
            {
                retorno.rptIndicadoQualidade.Add(new ReportIndicadoHistoriasModel.ReportIndicadoQualidade()
                    {
                        QualidadeID = item.ID,
                        QualidadeNome = item.Nome,
                        IndicadoID = model.Infos.Indicado.ID
                    });
            }

            retorno.rptIndicadoHistoria = new List<ReportIndicadoHistoriasModel.reportIndicadoHistoria>();
            foreach (var item in model.Historias)
            {
                retorno.rptIndicadoHistoria.Add(new ReportIndicadoHistoriasModel.reportIndicadoHistoria()
                    {
                        HistoriaID = item.Historia.ID,
                        Categoria = item.Historia.HistoriaCategoria.Nome,
                        IndicadorNome = item.Historia.IndicadorNome,
                        NotaHistoria = string.Format("{0:0.00}", item.Historia.Nota),
                        StatusIndicado = item.Historia.IndicadoAprovacao.Description(),
                        StatusModeracao = (item.Historia.IndicadoAprovacao == enumAprovacao.aprovado 
                                           || item.Historia.IndicadoAprovacao ==  enumAprovacao.aprovadoComRessalva 
                                          ? (item.Historia.ModeracaoEncerrada ? "Encerrada" : "Em Andamento") 
                                          : "Não Iniciada"),
                        StatusTriagem = item.Historia.TriagemAprovacao.Description(),
                        TextoHistoria = item.Historia.Texto,
                        TituloHistoria = item.Historia.Titulo,
                        IndicadoID = model.Infos.Indicado.ID
                    });
            }


            return retorno;
        }
    }
}

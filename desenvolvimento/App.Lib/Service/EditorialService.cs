using Facebook;
using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    public class EditorialService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IEditorialDAL dal = new EditorialADO();

        private int Salvar(Editorial editorial)
        {
            editorial.DataModificacao = DateTime.Now;
            if (editorial.ID > 0)
            {
                editorial.DataModificacao = DateTime.Now;
                dal.Atualizar(editorial);
            }
            else
            {
                editorial.ID = dal.Inserir(editorial);
            }

            return editorial.ID;
        }

        public RetornoModel SalvarEditorial(Editorial editorial)
        {
            RetornoModel retorno = null;
            retorno = Validar(editorial);

            if (retorno.Sucesso)
                editorial.ID = Salvar(editorial);

            return retorno;
        }

        public RetornoModel Inativar(Editorial editorial)
        {
            RetornoModel retorno = null;

            editorial.DataModificacao = DateTime.Now;

            dal.Inativar(editorial);

            if (editorial.ID > 0)
            {
                AtualizarCache(Carregar(editorial.ID));
            }

            return retorno;

        }

        public RetornoModel AtualizarCache(Editorial editorial)
        {
            RetornoModel retorno = null;
            EditorialModel model = new EditorialModel();

            model.Editorial = editorial;
            model.Conteudo = new EditorialBlocoService().ListarPorEditorialID(editorial.ID, true);

            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialTemplateConteudo, "ConteudoTemplate.cshtml");
            editorial.ConteudoHTML = RazorUtil.Render(path, EditorialModel.GetModel(model));

            editorial.DataModificacao = DateTime.Now;

            dal.AtualizarCache(editorial);

            return retorno;
        }


        public Editorial Carregar(int id)
        {
            return dal.Carregar(id);
        }

        public IList<Editorial> Listar()
        {
            return dal.Listar();
        }

        public IList<Editorial> Listar(Int32 skip, Int32 take, string palavraChave = "")
        {
            IList<Editorial> lista = dal.Listar(skip, take, palavraChave);
            TotalRegistros = dal.TotalRegistros;

            return lista;
        }

        private RetornoModel Validar(Editorial editorial)
        {
            if (editorial == null)
            {
                return new RetornoModel { Mensagem = "Dados obrigatórios não fornecidos.", Sucesso = false };
            }
            if (string.IsNullOrEmpty(editorial.IndicadoNome))
            {
                return new RetornoModel { Mensagem = "Nome do Indicado inválido.", Sucesso = false };
            }
            if (string.IsNullOrEmpty(editorial.TopoTitulo))
            {
                return new RetornoModel { Mensagem = "Título inválido.", Sucesso = false };
            }
            if (editorial.DataExibicao == null ||
                editorial.DataExibicao == DateTime.MinValue ||
                editorial.DataExibicao == DateTime.MaxValue)
            {
                return new RetornoModel { Mensagem = "Data de Exibição inválida.", Sucesso = false };
            }
            if (string.IsNullOrWhiteSpace(editorial.IndicadoFoto))
            {
                return new RetornoModel { Mensagem = "Foto do Indicado inválida.", Sucesso = false };
            }
            if (string.IsNullOrWhiteSpace(editorial.TopoImagem1920))
            {
                return new RetornoModel { Mensagem = "Imagem de 1920 inválida.", Sucesso = false };
            }

            return new RetornoModel { Mensagem = "OK", Sucesso = true };
        }

        public RetornoModel<string> Upload(HttpPostedFileBase arquivo, string nomeArq = null, bool convertFile = true)
        {
            try
            {
                if ((arquivo != null) && (arquivo.ContentLength > 0))
                {
                    string content = arquivo.ContentType.Split('/')[0];
                    if (content == "image" || content == "video")
                    {
                        //Salva o arquivo 
                        if (string.IsNullOrWhiteSpace(nomeArq))
                            nomeArq = Guid.NewGuid().ToString();

                        string nomeArqSalvar = nomeArq;
                        if (content == "image")
                        {
                            if (arquivo.ContentLength > ConfiguracaoAppUtil.GetAsInt(enumConfiguracaoLib.uploadImagemMaxSize))
                            {
                                int maxKb = Convert.ToInt32(ConfiguracaoAppUtil.GetAsInt(enumConfiguracaoLib.uploadImagemMaxSize) / 1024);
                                return new RetornoModel<string> { Mensagem = "O tamanho máximo para upload de imagem é de " + ((maxKb > 1024) ? Convert.ToInt32(maxKb / 1024) + "Mb" : maxKb + "Kb") + ".", Sucesso = false };
                            }
                            nomeArqSalvar += Path.GetExtension(arquivo.FileName);
                            nomeArq = nomeArqSalvar;
                        }
                        else
                        {
                            if (arquivo.ContentLength > ConfiguracaoAppUtil.GetAsInt(enumConfiguracaoLib.uploadVideoMaxSize))
                            {
                                int maxKb = Convert.ToInt32(ConfiguracaoAppUtil.GetAsInt(enumConfiguracaoLib.uploadVideoMaxSize) / 1024);
                                return new RetornoModel<string> { Mensagem = "O tamanho máximo para upload de vídeo é de " + ((maxKb > 1024) ? Convert.ToInt32(maxKb / 1024) + "Mb" : maxKb + "Kb") + ".", Sucesso = false };
                            }
                            if (convertFile)
                                nomeArqSalvar += ".orig" + Path.GetExtension(arquivo.FileName);
                            else
                                nomeArqSalvar += Path.GetExtension(arquivo.FileName);
                        }
                        string caminhoArquivo = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArqSalvar);
                        arquivo.SaveAs(caminhoArquivo);

                        if (!AntivirusUtil.Verificar(caminhoArquivo))
                        {
                            return new RetornoModel<string> { Mensagem = "Este arquivo contém vírus, favor tentar outro arquivo.", Sucesso = false };
                        }
                        else
                        {
                            if (content == "video")
                            {
                                if (convertFile)
                                    Task.Run(() => ConverterVideo(caminhoArquivo, nomeArq));
                            }
                        }
                    }
                    else
                    {
                        return new RetornoModel<string> { Mensagem = "Tipo de arquivo inválido.", Sucesso = false };
                    }
                }
                else
                {
                    return new RetornoModel<string> { Mensagem = "Nenhum arquivo enviado.", Sucesso = false };
                }

            }
            catch (Exception e)
            {
                LogUtil.Error(e);
                return new RetornoModel<string> { Mensagem = "Falha no upload, tente novamente!", Sucesso = false };
            }
            return new RetornoModel<string> { Retorno = nomeArq, Mensagem = "Upload realizado com sucesso", Sucesso = true };
        }

        private void ConverterVideo(string caminhoArquivo, string nomeArq)
        {
            var ffMpeg = new FFMpegConverter();
            ffMpeg.ConvertMedia(caminhoArquivo, ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + "Temp.mp4"), Format.mp4);
            ffMpeg.ConvertMedia(caminhoArquivo, ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + "Temp.webm"), Format.webm);
            ffMpeg.ConvertMedia(caminhoArquivo, ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + "Temp.ogv"), Format.ogg);

            File.Move(ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + "Temp.webm"), ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + ".webm"));
            File.Move(ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + "Temp.ogv"), ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + ".ogv"));
            File.Move(ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + "Temp.mp4"), ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.editorialMidiaDiretorio, nomeArq + ".mp4"));
        }

        /// <summary>
        /// Contar quantas historias estão publicadas e dentro da data
        /// </summary>
        /// <returns></returns>
        public int ContarPublicados()
        {
            return dal.ContarPublicados();
        }

        /// <summary>
        /// Carrega a última historia publicada e dentro da data
        /// </summary>
        /// <returns></returns>
        public Editorial CarregarUltimaPublicada(DateTime data)
        {
            Editorial model = dal.CarregarUltimaPublicada(data, 0);
            model.EditorialAnterior = dal.CarregarUltimaPublicada(model.DataExibicao, model.ID);
            return model;
        }

        /// <summary>
        /// Carrega detalhe de uma historia
        /// </summary>
        /// <returns></returns>
        public Editorial CarregarDetalhe(string url)
        {
            Editorial model = dal.Carregar(url);
            model.EditorialAnterior = dal.CarregarUltimaPublicada(model.DataExibicao, model.ID);
            return model;
        }

        /// <summary>
        /// Listar histórias publicadas e dentro da data em ordem de 
        /// </summary>
        /// <returns></returns>
        public IList<EditorialResumoModel> ListarPublicados()
        {
            return dal.ListarPublicados().Select(t => new EditorialResumoModel().CopyFrom(t)).ToList();
        }


        public string GetUrlLivre(int id, string url, string titulo)
        {
            string retorno = string.Empty;
            int count = 1;
            

            if (string.IsNullOrWhiteSpace(url))
                if (!string.IsNullOrWhiteSpace(titulo))
                    url = titulo.UrlFormatar();


            if (!string.IsNullOrWhiteSpace(url))
            {
                retorno = url.UrlFormatar();

                while (!dal.UrlLivre(id, retorno))
                {
                    retorno = string.Format("{0}-{1}", url, count++).UrlFormatar();
                }
            }

            return retorno;
        }

        public IList<string> getListUrls()
        {
            return dal.getListUrls();
        }
    }
}

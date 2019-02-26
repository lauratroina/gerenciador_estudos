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
    public class EditorialBlocoService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IEditorialBlocoDAL dal = new EditorialBlocoADO();

        private int Salvar(EditorialBloco bloco)
        {
            bloco.DataModificacao = DateTime.Now;
            if (bloco.ID > 0)
            {
                bloco.DataModificacao = DateTime.Now;
                dal.Atualizar(bloco);
            }
            else
            {
                bloco.ID = dal.Inserir(bloco);
            }

            if (bloco.ID > 0)
            {
                (new EditorialService()).AtualizarCache(new EditorialService().Carregar(bloco.EditorialID));
            }

            return bloco.ID;
        }


        public RetornoModel SalvarBloco(EditorialBloco bloco)
        {
            RetornoModel retorno = null;
            retorno = Validar(bloco);

            if (retorno.Sucesso)
                bloco.ID = Salvar(bloco);

            return retorno;
        }

        public RetornoModel Inativar(EditorialBloco bloco)
        {
            RetornoModel retorno = null;

            bloco.DataModificacao = DateTime.Now;

            dal.Inativar(bloco);
            
            if (bloco.ID > 0)
            {
                (new EditorialService()).AtualizarCache(new EditorialService().Carregar(bloco.EditorialID));
            }

            return retorno;

        }


        public EditorialBloco Carregar(int id)
        {
            EditorialBloco retorno = dal.Carregar(id);

            retorno.ListaArquivos = new EditorialBlocoArquivoService().ListarPorBlocoID(retorno.ID, false);

            return retorno;
        }

        public IList<EditorialBloco> ListarPorEditorialID(int editorialID, bool apenasAtivos = false, Int32 skip = 0, Int32 take = 300000, string palavraChave = "")
        {
            IList<EditorialBloco> lista = dal.Listar(editorialID, apenasAtivos, skip, take, palavraChave);
            TotalRegistros = dal.TotalRegistros;

            return lista;
        }

        public IList<EditorialBloco> Listar()
        {
            return dal.Listar();
        }

        public IList<EditorialBloco> Listar(Int32 skip, Int32 take)
        {
            IList<EditorialBloco> lista = dal.Listar(skip, take);
            TotalRegistros = dal.TotalRegistros;

            return lista;
        }

        private RetornoModel Validar(EditorialBloco bloco)
        {
            if (bloco == null)
            {
                return new RetornoModel { Mensagem = "Dados obrigatórios não fornecidos.", Sucesso = false };
            }
            if (string.IsNullOrWhiteSpace(bloco.Nome))
            {
                return new RetornoModel { Mensagem = "Nome do item inválido.", Sucesso = false };
            }
            if (bloco.Ordem < 0)
            {
                return new RetornoModel { Mensagem = "Ordem do item inválida.", Sucesso = false };
            }
            //1 - Titulo
            //2 - SubTitulo
            //3 - Texto
            //4 - Citação
            //5 - Galeria Imagem/Video
            //6 - Galeria Audio
            //7 - Compartilhe
            //8 - Espaçador
            //9 - Imagem Full
            //10- Imagem
            if (string.IsNullOrWhiteSpace(bloco.Texto))
            {
                if (bloco.Tipo == 1 || //Titulo
                    bloco.Tipo == 2 || //SubTitulo
                    bloco.Tipo == 3)   //Texto
                {
                    return new RetornoModel { Mensagem = "Texto do item inválido.", Sucesso = false };
                }
            }
            if (string.IsNullOrWhiteSpace(bloco.TextoResumido))
            {
                if (bloco.Tipo == 9) //Imagem Full
                {
                    return new RetornoModel { Mensagem = "Título do item inválido.", Sucesso = false };
                }
            }

            if (string.IsNullOrWhiteSpace(bloco.NomeArquivo))
            {
                if (bloco.Tipo == 9) //Imagem Full
                {
                    return new RetornoModel { Mensagem = "Arquivo do item inválido.", Sucesso = false };
                }
            }

            return new RetornoModel { Mensagem = "OK", Sucesso = true };
        }


        public RetornoModel<string> Upload(HttpPostedFileBase arquivo)
        {
            string nomeArq = null;
            try
            {
                if ((arquivo != null) && (arquivo.ContentLength > 0))
                {
                    string content = arquivo.ContentType.Split('/')[0];
                    if (content == "image" || content == "video")
                    {
                        //Salva o arquivo 
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
                            nomeArqSalvar += ".orig" + Path.GetExtension(arquivo.FileName);
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
    }
}

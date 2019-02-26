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
    public class EditorialBlocoArquivoService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IEditorialBlocoArquivoDAL dal = new EditorialBlocoArquivoADO();

        private int Salvar(EditorialBlocoArquivo arquivo)
        {
            arquivo.DataModificacao = DateTime.Now;
            if (arquivo.ID > 0)
            {
                arquivo.DataModificacao = DateTime.Now;
                dal.Atualizar(arquivo);
            }
            else
            {
                arquivo.ID = dal.Inserir(arquivo);
            }

            if (arquivo.ID > 0)
            {
                (new EditorialService()).AtualizarCache(new EditorialService().Carregar(new EditorialBlocoService().Carregar(arquivo.EditorialBlocoID).EditorialID));
            }
            return arquivo.ID;
        }

        public RetornoModel SalvarArquivo(EditorialBlocoArquivo arquivo)
        {
            RetornoModel retorno = null;
            retorno = Validar(arquivo);

            if (retorno.Sucesso)
                arquivo.ID = Salvar(arquivo);

            return retorno;
        }

        public RetornoModel Inativar(EditorialBlocoArquivo arquivo)
        {
            RetornoModel retorno = null;

            arquivo.DataModificacao = DateTime.Now;

            dal.Inativar(arquivo);

            if (arquivo.ID > 0)
            {
                (new EditorialService()).AtualizarCache(new EditorialService().Carregar(new EditorialBlocoService().Carregar(arquivo.EditorialBlocoID).EditorialID));
            }
            return retorno;

        }


        public EditorialBlocoArquivo Carregar(int id)
        {
            return dal.Carregar(id);
        }

        public IList<EditorialBlocoArquivo> Listar()
        {
            return dal.Listar();
        }

        public IList<EditorialBlocoArquivo> Listar(Int32 skip, Int32 take)
        {
            IList<EditorialBlocoArquivo> lista = dal.Listar(skip, take);
            TotalRegistros = dal.TotalRegistros;

            return lista;
        }

        private RetornoModel Validar(EditorialBlocoArquivo arquivo)
        {
            if (arquivo == null)
            {
                return new RetornoModel { Mensagem = "Dados obrigatórios não fornecidos.", Sucesso = false };
            }
            if (string.IsNullOrEmpty(arquivo.NomeArquivo))
            {
                if (arquivo.Tipo == "M")
                    return new RetornoModel { Mensagem = "Imagem inválida.", Sucesso = false };
                else
                    return new RetornoModel { Mensagem = "Audio inválido.", Sucesso = false };
            }

            return new RetornoModel { Mensagem = "OK", Sucesso = true };
        }


        public  IList<EditorialBlocoArquivo> ListarPorBlocoID(int blocoID, bool apenasAtivos = false)
        {
            return dal.ListarPorBlocoID(blocoID, apenasAtivos);
        }

        public RetornoModel<string> Upload(HttpPostedFileBase arquivo)
        {
            string nomeArq = null;
            try
            {
                if ((arquivo != null) && (arquivo.ContentLength > 0))
                {
                    string content = arquivo.ContentType.Split('/')[0];
                    if (content == "image" || content == "video" || content == "audio")
                    {
                        //Salva o arquivo 
                        nomeArq = Guid.NewGuid().ToString();
                        string nomeArqSalvar = nomeArq;
                        if (content == "image")
                        {
                            nomeArqSalvar += Path.GetExtension(arquivo.FileName);
                            nomeArq = nomeArqSalvar;
                        }
                        else if (content == "audio")
                        {
                            nomeArqSalvar += Path.GetExtension(arquivo.FileName);
                            nomeArq = nomeArqSalvar;
                        }
                        else
                        {
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

        public void ExcluirArquivo(int id)
        {
            EditorialBlocoArquivo arquivo = dal.Carregar(id);

            dal.Excluir(id);

            if (arquivo.ID > 0)
            {
                (new EditorialService()).AtualizarCache(new EditorialService().Carregar(new EditorialBlocoService().Carregar(arquivo.EditorialBlocoID).EditorialID));
            }
        }
    }
}

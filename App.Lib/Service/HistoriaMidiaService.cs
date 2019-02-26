using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Util;
using BradescoNext.Lib.Util.Enumerator;

namespace BradescoNext.Lib.Service
{
    public class HistoriaMidiaService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IHistoriaMidiaDAL dal = new HistoriaMidiaADO();

        public void Salvar(HistoriaMidia midia)
        {
            if (midia.ID > 0)
            {
                dal.Atualizar(midia);
            }
            else
            {
                dal.Inserir(midia);
            }
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
                        string caminhoArquivo = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArqSalvar);
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

        public void ConverterVideo(string caminhoArquivo, string nomeArq)
        {
            var ffMpeg = new FFMpegConverter();
            ffMpeg.ConvertMedia(caminhoArquivo, ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + "Temp.mp4"), Format.mp4);
            ffMpeg.ConvertMedia(caminhoArquivo, ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + "Temp.webm"), Format.webm);
            ffMpeg.ConvertMedia(caminhoArquivo, ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + "Temp.ogv"), Format.ogg);

            File.Move(ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + "Temp.webm"), ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + ".webm"));
            File.Move(ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + "Temp.ogv"), ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + ".ogv"));
            File.Move(ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + "Temp.mp4"), ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + ".mp4"));
        }

        public static bool VerificaVideoConvertido(string nomeArq)
        {
            return File.Exists(ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.historiaMidiaDiretorio, nomeArq + ".mp4"));
        }


        public HistoriaMidia Carregar(int id)
        {
            return dal.Carregar(id);
        }

        public IList<HistoriaMidia> Listar(int historiaID, bool somenteAtivos)
        {
            return dal.Listar(historiaID, somenteAtivos);
        }


    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Util;
using BradescoNext.Lib.Util.Enumerator;

namespace BradescoNext.Lib.Models
{
    public class EditorialModel<T>
    {
        public string PathImagens { get; set; }
        public string PathApp { get; set; }
        public string PathAppSite { get; set; }
        public string PathHost { get; set; }
        public T Model { get; set; }

    }

    public class EditorialModel
    {
        public Editorial Editorial { get; set; }

        public IList<EditorialBloco> Conteudo { get; set; }

        public EditorialBloco Bloco { get; set; }
        public EditorialBlocoArquivo Arquivo { get; set; }
        public IList<sTipo> Tipos { get; set; }
        public IList<sTipo> Diposicoes { get; set; }
        public IList<sTipo> Alinhamentos { get; set; }

        public EditorialModel()
        {
            Editorial = new Editorial();
            Editorial.DataExibicao = DateTime.Now;
            Conteudo = new List<EditorialBloco>();
            Bloco = new EditorialBloco();
            Bloco.ListaArquivos = new List<EditorialBlocoArquivo>();
            Arquivo = new EditorialBlocoArquivo();

            Tipos = new List<sTipo>();
            Tipos.Add(new sTipo() { ID = 1, Nome = "Título" });
            Tipos.Add(new sTipo() { ID = 2, Nome = "Subtítulo" });
            Tipos.Add(new sTipo() { ID = 3, Nome = "Texto" });
            Tipos.Add(new sTipo() { ID = 10, Nome = "Imagem" });
            Tipos.Add(new sTipo() { ID = 4, Nome = "Citação" });
            Tipos.Add(new sTipo() { ID = 5, Nome = "Galeria Imagens/Video" });
            Tipos.Add(new sTipo() { ID = 6, Nome = "Galeria Audio" });
            Tipos.Add(new sTipo() { ID = 7, Nome = "Compartilhe" });
            Tipos.Add(new sTipo() { ID = 8, Nome = "Espaçador" });
            Tipos.Add(new sTipo() { ID = 9, Nome = "Imagem Full" });
            Tipos.Add(new sTipo() { ID = 11, Nome = "Facebook Optin" });

            Diposicoes = new List<sTipo>();
            Diposicoes.Add(new sTipo() { ID = 6, Nome = "Inteiro (1024px)" });
            Diposicoes.Add(new sTipo() { ID = 3, Nome = "Metade (512px)" });
            Diposicoes.Add(new sTipo() { ID = 2, Nome = "1/3 (340px)" });
            Diposicoes.Add(new sTipo() { ID = 4, Nome = "2/3 (680px)" });

            Alinhamentos = new List<sTipo>();
            Alinhamentos.Add(new sTipo() { ID = 1, Nome = "Esquerda" });
            Alinhamentos.Add(new sTipo() { ID = 2, Nome = "Centralizado" });
        }

        public struct sTipo
        {
            public int ID { get; set; }
            public string Nome { get; set; }
        }

        public static EditorialModel<T> GetModel<T>(T model, bool absoluto = false)
        {
            EditorialModel<T> editorialModel = new EditorialModel<T>();
            editorialModel.Model = model;
            editorialModel.PathImagens = ConfiguracaoAppUtil.Get(enumConfiguracaoLib.editorialMidiaUrl);

            if (editorialModel.PathImagens.StartsWith("~"))
            {
                if (HttpContext.Current != null)
                {
                    editorialModel.PathHost = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;
                    if ((HttpContext.Current.Request.Url.Port != 80) && (HttpContext.Current.Request.Url.Port != 443))
                    {
                        editorialModel.PathHost += ":" + HttpContext.Current.Request.Url.Port.ToString();
                    }
                    if (absoluto)
                    {
                        editorialModel.PathApp = editorialModel.PathHost + VirtualPathUtility.ToAbsolute("~/");
                        editorialModel.PathAppSite = editorialModel.PathHost + VirtualPathUtility.ToAbsolute("~/../");
                        editorialModel.PathImagens = editorialModel.PathHost + VirtualPathUtility.ToAbsolute(editorialModel.PathImagens);
                    }
                    else
                    {
                        editorialModel.PathApp = VirtualPathUtility.ToAbsolute("~/");
                        editorialModel.PathAppSite = VirtualPathUtility.ToAbsolute("~/../");
                        editorialModel.PathImagens = VirtualPathUtility.ToAbsolute(editorialModel.PathImagens);
                    }
                    
                }
                else
                {
                    editorialModel.PathHost = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.hostUrlWithoutRequest);
                    if (absoluto)
                    {
                        editorialModel.PathApp = editorialModel.PathHost + ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.appContextoWithoutRequest);
                        editorialModel.PathAppSite = editorialModel.PathImagens.Replace("~", editorialModel.PathApp) + "/../";
                        editorialModel.PathImagens = editorialModel.PathImagens.Replace("~", editorialModel.PathApp);
                    }
                    else
                    {
                        editorialModel.PathApp = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.appContextoWithoutRequest);
                        editorialModel.PathAppSite = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.appContextoWithoutRequest) + "/../";
                        editorialModel.PathImagens = editorialModel.PathImagens.Replace("~", editorialModel.PathApp);
                    }
                }
            }
            
            return editorialModel;

        }
    }

    public class EditorialResumoModel
    {

        public string IndicadoFoto { get; set; }

        public string IndicadoNome { get; set; }

        public string TopoTexto { get; set; }

        public string Url { get; set; }

    }
}


using System.ComponentModel;
namespace App.Lib.Enumerator
{
    public enum enumConfiguracaoLib
    {
        emailTemplateDiretorio,
        emailTemplateImagemUrl,
        chaveCriptografia,
        uploadImagemMaxSize,
        uploadVideoMaxSize,
        MidiasDiretorio,
        MidiasUrl,
        registrosPagina,
        qntdRegistrosInovadorPagina,
        siteIndisponivel
    }

    public enum enumUsuarioException
    {
        nenhum,
        usuarioNaoEncontrado,
        senhaInvalida,
        usuarioInativo,
        usuarioJaCadastradoComLogin
    }

    public enum enumTipoBusca
    {
        [Description("Tudo")]
        tudo,
        [Description("Nome do conector")]
        Conector,
        [Description("Nome do inovador")]
        Inovador,
        [Description("Titulo do artigo")]
        Artigo,
        [Description("Titulo do projeto")]
        Projeto,
        [Description("Titulo do estudo de caso")]
        EstudoDeCaso
    }
    public enum enumTemplate
    {
        ArtigoExtenso,
        ArtigoSimples,
        EstudoCaso,
        Banner,
        Citacao,
        Conector,
        Convidados,
        Inovador,
        Projeto,
        Tag
    }
    public enum enumTipoRegistro
    {
        artigos,
        estudosCaso,
        projetos,
        conectores,
        inovadores,
        banners,
        bannersBuraco,
        citacoes,
        citacoesBuraco,
        tags,
        convidados,
        compartilhamentos,
        comentarios
    }
    
}

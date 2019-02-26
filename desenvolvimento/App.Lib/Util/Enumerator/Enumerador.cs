
namespace App.Lib.Util.Enumerator
{
    public enum enumConfiguracaoGeral
    {
        sessionModelKey,
        sessionModelKeyAdmin,
        itensPorPagina,
        logCategory,
        facebookAppID,
        facebookAppSecret,
        facebookAppPermissao,
        linkedinAppID,
        linkedinAppSecret,
        httpsHabilitado,
        urlDominioReplace,
        cookieDominio,
        emailHost,
        emailPort,
        emailSSL,
        emailUser,
        emailPass,
        emailDefaultSender,
        emailSecurityProtocol,
        antivirusPrograma,
        antivirusParametros,
        antivirusVerificacao,
        razorNamespaces,
        hostUrlWithoutRequest,
        hostUrlSite,
        appContextoWithoutRequest
    }

    public enum enumReportFormat
    {
        Excel,
        PDF,
        Image,
        HTML,//Nao suportado pelo report viewer
        CSV,//Nao suportado pelo report viewer
        TXT//Nao suportado pelo report viewer
    }
}

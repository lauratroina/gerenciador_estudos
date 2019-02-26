using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Util;
using RazorEngine.Text;


namespace BradescoNext.Lib.Service
{
    public class CompartilheService
    {
        public RetornoModel EnviarEmail(Compartilhe entidade)
        {
            if (string.IsNullOrEmpty(entidade.DeNome))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Seu Nome é obrigatório" };
            }
            if (string.IsNullOrEmpty(entidade.DeEmail))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Seu Email é obrigatório" };
            }
            if (string.IsNullOrEmpty(entidade.ParaNome))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Nome do Amigo é obrigatório" };
            }
            if (string.IsNullOrEmpty(entidade.ParaEmail))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Email do Amigo é obrigatório" };
            }
            if (!entidade.DeEmail.IsValidMail())
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Seu Email inválido" };
            }
            if (!entidade.ParaEmail.IsValidMail())
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Email do Amigo inválido" };
            }


            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "Compartilhe.cshtml");
            string email = RazorUtil.Render(path, EmailModel.GetModel(entidade));
            EmailUtil.Send(entidade.DeNome, entidade.DeEmail, entidade.ParaNome, entidade.ParaEmail, "Bradesco | Tocha Olímpica | Compartilharam com você", email);

            return new RetornoModel() { Sucesso = true, Mensagem = "Ok!" };

        }
    }
}

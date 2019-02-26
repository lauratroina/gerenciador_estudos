using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Identity;
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Util; 
namespace BradescoNext.Lib.Service 
{ 
    public class HistoriaModeracaoService
    {
        private IHistoriaModeracaoDAL dal = new HistoriaModeracaoADO();

        public int Incluir(HistoriaModeracao entidade)
        {
            return dal.Inserir(entidade);
        }

        public void Salvar(HistoriaModeracao entidade)
        {
            dal.Atualizar(entidade);
        }

        public int BuscaHistoriaEmAnalise(int historiaID, int usuarioID)
        {
            return dal.BuscaHistoriaEmAnalise(historiaID, usuarioID);
        }

        public bool HistoriaModerada(int historiaID, int usuarioID, int qtdeModeracaoConfiguracao)
        {
            return dal.QtdeModeracaoRealizada(historiaID, usuarioID) >= qtdeModeracaoConfiguracao;
        }

        public bool HistoriaModerada(int historiaID, int qtdeModeracaoConfiguracao)
        {
            return dal.QtdeModeracaoRealizada(historiaID) >= qtdeModeracaoConfiguracao;
        }

        public void EnviarEmailsAdicionadoNaGaleria(IndicadoModel entidade)
        {
            string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "AvisoAdicionadoGaleriaParaIndicador.cshtml");
            string email = RazorUtil.Render(path, EmailModel.GetModel(entidade));
            EmailUtil.Send(entidade.Historia.IndicadorEmail, "Bradesco | Tocha Olímpica | Adicionado a galeria", email);

            path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "AvisoAdicionadoGaleriaParaIndicado.cshtml");
            email = RazorUtil.Render(path, EmailModel.GetModel(entidade.Indicado));
            EmailUtil.Send(entidade.Indicado.Email, "Bradesco | Tocha Olímpica | Adicionado a galeria", email);
        }
        public bool ModeracaoRealizada(int historiaID, int usuarioID)
        {
            return dal.ModeracaoRealizada(historiaID, usuarioID);
        }

    } 
} 

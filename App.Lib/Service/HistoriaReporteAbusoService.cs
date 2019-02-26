using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Models;

namespace BradescoNext.Lib.Service
{
    public class HistoriaReporteAbusoService
    {
        private IHistoriaReporteAbusoDAL dal = new HistoriaReporteAbusoADO();
        public void Salvar(HistoriaReporteAbuso reporte)
        {
            if (reporte.ID > 0)
            {
                dal.Atualizar(reporte);
            }
            else
            {
                dal.Inserir(reporte);
            }
        }
        public void InativarReportes(IList<HistoriaReporteAbuso> reportes)
        {
            foreach (var item in reportes)
            {
                item.Inativo = true;
                dal.InativarReporte(item);
            }
        }

        public RetornoModel ReportarAbuso(HistoriaReporteAbuso model, int quantidadeAbuso)
        {
            if (model.HistoriaID == 0)
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "História deve ser informado" };
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Email deve ser informado" };
            }
            if (string.IsNullOrEmpty(model.Nome))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Nome deve ser informado" };
            }
            if (string.IsNullOrEmpty(model.Mensagem))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Mensagem deve ser informada" };
            }
            if (!model.Email.IsValidMail())
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Email inválido" };
            }
            if (dal.Carregar(model.HistoriaID, model.Email) != null)
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Você só pode reportar Abuso uma única vez por História" };
            }
            model.DataCadastro = DateTime.Now;
            Salvar(model);
            HistoriaADO hAdo = new HistoriaADO();
            hAdo.InserirLog(model.HistoriaID);
            hAdo.AdicionarAbuso(model.HistoriaID);
            Historia historia = hAdo.Carregar(model.HistoriaID);
            IndicadoADO iAdo = new IndicadoADO();
            Indicado indicado = iAdo.Carregar(historia.IndicadoID??0);
            if(!indicado.RemoverGaleria)
            {
                if(quantidadeAbuso <= iAdo.QtdeReportsAbuso(indicado.ID))
                {
                    iAdo.InserirLog(indicado.ID);
                    iAdo.RemoverGaleriaAbuso(indicado.ID);
                }
            }
            
            return new RetornoModel() { Sucesso = true, Mensagem = "OK!" };

        }
    }
}

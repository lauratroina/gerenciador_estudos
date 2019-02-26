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
    public class IndicadorService
    {
        private IIndicadorDAL dal = new IndicadorADO();

        public IList<Indicador> ListarIndicadoresContinuacao(DateTime dataAtual, int horasEmailContinuacao, int quantidadeEmailContinuacao)
        {
            return dal.ListarPendentesRoboContinuacao(dataAtual, horasEmailContinuacao, quantidadeEmailContinuacao);
        }

        public void AtualizarAguardandoContinuacao(Indicador indicador)
        {
            dal.AtualizarContinuacao(indicador);
        }

        public RetornoModel SalvarIndicacao(Indicador model)
        {
            Indicador indicador = dal.Carregar(model.Email);
            if (indicador == null)
            {
                model.DataCadastro = DateTime.Now;
                model.DataModificacao = model.DataCadastro;
                model.DataEnvioEmailContinuacao = DateTime.Now;
                dal.Inserir(model);
            }
            else
            {
                if ((indicador.Nome != model.Nome) || (indicador.Email != model.Email) || (indicador.FacebookID != model.FacebookID))
                {
                    indicador.Nome = model.Nome;
                    indicador.Email = model.Email;
                    if (!string.IsNullOrEmpty(model.FacebookID))
                    {
                        indicador.FacebookID = model.FacebookID;
                        indicador.FacebookToken = model.FacebookToken;
                    }
                    indicador.DataModificacao = DateTime.Now;
                    dal.Atualizar(indicador);
                }
            }
            return new RetornoModel() { Sucesso = true, Mensagem = "OK" };
        }
    }
}

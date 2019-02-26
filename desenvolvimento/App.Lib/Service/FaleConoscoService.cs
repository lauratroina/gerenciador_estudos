using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Util;

namespace BradescoNext.Lib.Service
{
    public class FaleConoscoService
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IFaleConoscoDAL dal = new FaleConoscoADO();
        public void Salvar(FaleConosco faleConosco)
        {
            faleConosco.DataCadastro = DateTime.Now;
            dal.Inserir(faleConosco);
        }

        public RetornoModel Enviar(FaleConosco model)
        {
            if (string.IsNullOrEmpty(model.Nome))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Nome é obrigatório" };
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Email é obrigatório" };
            }
            if (string.IsNullOrEmpty(model.Mensagem))
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Mensagem  é obrigatório" };
            }
            if (model.FaleConoscoAssuntoID == 0)
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Assunto  é obrigatório" };
            }
            if (!model.Email.IsValidMail())
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Email inválido" };
            }
            
            Salvar(model);
            FaleConoscoAssuntoService assuntoService = new FaleConoscoAssuntoService();
            model.FaleConoscoAssunto = assuntoService.Carregar(model.FaleConoscoAssuntoID);

            if (model.FaleConoscoAssunto.Destinos.Count > 0)
            {

                string path = ConfiguracaoAppUtil.GetPath(enumConfiguracaoLib.emailTemplateDiretorio, "FaleConosco.cshtml");
                string email = RazorUtil.Render(path, EmailModel.GetModel(model));

                foreach (FaleConoscoDestino destino in model.FaleConoscoAssunto.Destinos)
                {
                    EmailUtil.Send(model.Nome, model.Email, destino.Nome, destino.Email, "Bradesco | Tocha Olímpica | Fale Conosco | " + model.FaleConoscoAssunto.Nome, email);
                }
            
            }
            return new RetornoModel() { Sucesso = true, Mensagem = "Ok!" };
            
        }

        public IList<FaleConosco> Listar(Int32 skip, Int32 take, string palavraChave)
        {
            IList<FaleConosco> lista = dal.Listar(skip, take, palavraChave);
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }

        public FaleConosco Carregar(int id)
        {
            return dal.Carregar(id);
        }
    }
}

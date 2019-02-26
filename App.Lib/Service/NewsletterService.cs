
using System;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.DAL.ADO;
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Models;
using System.Linq;
using BradescoNext.Lib.Enumerator;
using BradescoNext.Lib.Util;

namespace BradescoNext.Lib.Service 
{ 
    public class NewsletterService 
    { 

		private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private INewsletterDAL dal = new NewsletterADO();

        public RetornoModel Inscrever(Newsletter newsletter)
        {
            if (!newsletter.Email.IsValidMail())
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Email inválido" };
            }
            newsletter.Inativo = false;
            Salvar(newsletter);
            return new RetornoModel() { Sucesso = true, Mensagem = "OK!" };
        }

        public RetornoModel Desiscrever(Newsletter newsletter)
        {
            if (!newsletter.Email.IsValidMail())
            {
                return new RetornoModel() { Sucesso = false, Mensagem = "Email inválido" };
            }
            if (newsletter.Origem == enumNewsletterOrigem.todas)
            {
                newsletter.Origem = enumNewsletterOrigem.site;
                newsletter.Inativo = true;
                Salvar(newsletter);

                newsletter.Origem = enumNewsletterOrigem.participantes;
                newsletter.Inativo = true;
                Salvar(newsletter);

                newsletter.Origem = enumNewsletterOrigem.rota;
            }
            newsletter.Inativo = true;
            Salvar(newsletter);
            
            return new RetornoModel() { Sucesso = true, Mensagem = "OK!" };
        }


        public void Salvar(Newsletter newsletter, bool simples = false)
        {
            if (simples)
            {
                if(newsletter.ID > 0)
                {
                    dal.Atualizar(newsletter);
                }
                else
                {
                    dal.Inserir(newsletter);
                }
            }
            else
            {
                // caso já tenha email cadastra ordem
                Newsletter newsletterExistente = dal.Carregar(newsletter.Email, newsletter.Origem);

                if (newsletterExistente != null)
                {
                    if (newsletterExistente.Inativo != newsletter.Inativo)
                    {
                        // se o email ja existe, com essa origem, ele vira ativo
                        newsletterExistente.Inativo = newsletter.Inativo;
                        dal.Atualizar(newsletterExistente);
                    }
                }
                else
                {
                    newsletter.DataCadastro = DateTime.Now;
                    dal.Inserir(newsletter);
                }
            }
            
        }

        public Newsletter Carregar(int id) 
        { 
            return dal.Carregar(id);  
        }

        public IList<Newsletter> Listar(bool somenteAtivos = true)
		{
			return dal.Listar(somenteAtivos); 
		}

        public IList<Newsletter> Listar(Int32 skip, Int32 take,string palavraChave, string origem, bool somenteAtivos = true) 
        { 
			IList<Newsletter> lista = dal.Listar(skip, take, palavraChave,somenteAtivos,origem); 
			TotalRegistros = dal.TotalRegistros;
            return lista;
        }
       
    } 
} 

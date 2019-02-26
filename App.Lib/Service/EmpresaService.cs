
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
namespace BradescoNext.Lib.Service 
{ 
    public class EmpresaService 
    {

        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IEmpresaDAL dal = new EmpresaADO();

        public void Excluir(int id)
        {
            dal.Excluir(id);
        }
        public Empresa Carregar(int id)
        {
            return dal.Carregar(id);
        }

        public void Salvar(Empresa empresa)
        {
            if (empresa.ID > 0)
            {
                dal.Atualizar(empresa);
            }
            else
            {
                dal.Inserir(empresa);
            }
        }

        public IList<Empresa> Listar()
        {
            return dal.Listar();
        }

        public IList<Empresa> Listar(Int32 paginaAtual, Int32 totalRegPorPagina, string palavraChave, bool somenteAtivos = true)
        {
            IList<Empresa> lista = dal.Listar(paginaAtual, totalRegPorPagina, palavraChave, somenteAtivos);
            TotalRegistros = dal.TotalRegistros;
            return lista;
        }
    } 
} 

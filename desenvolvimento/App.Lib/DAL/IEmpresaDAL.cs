using System; 
using System.Collections.Generic; 
using System.Text;
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface IEmpresaDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(Empresa entidade); 
         void Atualizar(Empresa entidade); 
         void Excluir(int id); 
         Empresa Carregar(int id);
         IList<Empresa> Listar();
         IList<Empresa> Listar(Int32 skip, Int32 take, string palavraChave, bool somenteAtivos = true);
    } 
} 

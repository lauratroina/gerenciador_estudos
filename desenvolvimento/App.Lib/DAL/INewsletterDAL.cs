using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface INewsletterDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(Newsletter entidade); 
         void Atualizar(Newsletter entidade);
         Newsletter Carregar(int id);
         Newsletter Carregar(string email, enumNewsletterOrigem origem);
		 IList<Newsletter> Listar(bool somenteAtivos = true);
         IList<Newsletter> Listar(Int32 skip, Int32 take, string palavraChave, bool somenteAtivos, string origem);
    } 
} 

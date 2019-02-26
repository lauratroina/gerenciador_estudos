using System; 
using System.Collections.Generic; 
using System.Text; 
using App.Lib.Entity; 
namespace App.Lib.DAL 
{ 
    public interface IUsuarioDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(Usuario entidade); 
         void Atualizar(Usuario entidade);
         Usuario Carregar(int id);
         Usuario Carregar(string login); 
		 IList<Usuario> Listar();
         IList<Usuario> Listar(Int32 skip, Int32 take, string palavraChave, bool somenteAtivos = true);
    } 
} 

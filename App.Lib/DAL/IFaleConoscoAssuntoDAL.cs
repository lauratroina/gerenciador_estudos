using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface IFaleConoscoAssuntoDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(FaleConoscoAssunto entidade); 
         void Atualizar(FaleConoscoAssunto entidade); 
         FaleConoscoAssunto Carregar(int id);
         FaleConoscoAssunto CarregarComDestinos(int id);
		 IList<FaleConoscoAssunto> Listar(bool somenteAtivos = true);
         IList<FaleConoscoAssunto> Listar(Int32 skip, Int32 take, bool somenteAtivos = true);
         
    } 
} 

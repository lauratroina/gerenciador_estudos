using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface IFaleConoscoDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(FaleConosco entidade); 
         FaleConosco Carregar(int id); 
		 IList<FaleConosco> Listar();
         IList<FaleConosco> Listar(Int32 skip, Int32 take, string palavraChave);
    } 
} 

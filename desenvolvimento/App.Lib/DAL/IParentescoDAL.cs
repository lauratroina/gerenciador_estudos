using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface IParentescoDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(Parentesco entidade); 
         void Atualizar(Parentesco entidade); 
		 IList<Parentesco> Listar();
         IList<Parentesco> Listar(bool somenteAtivos);
         IList<Parentesco> Listar(Int32 skip, Int32 take);
    } 
} 

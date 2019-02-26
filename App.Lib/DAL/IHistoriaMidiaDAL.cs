using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface IHistoriaMidiaDAL 
    { 
	     void Inserir(HistoriaMidia entidade); 
         void Atualizar(HistoriaMidia entidade);
         HistoriaMidia Carregar(int id); 
		 IList<HistoriaMidia> Listar(int historiaID, bool somenteAtivos = true);
    } 
} 

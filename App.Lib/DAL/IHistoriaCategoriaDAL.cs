using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface IHistoriaCategoriaDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(HistoriaCategoria entidade); 
         void Atualizar(HistoriaCategoria entidade);
         HistoriaCategoria Carregar(int id); 
		 IList<HistoriaCategoria> Listar();
         IList<HistoriaCategoria> Listar(Int32 skip, Int32 take, string palavraChave);
         IList<HistoriaCategoria> Listar(bool somenteAtivos);
         IList<HistoriaCategoria> Listar(int indicadoID);
    } 
} 

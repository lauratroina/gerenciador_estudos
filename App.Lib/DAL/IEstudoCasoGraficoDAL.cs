using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{
    public interface IEstudoCasoGraficoDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         void Inserir(EstudoCasoGrafico entidade); 
         void Atualizar(EstudoCasoGrafico entidade);
         EstudoCasoGrafico Carregar(int id); 
		 IList<EstudoCasoGrafico> Listar();
         IList<EstudoCasoGrafico> Listar(Int32 skip, Int32 take, string palavraChave);

         EstudoCasoGrafico CarregarParaEstudo(int estudoCasoID);
    } 
} 

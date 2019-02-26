using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity; 
namespace BradescoNext.Lib.DAL 
{ 
    public interface IHistoriaCategoriaHistoriaDAL 
    {
        void Inserir(int historiaID, int historiaCategoriaID);
        void Inserir(int historiaID, string listaHistoriaCategoriaID);
        void Excluir(int historiaID, int historiaCategoriaID);
        void Excluir(int historiaID);
        IList<HistoriaCategoriaHistoria> Listar(int historiaID);
    } 
} 

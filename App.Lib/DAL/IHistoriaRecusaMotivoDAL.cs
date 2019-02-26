using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity;
namespace BradescoNext.Lib.DAL 
{ 
    public interface IHistoriaRecusaMotivoDAL 
    {
        void Incluir(int HistoriaID, string ListaRecusaMotivoID);
        IList<HistoriaRecusaMotivo> Listar(int historiaID);
    } 
} 

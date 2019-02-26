using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.Entity;
using BradescoNext.Lib.Entity.Enumerator; 
namespace BradescoNext.Lib.DAL 
{
    public interface IDocumentoTipoDAL
    {
        Int32 TotalRegistros { get; set; }
        IList<DocumentoTipo> Listar();
    } 
} 

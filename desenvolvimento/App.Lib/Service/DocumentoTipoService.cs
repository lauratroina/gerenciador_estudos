using System; 
using System.Collections.Generic; 
using System.Text; 
using BradescoNext.Lib.DAL; 
using BradescoNext.Lib.DAL.ADO; 
using BradescoNext.Lib.Entity; 

namespace BradescoNext.Lib.Service 
{ 
    public class DocumentoTipoService 
    { 

		private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private IDocumentoTipoDAL dal = new DocumentoTipoADO();

        public IList<DocumentoTipo> Listar()
        {
            IList<DocumentoTipo> lista = dal.Listar();
            return lista;
        }
    } 
} 

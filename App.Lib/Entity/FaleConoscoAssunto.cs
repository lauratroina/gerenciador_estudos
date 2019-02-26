using System; 
using System.Text; 
using System.Collections.Generic; 
namespace BradescoNext.Lib.Entity  
{  
     [Serializable] 
     public class FaleConoscoAssunto
     { 
        private int _ID; 
        private string _Nome;
        private int _Ordem;
        private bool _Inativo;
        private IList<FaleConoscoDestino> _Destinos;

		
        public int ID
        { 
            set { _ID = value; } 
            get { return _ID; } 
        }
         
        public string Nome
        { 
            set { _Nome = value; } 
            get { return _Nome; } 
        }
        public int Ordem
        {
            set { _Ordem = value; }
            get { return _Ordem; }
        }

        public bool Inativo
        {
            set { _Inativo = value; }
            get { return _Inativo; }
        }

        public IList<FaleConoscoDestino> Destinos
        {
            set { _Destinos = value; }
            get { return _Destinos; }
        }
     } 
} 
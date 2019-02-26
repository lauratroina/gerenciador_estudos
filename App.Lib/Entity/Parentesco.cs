using System; 
using System.Text; 
using System.Collections.Generic; 
namespace BradescoNext.Lib.Entity  
{  
     [Serializable] 
     public class Parentesco
     { 
        private int _ID; 
        private string _Nome;
        private bool _Inativo;
        private int _Ordem;

		
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

        public bool Inativo
        {
            set { _Inativo = value; }
            get { return _Inativo; }
        }


        public int Ordem
        {
            set { _Ordem = value; }
            get { return _Ordem; }
        } 

         
     } 
} 
using System; 
using System.Collections.Generic; 
using System.Text; 
using App.Lib.Entity;
using App.Lib.Entity.Enumerator; 
namespace App.Lib.DAL 
{ 
    public interface IUsuarioPerfilDAL 
    { 
	     Int32 TotalRegistros { get; set; }
         
		 IList<UsuarioPerfil> Listar();
    } 
} 

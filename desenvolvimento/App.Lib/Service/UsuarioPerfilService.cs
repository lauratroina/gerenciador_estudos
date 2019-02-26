  
using System; 
using System.Collections.Generic; 
using System.Text; 
using App.Lib.DAL; 
using App.Lib.DAL.ADO; 
using App.Lib.Entity; 
namespace App.Lib.Service 
{ 
    public class UsuarioPerfilService 
    { 

		private IUsuarioPerfilDAL dal = new UsuarioPerfilADO(); 

        public IList<UsuarioPerfil> Listar()
		{
			return dal.Listar(); 
		}
       
    } 
} 

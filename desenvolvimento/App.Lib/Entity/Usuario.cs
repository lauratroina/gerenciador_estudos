using System;
using System.Text;
using System.Collections.Generic;
using App.Lib.Entity.Enumerator;
namespace App.Lib.Entity
{
    [Serializable]
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        private DateTime _UltimoAcesso ;
        public int UsuarioPerfilID { get; set; }
        public bool Inativo { get; set; }
        public int UsuarioID { get; set; }
        public DateTime DataCadastro { get; set; }
        public UsuarioPerfil Perfil { get; set; }

        public bool Ativo
        {
            get { return !Inativo; }
            set { Inativo = !value; }
        }
        
        public DateTime UltimoAcesso
        {
            set { _UltimoAcesso = value; }
            get { return (_UltimoAcesso == DateTime.MinValue) ? new DateTime(1899, 1, 1) : _UltimoAcesso; }
        }
    }
}
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
        public bool Inativo { get; set; }
        public UsuarioPerfil Perfil
        {
            get
            {
                return new UsuarioPerfil
                {
                    ID = 1,
                    Nome = Entity.Enumerator.enumPerfilNome.master,
                    Descricao = "Master"
                };
            }
        }
        public bool Ativo
        {
            get { return !Inativo; }
            set { Inativo = !value; }
        }

        public
    }
}
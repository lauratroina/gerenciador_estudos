using System;
using System.Text;
using System.Collections.Generic;
using App.Lib.Entity.Enumerator;
namespace App.Lib.Entity
{
    [Serializable]
    public class UsuarioPerfil
    {
        public int ID { get; set; }
        public string NomeDB { get; set; }
        public string Descricao { get; set; }

        public enumPerfilNome Nome
        {
            set { NomeDB = value.ToString(); }
            get { try { return EnumExtensions.FromString<enumPerfilNome>(NomeDB); } catch { return enumPerfilNome.todos; } }
        }
    }
}
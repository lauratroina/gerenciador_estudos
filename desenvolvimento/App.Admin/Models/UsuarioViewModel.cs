using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Models
{
    public class UsuarioViewModel
    {

        public int id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Usuário")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe o Login do Usuário")]
        public string login { get; set; }

        [Required(ErrorMessage = "Informe o E-mail de Acesso")]
        public string senha { get; set; }
        public bool inativo { get; set; }
        public string senharepeat { get; set; }
        public string senhaatual { get; set; }
        
    }

    public class UsuarioSenhaViewModel
    {

        public int id { get; set; }

        [Required(ErrorMessage = "Informe a Senha do Usuário")]
        public string senha { get; set; }

        [Required(ErrorMessage = "Repita a Senha do Usuário")]
        public string senharepeat { get; set; }

        public string senhaatual { get; set; }
    }

}
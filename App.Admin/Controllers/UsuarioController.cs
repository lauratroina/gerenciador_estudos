using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Admin.Models;
using App.Admin.Enumerator;
using App.Lib.Service;
using App.Lib.Util;
using App.Lib.Entity;
using App.Lib.Entity.Enumerator;
using App.Lib.Models;

namespace App.Admin.Controllers
{
    [OnlyHttps]
    [AppAdminAuthorize(enumPerfilNome.master)]
    public class UsuarioController : MasterController
    {

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar(jQueryDataTableParamModel param)
        {
            IList<Usuario> result = new List<Usuario>();
            int totalReg = 0;
            try
            {
                //Monta palavra chave
                string palavraChave = (param.sSearch != null) ? param.sSearch.ToString() : string.Empty;

                //Cria Instancia da BO de Medicamento
                UsuarioService usuarioService = new UsuarioService();

                //Corrige calculo paginação
                int skip = param.iDisplayStart;
                int take = param.iDisplayLength;


                //Consulta dados no banco
                result = usuarioService.Listar(skip, take, palavraChave);

                //Retorna total de registros para monstar a paginação
                totalReg = usuarioService.TotalRegistros;
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
            }
            //Formata saida dos dados para funcionar no DataGrid obrigatório colocar todos parametros que vão aparecer lá
            List<string[]> listaDados = new List<string[]>();
            foreach (var item in result)
            {
                string ultimoAcesso = "";
                if(item.UltimoAcesso > new DateTime(1900,1,1))
                {
                    ultimoAcesso = string.Format("{0:dd/MM/yyyy}", item.UltimoAcesso);
                }
                listaDados.Add(new string[] { item.Perfil.Descricao, item.Login, item.Nome, ultimoAcesso, item.ID.ToString() + "-" + item.Inativo.ToString(), item.ID.ToString() });
            }

            //Monta Json de Retorno
            JsonResult retorno = Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalReg,
                iTotalDisplayRecords = totalReg,
                aaData = listaDados
            },
            JsonRequestBehavior.AllowGet);

            return retorno;


        }


        public ActionResult Editar(Int32 id = 0)
        {

            UsuarioModel retorno = new UsuarioModel();
            retorno.Usuario = new Usuario();
            retorno.Perfils = CarregaDropPerfil();

            if (id != 0)
            {
                try
                {
                    UsuarioService service = new UsuarioService();
                    retorno.Usuario = service.Carregar(id);
                    retorno.SenhaNova = retorno.Usuario.Senha;
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex);
                    return RedirectToAction("Index", "Usuario", new { msg = "Ocorreu um erro ao carregar os dados", msgtipo = enumTipoMensagem.erro });
                }
            }

            return View(retorno);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Salvar(UsuarioModel model)
        {
            if (!string.IsNullOrEmpty(model.SenhaNova))
                model.Usuario.Senha = UsuarioService.CriptografarSenha(model.SenhaNova);

            model.Usuario.UsuarioID = SessionModel.Usuario.ID;

            model.Usuario.UsuarioPerfilID = 1;
            try
            {
                UsuarioService service = new UsuarioService();
                service.Salvar(model.Usuario);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RedirectToAction("Index", "Usuario", new { msg = "Ocorreu um erro ao salvar os dados", msgtipo = enumTipoMensagem.erro });
            }

            return RedirectToAction("Index", "Usuario", new { msg = "Dados Salvos com Sucesso", msgtipo = enumTipoMensagem.sucesso });

        }

        
        public ActionResult Inativar(int id, bool inativo)
        {
            
            try
            {
                UsuarioService service = new UsuarioService();
                var usuario = service.Carregar(id);
                usuario.Inativo = inativo;
                usuario.UsuarioID = SessionModel.Usuario.ID;

                service.Salvar(usuario);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return Json(new RetornoModel() { Sucesso = false, Mensagem = "Ocorreu um erro ao tentar inativar o usuário" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new RetornoModel() { Sucesso = true, Mensagem = "OK" }, JsonRequestBehavior.AllowGet);

        }

        
        private IList<UsuarioPerfil> CarregaDropPerfil()
        {
            IList<UsuarioPerfil> list = new List<UsuarioPerfil>();
            try
            {
                UsuarioPerfilService empServ = new UsuarioPerfilService();
                list = empServ.Listar();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
            }
            return list;
        }
    }
}
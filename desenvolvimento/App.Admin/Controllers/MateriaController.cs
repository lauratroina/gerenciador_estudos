using App.Admin.Enumerator;
using App.Admin.Models;
using App.Lib.Entity;
using App.Lib.Entity.Enumerator;
using App.Lib.Service;
using App.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class MateriaController : MasterController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar(jQueryDataTableParamModel param)
        {
            IList<Materia> result = new List<Materia>();
            int totalReg = 0;
            try
            {
                //Monta palavra chave
                string palavraChave = (param.sSearch != null) ? param.sSearch.ToString() : string.Empty;

                MateriaService service = new MateriaService();

                int skip = param.iDisplayStart;
                int take = param.iDisplayLength;


                result = service.Listar(skip, take, palavraChave);

                totalReg = service.TotalRegistros;
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
            }

            List<string[]> listaDados = new List<string[]>();
            foreach (var item in result)
            {
                listaDados.Add(new string[] { item.Nome, string.IsNullOrEmpty(item.CorBorda)?"":item.CorBorda, string.IsNullOrEmpty(item.CorFundo)?"":item.CorFundo, item.ID.ToString() });
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

        [AppAdminAuthorize(enumPerfilNome.master)]
        public ActionResult Editar(Int32 id = 0)
        {
            Materia materia = new Materia();
            if (id != 0)
            {
                try
                {
                    materia = new MateriaService().Carregar(id);
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex);
                    return RedirectToAction("Index", "Materia", new { msg = "Ocorreu um erro ao carregar os dados", msgtipo = enumTipoMensagem.erro });
                }
            }

            return View(materia);
        }

        [HttpPost]
        [ValidateInput(false)]
        [AppAdminAuthorize(enumPerfilNome.master)]

        public ActionResult Salvar(Materia model)
        {
            try
            {
                MateriaService service = new MateriaService();
                service.Salvar(model);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RedirectToAction("Index", "Materia", new { msg = "Ocorreu um erro ao salvar os dados", msgtipo = enumTipoMensagem.erro });
            }

            return RedirectToAction("Index", "Materia", new { msg = "Dados Salvos com Sucesso", msgtipo = enumTipoMensagem.sucesso });

        }

        [AppAdminAuthorize(enumPerfilNome.master)]
        public ActionResult Deletar(int id)
        {
            try
            {
                MateriaService service = new MateriaService();
                service.Deletar(id);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RedirectToAction("Index", "Materia", new { msg = "Ocorreu um erro ao salvar os dados", msgtipo = enumTipoMensagem.erro });
            }

            return RedirectToAction("Index", "Materia", new { msg = "Dados Salvos com Sucesso", msgtipo = enumTipoMensagem.sucesso });

        }
        
    }
}
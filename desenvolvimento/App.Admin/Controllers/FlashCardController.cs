using App.Admin.Enumerator;
using App.Admin.Models;
using App.Lib.Entity;
using App.Lib.Service;
using App.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class FlashCardController : MasterController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar(jQueryDataTableParamModel param)
        {
            IList<Carta> result = new List<Carta>();
            int totalReg = 0;
            try
            {
                //Monta palavra chave
                string palavraChave = (param.sSearch != null) ? param.sSearch.ToString() : string.Empty;

                CartaService service = new CartaService();

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
                listaDados.Add(new string[] {
                    item.ID.ToString(),
                    item.TextoFrente.LimpaHtml().Truncate(50),
                    item.Materia.Nome,
                    string.Format("{0}-{1}", item.Status.ToString(),item.ID.ToString()),
                    string.Format("{0}-{1}", item.Favorita.ToString(), item.ID.ToString()),
                    item.ID.ToString() });
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


        public ActionResult Editar(Int32 id = 0, int materia=0)
        {
            Carta entidade = new Carta();
            entidade.Status = true;

            try
            {
                if (id != 0)
                {
                    entidade = new CartaService().Carregar(id);
                }
                entidade.Materias = new MateriaService().Listar();
                if (materia != 0)
                    entidade.MateriaID = materia;
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RedirectToAction("Index", "FlashCard", new { msg = "Ocorreu um erro ao carregar os dados", msgtipo = enumTipoMensagem.erro });
            }

            return View(entidade);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Salvar(Carta model)
        {
            try
            {
                CartaService service = new CartaService();
                service.Salvar(model);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RedirectToAction("Index", "FlashCard", new { msg = "Ocorreu um erro ao salvar os dados", msgtipo = enumTipoMensagem.erro });
            }

            if(model.InserirProxima)
                return RedirectToAction("Editar", "FlashCard", new { materia=model.MateriaID, msg = "Dados Salvos com Sucesso", msgtipo = enumTipoMensagem.sucesso });

            return RedirectToAction("Index", "FlashCard", new { msg = "Dados Salvos com Sucesso", msgtipo = enumTipoMensagem.sucesso });

        }


        public ActionResult Deletar(int id)
        {
            try
            {
                CartaService service = new CartaService();
                service.Deletar(id);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return RedirectToAction("Index", "FlashCard", new { msg = "Ocorreu um erro ao salvar os dados", msgtipo = enumTipoMensagem.erro });
            }

            return RedirectToAction("Index", "FlashCard", new { msg = "Dados Salvos com Sucesso", msgtipo = enumTipoMensagem.sucesso });

        }

        public ActionResult Favoritar(int id, bool favorito)
        {
            try
            {
                new CartaService().Favoritar(id, favorito);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "Ocorreu um erro ao favoritar essa carta", msgtipo = enumTipoMensagem.erro });
            }
            return Json(new { msg = "Carta favoritada com sucesso", msgtipo = enumTipoMensagem.sucesso });

        }

        public ActionResult MudarStatus(int id, bool status)
        {
            try
            {
                new CartaService().MudarStatus(id, status);

            }
            catch (Exception ex)
            {
                return Json(new { msg = "Erro ao atualizar o status", msgtipo = enumTipoMensagem.erro });
            }
            return Json(new { msg = "Status atualizado com sucesso", msgtipo = enumTipoMensagem.sucesso });

        }

        public ActionResult Sortear()
        {
            SorteioViewModel model = new SorteioViewModel();
            model.Materias = new MateriaService().Listar();
            return View(model);
        }

        [HttpPost]
        public ActionResult ListarParaEstudo(SorteioViewModel model)
        {
            IList<Carta> retorno;
            var rnd = new Random();
            try
            {
                retorno = new CartaService().Listar(model.MateriasIDs, model.Favoritas);
            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, sucesso = false, JsonRequestBehavior.AllowGet });
            }
            return Json(new { data = retorno.OrderBy(item => rnd.Next()), sucesso = true, JsonRequestBehavior.AllowGet });
        }
    }
}
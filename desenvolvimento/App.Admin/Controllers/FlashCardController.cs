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

        [AppAdminAuthorize(enumPerfilNome.master)]
        public ActionResult Editar(Int32 id = 0, int materia = 0)
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
        [AppAdminAuthorize(enumPerfilNome.master)]
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

            if (model.InserirProxima)
                return RedirectToAction("Editar", "FlashCard", new { materia = model.MateriaID, msg = "Dados Salvos com Sucesso", msgtipo = enumTipoMensagem.sucesso });

            return RedirectToAction("Index", "FlashCard", new { msg = "Dados Salvos com Sucesso", msgtipo = enumTipoMensagem.sucesso });

        }


        [AppAdminAuthorize(enumPerfilNome.master)]
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

        [AppAdminAuthorize(enumPerfilNome.master)]

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

        [AppAdminAuthorize(enumPerfilNome.master)]
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

        public ActionResult Filtrar()
        {
            SorteioViewModel model = new SorteioViewModel();
            try
            {
                model.Materias = new MateriaService().Listar();
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Sortear(SorteioViewModel model)
        {
            Guid Identificador = new Guid();
            try
            {
                CartaService service = new CartaService();
                Identificador = service.GerarSorteio(model.MateriasIDs, model.Favoritas);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Detalhe", "FlashCard", new { id = Identificador });
        }

        public ActionResult Detalhe(Guid id, bool r = false, int idc = 0)
        {
            CartaService service = new CartaService();
            Sorteio retorno = new Sorteio();
            try
            {
                if (!r && idc > 0)
                {
                    service.ApagaSorteio(idc);
                }
                retorno = service.Carregar(id);
                if (retorno == null)
                {
                    retorno = service.Carregar(id);
                    if (retorno == null)
                    {
                        return RedirectToAction("Index", "FlashCard", new { msg = "Não há mais cartas", msgtipo = enumTipoMensagem.sucesso });
                    }
                }
            }
            catch (Exception ex)
            { }
            return View(retorno);
        }

        public ActionResult Ver(int id)
        {
            Carta model = new Carta();
            try
            {
                CartaService service = new CartaService();
                model = service.Carregar(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Flashcards", new { msg = "Ocorreu um erro ao carregar essa carta.", msgtipo = enumTipoMensagem.erro });
            }
            return View(model);
        }
    }
}
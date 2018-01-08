using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQH.Business.Contrato;
using SQH.Entities.Models;
using SQH.Entities.Requisitores;
using SQH.Shared.Extensions;
using SQH.Web.Controllers.Base;
using SQH.Web.Filter;
using System;
using System.Collections.Generic;

namespace SQH.Web.Controllers
{
    [Route("dashboard")]
    [Authorize]
    public class DashboardController : CustomMvcController
    {
        private readonly IRequisicao client;
        public DashboardController(IRequisicao _client)
        {
            client = _client;
        }

        [Route("apontamento", Name = "dashget")]
        public IActionResult Index()
        {
            DashboardModel proj = new DashboardModel();
            return View(proj);
        }

        [HttpPost, Route("apontamento", Name = "dashpost"), CookieContainerGetter]
        [ValidateAntiForgeryToken]
        public IActionResult Index(DashboardModel model)
        {
            if (ModelState.IsValid)
            {
                string[] datas = model.periodo.Split('-');
                DateTime periodoInicial = DateTime.Parse(datas[0].Trim());
                DateTime periodoFinal = DateTime.Parse(datas[1].Trim());

                ApontamentoRequisitor requisitor = new ApontamentoRequisitor();
                requisitor.PRJ_DESCRICAO = model.textoProjeto;
                requisitor.PRJ_ID = model.idProjeto;
                requisitor.SUB_DESCRICAO = model.textoSubProjeto;
                requisitor.VG_ID = model.idPapel;
                requisitor.TPITEM_DESCRICAO = model.textoPapel;
                requisitor.TSOL_ID = model.idMotivo;
                requisitor.TSOL_DESC = model.textoMotivo;

                IEnumerable<DateTime> listaDatasApontamento = periodoInicial.ListaDatasExcetoFDS(periodoFinal);
                model.resposta = client.BuscarRespostaApontamento(requisitor, listaDatasApontamento, PacoteDeBiscoitos);
            }
            return View(model);
        }

        [HttpGet, Route("projeto"), CookieContainerGetter]
        public JsonResult BuscaProjeto(string data)
        {
            var requisitor = new ProjetoRequisitor() { dataRequisicao = DateTime.Parse(data).ToString("dd/MM/yyyy") };
            DashboardModel projeto = client.CarregaProjetoMotivos(requisitor, PacoteDeBiscoitos).CopiarPara(new DashboardModel());

            return Json(projeto);
        }

        [HttpGet, Route("subprojeto")]
        public JsonResult BuscaSubProjetos(string projeto, string idColaborador, string dataProjeto)
        {
            SubProjetoRequisitor requisitor = new SubProjetoRequisitor();
            requisitor.colaborador = idColaborador;
            requisitor.data = dataProjeto;
            requisitor.projeto = projeto;

            var response = client.CarregaSubProjeto(requisitor, PacoteDeBiscoitos);
            return Json(response);
        }

        [HttpGet, Route("papeis")]
        public JsonResult BuscaPapeis(string projeto, string dataProjeto, string idColaborador, string subprojeto)
        {
            PapeisRequisitor requisitor = new PapeisRequisitor(projeto, dataProjeto, idColaborador, subprojeto);

            var response = client.CarregarPapeis(requisitor, PacoteDeBiscoitos);
            return Json(response);
        }
    }
}
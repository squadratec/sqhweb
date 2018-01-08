using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SQH.Business.Contrato;
using SQH.Entities.Requisitores;
using SQH.Entities.Response;
using SQH.Web.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SQH.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IRequisicao client;
        public HomeController(IRequisicao _client)
        {
            client = _client;
        }

        [HttpGet]
        [Route("~/")]
        [Route("login", Name = "loginget")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("login", Name = "loginpost")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Entities.Models.Login model)
        {
            if (ModelState.IsValid)
            {
                AuthRetorno retorno = client.ValidaLoginUsuario(new LoginRequisitor(model.usuario, model.senha));

                if (retorno.authenticated)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                                  new ClaimsPrincipal(retorno.Claims),
                                                  new AuthenticationProperties
                                                  {
                                                      IsPersistent = true,
                                                      ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
                                                  });

                    return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("usuario", retorno.message);
                }
            }
            return View(model);
        }

        [HttpGet, Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index));
        }

        [HttpGet, Route("erro")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

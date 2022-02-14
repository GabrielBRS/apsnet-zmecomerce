using ecommerce_aspnetmvc_entityframework.Libraries.Filtro;
using ecommerce_aspnetmvc_entityframework.Libraries.Login;
using ecommerce_aspnetmvc_entityframework.Models;
using ecommerce_aspnetmvc_entityframework.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_aspnetmvc_entityframework.Areas.Colaboradores.Controllers
{
    [Area("Colaborador")]
    //TODO:Habilitar validação de Login
    //[ColaboradorAutorizacao]
    public class HomeController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        private Libraries.Login.LoginColaborador _loginColaborador;

        public HomeController(IColaboradorRepository colaboradorRepository, LoginColaborador loginColaborador)
        {
            _colaboradorRepository = colaboradorRepository;
            _loginColaborador = loginColaborador;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([FromForm]Models.Colaborador colaborador)
        {
            Models.Colaborador colaboradorDB = _colaboradorRepository.Login(colaborador.Email, colaborador.Senha);

            if (colaboradorDB != null)
            {
                _loginColaborador.Login(colaboradorDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado";
                return View();
            }
        }

        [ColaboradorAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {

            _loginColaborador.Logout();
            return RedirectToAction("Login", "Home");
        }

        [ColaboradorAutorizacao]
        public IActionResult Painel()
        {
            return View();
        }

        public IActionResult RecuperarSenha()
        {
            return View();
        }

        public IActionResult CadastrarNovaSenha()
        {
            return View();
        }
    }
}

using ecommerce_aspnetmvc_entityframework.Database;
using ecommerce_aspnetmvc_entityframework.Libraries.Email;
using ecommerce_aspnetmvc_entityframework.Libraries.Filtro;
using ecommerce_aspnetmvc_entityframework.Libraries.Login;
using ecommerce_aspnetmvc_entityframework.Models;
using ecommerce_aspnetmvc_entityframework.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace ecommerce_aspnetmvc_entityframework.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _clienteRepository;
        private INewsletterRepository _newsletterRepository;
        //private LoginColaborador _loginColaborador;
        private LoginCliente _loginCliente;
        private GerenciarEmail _gerenciarEmail;


        public HomeController (IClienteRepository clienteRepository, INewsletterRepository newsletterRepository, LoginCliente loginCliente, GerenciarEmail gerenciarEmail)  
        {
            _clienteRepository = clienteRepository;
            _newsletterRepository = newsletterRepository;
            _loginCliente = loginCliente;
            _gerenciarEmail = gerenciarEmail;
            


        }
        
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter)
        {
            if (ModelState.IsValid)
            {
                _newsletterRepository.Cadastrar(newsletter);

                /*
                _banco.NewsletterEmail.Add(newsletter);
                _banco.SaveChanges();

                TempData["MSG_S"] = "Ficamos felizes com a sua decisão, seu email foi cadastrado com sucesso!";
                */

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Noticias()
        {
            return View();
        }

        public IActionResult Enciclopedia()
        {
            return View();
        }

        public IActionResult Sabbath()
        {
            return View();
        }

        public IActionResult CursosOnline()
        {
            return View();
        }

        public IActionResult LojaVirtual()
        {
            return View();
        }

        public IActionResult Assinaturas()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                if (isValid)
                {
                    _gerenciarEmail.EnviarContatoPorEmail(contato);

                    ViewData["MSG_5"] = "Mensagem de contato enviado com sucesso!";
                }

                else
                {
                    StringBuilder sb = new StringBuilder();
                   foreach(var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br />");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["Contato"] = contato;
                }
                
            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Opps! Tives um erro, tente novamente mais tarde!";

            }
             
            return View("Contato");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _clienteRepository.Login(cliente.Email, cliente.Senha);

            if(clienteDB != null)
            {
                _loginCliente.Login(clienteDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado";
                return View();
            }
        }

        [HttpGet]
        [ClienteAutorizacaoAttribute]
        public IActionResult Painel()
        {
            return new ContentResult() { Content = "Este é o painel" };
        }

        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.Cadastrar(cliente);

                TempData["MSG_S"] = "Cadastro realizado com sucesso!";

                return RedirectToAction(nameof(CadastroCliente));
            }
            
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

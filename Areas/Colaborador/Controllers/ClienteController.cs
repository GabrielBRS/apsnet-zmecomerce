using ecommerce_aspnetmvc_entityframework.Libraries.Filtro;
using ecommerce_aspnetmvc_entityframework.Libraries.Lang;
using ecommerce_aspnetmvc_entityframework.Models;
using ecommerce_aspnetmvc_entityframework.Models.Constante;
using ecommerce_aspnetmvc_entityframework.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ecommerce_aspnetmvc_entityframework.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public IActionResult Index(int? pagina, string pesquisa)
        {
            IPagedList<Cliente> clientes = _clienteRepository.ObterTodosClientes(pagina,pesquisa);
            return View(clientes);
        }

        [ValidateHttpReferer]
        public IActionResult AtivarDesativar(int id)
        {
            Cliente cliente = _clienteRepository.ObterCliente(id);
            
            if(cliente.Situacao == SituacaoConstante.Ativo)
            {
                cliente.Situacao = SituacaoConstante.Desativado;
            }
            else
            {
                cliente.Situacao = SituacaoConstante.Ativo;
            }
            _clienteRepository.Atualizar(cliente);
            TempData["MSG_S"] = Mensagem.MSG_S001;
            return RedirectToAction(nameof(Index));
        }
    }
}

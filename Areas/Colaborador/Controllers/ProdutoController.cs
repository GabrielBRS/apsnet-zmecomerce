using ecommerce_aspnetmvc_entityframework.Libraries.Arquivo;
using ecommerce_aspnetmvc_entityframework.Libraries.Lang;
using ecommerce_aspnetmvc_entityframework.Models;
using ecommerce_aspnetmvc_entityframework.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_aspnetmvc_entityframework.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ProdutoController : Controller
    {
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;
        private IImagemRepository _imagemRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IImagemRepository imagemRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _imagemRepository = imagemRepository;
        }

        public IActionResult Index(int? pagina, string pesquisa)
        {
            var produtos = _produtoRepository.ObterTodosProdutos(pagina, pesquisa);
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a=> new SelectListItem(a.Nome,a.Id.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Product produto)
        {
            if (ModelState.IsValid)
            {
                //TODO - Salvar o Produto
                _produtoRepository.Cadastrar(produto);

                List<Imagem> ListaImagensDef = GerenciadorArquivo.MoverImagensProduto(new List<string>(Request.Form["imagem"]), produto.Id);

                _imagemRepository.CadastrarImagens(ListaImagensDef, produto.Id);

                //TODO - CaminhoTemp -> Mover a Imagem para o Caminho Definitivo

                //TODO - Salvar o Caminho Definitivo no banco de dados.

                TempData["MSG_5"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                produto.Imagens = new List<string>(Request.Form["imagem"]).Where(a=>a.Trim().Length > 0).Select(a => new Imagem { Caminho = a }).ToList();
 
                return View(produto);
            } 
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            Product product = _produtoRepository.ObterProdutos(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Atualizar(Product produto, int id)
        {
            if (ModelState.IsValid)
            {
                //TODO - Salvar o Produto
                _produtoRepository.Atualizar(produto);

                //TODO - Não mover imagens na pasta definitiva
                List<Imagem> ListaImagensDef = GerenciadorArquivo.MoverImagensProduto(new List<string>(Request.Form["imagem"]), produto.Id);
                _imagemRepository.ExcluirImagensDoProduto(produto.Id);
                _imagemRepository.CadastrarImagens(ListaImagensDef, produto.Id);

                //TODO - CaminhoTemp -> Mover a Imagem para o Caminho Definitivo

                //TODO - Salvar o Caminho Definitivo no banco de dados.

                TempData["MSG_5"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                produto.Imagens = new List<string>(Request.Form["imagem"]).Where(a => a.Trim().Length > 0).Select(a => new Imagem { Caminho = a }).ToList();

                return View(produto);
            }
        }
    }
}

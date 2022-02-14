using ecommerce_aspnetmvc_entityframework.Database;
using ecommerce_aspnetmvc_entityframework.Models;
using ecommerce_aspnetmvc_entityframework.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ecommerce_aspnetmvc_entityframework.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private IConfiguration _conf;
        private LojaVirtualContext _banco;

        public ProdutoRepository(LojaVirtualContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }

        public void Atualizar(Product produto)
        {
            _banco.Update(produto);
            _banco.SaveChanges();
        }

        public void Cadastrar(Product produto)
        {
            _banco.Add(produto);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Product produto = ObterProdutos(Id);
            _banco.Remove(produto);
            _banco.SaveChanges();
        }

        public Product ObterProdutos(int Id)
        {
            return _banco.Produtos.Include(a=>a.Imagens).Where(a=>a.Id == Id).FirstOrDefault();
        }

        public IPagedList<Product> ObterTodosProdutos(int? paginas, string pesquisa)
        {
            int registrosPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = paginas ?? 1;
            var bancoProduto = _banco.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não está vazio
                bancoProduto = bancoProduto.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }
            return bancoProduto.Include(a=>a.Imagens).ToPagedList<Product>(numeroPagina, registrosPorPagina);
        }
    }
}

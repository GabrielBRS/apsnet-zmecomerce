using ecommerce_aspnetmvc_entityframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ecommerce_aspnetmvc_entityframework.Repositories.Contracts
{
    public interface IProdutoRepository
    {
        //CRUD
        void Cadastrar(Product produto);
        void Atualizar(Product produto);
        void Excluir(int Id);

        Product ObterProdutos(int Id);
        IPagedList<Product> ObterTodosProdutos(int? paginas, string pesquisa);
    }
}

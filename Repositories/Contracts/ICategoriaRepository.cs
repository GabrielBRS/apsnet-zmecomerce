using ecommerce_aspnetmvc_entityframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ecommerce_aspnetmvc_entityframework.Repositories.Contracts
{
    public interface ICategoriaRepository
    {
        void Cadastrar(Categoria categoria);

        Categoria ObterCategoria(int Id);

        IEnumerable<Categoria> ObterTodasCategorias();

        IPagedList<Categoria> ObterTodasCategorias(int? pagina);

        void Atualizar(Categoria categoria);

        void Excluir(int Id);
    }
}

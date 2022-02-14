using ecommerce_aspnetmvc_entityframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ecommerce_aspnetmvc_entityframework.Repositories.Contracts
{
    public interface IClienteRepository
    {
        Cliente Login(string senha, string login);

        void Cadastrar(Cliente cliente);

        Cliente ObterCliente(int Id);

        IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa);

        void Atualizar(Cliente cliente);

        void Excluir(int Id);
    }
}

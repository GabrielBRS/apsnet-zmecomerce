using ecommerce_aspnetmvc_entityframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ecommerce_aspnetmvc_entityframework.Repositories.Contracts
{
     public interface IColaboradorRepository
    {
        Colaborador Login(string Email, string Senha);

        void Cadastrar(Colaborador colaborador);
        void Atualizar(Colaborador colaborador);
        void AtualizarSenha(Colaborador colaborador);
        void Excluir(int Id);

        Colaborador ObterColaboradores(int Id);
        //List<Colaborador> ObterTodosColaboradores();
        List<Colaborador> ObterTodosColaboradoresPorEmail(string email);
        IPagedList<Colaborador> ObterTodosColaboradores(int? paginas);
    }
}

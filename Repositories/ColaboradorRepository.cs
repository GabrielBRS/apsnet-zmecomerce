using ecommerce_aspnetmvc_entityframework.Database;
using ecommerce_aspnetmvc_entityframework.Models;
using ecommerce_aspnetmvc_entityframework.Models.Constante;
using ecommerce_aspnetmvc_entityframework.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ecommerce_aspnetmvc_entityframework.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        const int _registroPorPagina = 10;
        private IConfiguration _config;
        private LojaVirtualContext _banco;

        public ColaboradorRepository(LojaVirtualContext banco, IConfiguration config)
        {
            _banco = banco;
            _config = config;
        }

        public void Atualizar(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            _banco.Entry(colaborador).Property(a => a.Senha).IsModified = false;
            _banco.SaveChanges();
        }

        public void AtualizarSenha(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            _banco.Entry(colaborador).Property(a => a.Nome).IsModified = false;
            _banco.Entry(colaborador).Property(a => a.Senha).IsModified = false;
            _banco.Entry(colaborador).Property(a => a.Tipo).IsModified = false;
            _banco.SaveChanges();
        }

        public void Cadastrar(Colaborador colaborador)
        {
            _banco.Add(colaborador);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Colaborador colaborador = ObterColaboradores(Id);
            _banco.Remove(colaborador);
            _banco.SaveChanges();
        }

        public Colaborador Login(string Email, string Senha)
        {
            Colaborador colaborador = _banco.Colaboradores.Where(m => m.Email == Email && m.Senha == Senha).FirstOrDefault();
            return colaborador;
        }

        public Colaborador ObterColaboradores(int Id)
        {
            return _banco.Colaboradores.Find(Id);
        }

        
        public List<Colaborador> ObterTodosColaboradores()
        {
            return _banco.Colaboradores.ToList();
        }
        

        public IPagedList<Colaborador> ObterTodosColaboradores(int? paginas)
        {
            int registrosPorPagina = _config.GetValue<int>("RegistroPorPagina");
            int numeroPagina = paginas ?? 1;
            return _banco.Colaboradores.Where(a => a.Tipo != ColaboradorTipoConstante.Gerente).ToPagedList<Colaborador>(numeroPagina, registrosPorPagina);
        }

        public List<Colaborador> ObterTodosColaboradoresPorEmail(string email)
        {
            return _banco.Colaboradores.Where(a => a.Email == email).AsNoTracking().ToList();
        }
    }
}

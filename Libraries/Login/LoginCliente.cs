using ecommerce_aspnetmvc_entityframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ecommerce_aspnetmvc_entityframework.Libraries.Login
{
    public class LoginCliente
    {
        private string key = "Cliente.Login";
        private Sessao.Sessao _sessao;

        public LoginCliente(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente cliente)
        {
            string clienteJSONString = JsonConvert.SerializeObject(cliente);
            _sessao.Cadastrar(key, clienteJSONString);
        }

        public Cliente GetCliente()
        {
            if (_sessao.Existe(key))
            {
                string clienteJSONString = _sessao.Consultar(key);
                return JsonConvert.DeserializeObject<Cliente>(clienteJSONString); ;
            }
            else
            {
                return null;
            }         
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
 }

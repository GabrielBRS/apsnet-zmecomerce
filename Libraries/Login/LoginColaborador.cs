using ecommerce_aspnetmvc_entityframework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_aspnetmvc_entityframework.Libraries.Login
{
    public class LoginColaborador
    {
        private string key = "Colaborador.Login";
        private Sessao.Sessao _sessao;

        public LoginColaborador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Colaborador colaborador)
        {
            string ColaboradorJSONString = JsonConvert.SerializeObject(colaborador);
            _sessao.Cadastrar(key, ColaboradorJSONString);
        }

        public Colaborador GetColaborador()
        {
            if (_sessao.Existe(key))
            {
                string ColaboradorJSONString = _sessao.Consultar(key);
                return JsonConvert.DeserializeObject<Colaborador>(ColaboradorJSONString); ;
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

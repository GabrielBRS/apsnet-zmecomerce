using ecommerce_aspnetmvc_entityframework.Libraries.Login;
using ecommerce_aspnetmvc_entityframework.Models;
using ecommerce_aspnetmvc_entityframework.Models.Constante;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_aspnetmvc_entityframework.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private string _tipoColaboradorAutorizado;
        public ColaboradorAutorizacaoAttribute(string tipoColaboradorAutorizado = ColaboradorTipoConstante.Comum)
        {
            _tipoColaboradorAutorizado = tipoColaboradorAutorizado;
        }

        /*Tipos do filtros:
    * 
    * Autorização
    * Recurso
    * Ação
    * Exceção
    * Resultado
    * 
    */
        private Login.LoginColaborador _loginColaborador;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));

            Models.Colaborador colaborador = _loginColaborador.GetColaborador();

            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if(colaborador.Tipo == ColaboradorTipoConstante.Comum && _tipoColaboradorAutorizado == ColaboradorTipoConstante.Gerente)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}

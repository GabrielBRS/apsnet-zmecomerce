using ecommerce_aspnetmvc_entityframework.Libraries.Login;
using ecommerce_aspnetmvc_entityframework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_aspnetmvc_entityframework.Libraries.Filtro
{
    /*Tipos do filtros:
     * 
     * Autorização
     * Recurso
     * Ação
     * Exceção
     * Resultado
     * 
     */
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter //, IResourceFilter, IActionFilter, IExceptionFilter, IResultFilter
    {
        LoginColaborador _loginColaborador;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));

            Colaborador colaborador = _loginColaborador.GetColaborador();

            if (colaborador == null)
            {
                context.Result = new ContentResult() { Content = "Acesso Negado" };
            }
        }
    }

}

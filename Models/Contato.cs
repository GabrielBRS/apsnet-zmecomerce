using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_aspnetmvc_entityframework.Libraries.Lang;

namespace ecommerce_aspnetmvc_entityframework.Models
{
    public class Contato
    {
        [Required(ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public string Nome { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(10, ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(1000, ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string Texto { get; set; }

    }
}

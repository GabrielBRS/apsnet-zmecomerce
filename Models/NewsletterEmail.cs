using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_aspnetmvc_entityframework.Libraries.Lang;

namespace ecommerce_aspnetmvc_entityframework.Models
{
    public class NewsletterEmail 
    {

        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(ecommerce_aspnetmvc_entityframework.Libraries.Lang.Mensagem), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }
    }
}

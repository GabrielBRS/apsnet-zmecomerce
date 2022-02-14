using ecommerce_aspnetmvc_entityframework.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_aspnetmvc_entityframework.Models
{
    public class Categoria
    {
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]

        //TODO: Criar validação nome Categoria único no banco de dados

        public string Nome { get; set; }

        /*
         * Nome: Telefone sem fio
         * Slug: Telefone-sem-fio
         * Url normal: www.lojavirtual.com.br/categoria/5
         * Url amigável e com slug: www.lojavirtual.com.br/categoria/informatica (URL AMIGÁVEL)
         */

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public string Slug { get; set; }

        /*
         * Auto relacionamento
         * Informática
         * -Mouse
         *  -- Mouse sem fio
         *  -- Mouse com fio
         */

        [Display(Name = "Categoria Pai")]
        public int? CategoriaPaiId { get; set; }

        /*
         * ORM- Entity Framework Core
         */

        [ForeignKey("CategoriaPaiId")]
        public virtual Categoria CategoriaPai { get; set; }
    }
}

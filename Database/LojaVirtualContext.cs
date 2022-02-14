using ecommerce_aspnetmvc_entityframework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_aspnetmvc_entityframework.Migrations;
using ecommerce_aspnetmvc_entityframework.Database;
using ecommerce_aspnetmvc_entityframework.Libraries;


namespace ecommerce_aspnetmvc_entityframework.Database
{
    public class LojaVirtualContext : DbContext
    {
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options)
        {

        }       
        public DbSet<Models.Cliente> Clientes { get; set; }
        public DbSet<Models.NewsletterEmail> NewsletterEmail { get; set; }
        public DbSet<Models.Colaborador> Colaboradores { get; set; }
        public DbSet<Models.Categoria> Categorias { get; set; }

        public DbSet<Models.Product> Produtos { get; set; }
        public DbSet<Models.Imagem> Imagens { get; set; }
    }   

}
            
    
    
    
        
    
    

using ecommerce_aspnetmvc_entityframework.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ecommerce_aspnetmvc_entityframework.Repositories;
using ecommerce_aspnetmvc_entityframework.Repositories.Contracts;
using ecommerce_aspnetmvc_entityframework.Libraries.Sessao;
using ecommerce_aspnetmvc_entityframework.Libraries.Login;
using ecommerce_aspnetmvc_entityframework.Models;
using System.Net.Mail;
using System.Net;
using ecommerce_aspnetmvc_entityframework.Libraries.Email;
using ecommerce_aspnetmvc_entityframework.Libraries.Middleware;

namespace ecommerce_aspnetmvc_entityframework
{
    //TODO: por criptografia nas informações... cartão de crédito, fazer o site em method(post)

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Sessão configuração

            services.AddMemoryCache();
            services.AddSession(options =>
            {
               
            });

            services.Configure<Configuracoes>(Configuration.GetSection("ConfiguracoesGerais"));


            //padrão repositories
            services.AddHttpContextAccessor();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IImagemRepository, ImagemRepository>();
            /*
             * SMTLP
             */
            services.AddScoped<SmtpClient>(options => {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };
                return smtp;
            });
            services.AddScoped<GerenciarEmail>();

            services.AddControllersWithViews();

            services.AddScoped<Sessao>();
            services.AddScoped<LoginCliente>();
            services.AddScoped<LoginColaborador>();

            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Banco;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            services.AddDbContext<LojaVirtualContext> (options => options.UseSqlServer(connection)) ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseSession();

            app.UseMiddleware<ValidationAntiForgeryTokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // endpoints.MapControllerRoute(
                //   name: "Visualizar",
                // pattern: "{controller=Product}/{action=Visualizar}/{id?}"); 
            });
            #region
            #endregion
        }
    }
}

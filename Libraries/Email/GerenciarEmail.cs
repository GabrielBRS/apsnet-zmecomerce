using ecommerce_aspnetmvc_entityframework.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ecommerce_aspnetmvc_entityframework.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;
        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }
        public void EnviarContatoPorEmail(Contato contato)
        {
            string corpoMsg = string.Format("<h2> Contato - Site Atalaia" +
                "<b>Nome: </b> {0} <br />" +
                "<b>Email: </b> {1} <br />" +
                "<b>Texto: </b> {2} <br />" +
                "<br /> Email enviado automaticamente do site Atalaia",
                contato.Nome,
                contato.Email,
                contato.Texto
                );

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add("gabriel.brs.gsousa@gmail.com");
            mensagem.Subject = "Site Atalaia";
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //TODO: REMOVER COMENTÁRIO PARA MANDAR A MENSAGEM
            //_smtp.Send(mensagem);
       
        }

        public void EnviarSenhaParaColaboradorPorEmail(Colaborador colaborador)
        {
            string corpoMsg = string.Format("<h2> Colaborador - Site Atalaia </h2>" +
                "Sua senha é" +
                "<h3> {0} </3>" , colaborador.Senha);

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador - Site Atalaia - Senha do colaborador - " + colaborador.Nome;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;
        }
    }
}

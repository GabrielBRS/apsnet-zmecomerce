using ecommerce_aspnetmvc_entityframework.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_aspnetmvc_entityframework.Libraries.Arquivo
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemProduto(IFormFile file)
        {
            //TODO - Armazenar imagem em uma pasta
            var NomeArquivo = Path.GetFileName(file.FileName);
            var Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);

            using (var stream = new FileStream(Caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("/uploads/temp", NomeArquivo).Replace("\\", "/");
        }

        public static bool ExcluirImagemProduto(string caminho)
        {
            //Todo - Excluir imagem na pasta
            // C://LojaVirtual/wwwroot//uploads/temp/arquivo.jpg
            string Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));
            if (File.Exists(Caminho))
            {
                File.Delete(Caminho);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Imagem> MoverImagensProduto(List<string> ListaCaminhoTemp, int ProdutoId)
        {
            //Criar a Pasta do Produto
            var CaminhoDefinitivoPastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProdutoId.ToString());
            if (!Directory.Exists(CaminhoDefinitivoPastaProduto))
            {
                Directory.CreateDirectory(CaminhoDefinitivoPastaProduto);
            }
            //Mover a Imagem da Pasta Temp para a pasta definitiva
            List<Imagem> ListaImagensDef = new List<Imagem>();
            foreach (var CaminhoTemp in ListaCaminhoTemp)
            {
                if (!string.IsNullOrEmpty(CaminhoTemp))
                {
                    //  /uploads/temp/mouse;cosair.jpg
                    var NomeArquivo = Path.GetFileName(CaminhoTemp);

                    // /uploads/1003/mouse-cosair.jpg
                    var CaminhoDef = Path.Combine("/uploads", ProdutoId.ToString(), NomeArquivo).Replace("\\", "/");

                    if (CaminhoDef != CaminhoTemp)
                    {

                        var CaminhoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);
                        var CaminhoAbsolutoDef = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProdutoId.ToString(), NomeArquivo);

                        if (File.Exists(CaminhoTemp))
                        {
                            //Deleta arquivo no caminho de destino
                            if (File.Exists(CaminhoAbsolutoTemp))
                            {
                                File.Delete(CaminhoAbsolutoTemp);
                            }
                            //Copia arquivo da pasta Temporária para destino
                            File.Copy(CaminhoAbsolutoTemp, CaminhoAbsolutoDef);
                            //Deleta arquivo da pasta Temporária
                            if (File.Exists(CaminhoAbsolutoDef))
                            {
                                
                            }
                            ListaImagensDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", ProdutoId.ToString(), NomeArquivo).Replace("\\", "/"), ProdutoId = ProdutoId });
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        ListaImagensDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", ProdutoId.ToString(), NomeArquivo).Replace("\\", "/"), ProdutoId = ProdutoId });
                    }
                }

            }
            return ListaImagensDef;
        }
    }
}

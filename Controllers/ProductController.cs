using ecommerce_aspnetmvc_entityframework.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_aspnetmvc_entityframework.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Visualizar()
        {
            Product produto = GetProduct();
            return View(produto);
            //return new ContentResult() { Content = "<h3>Produto -> Visualizar<h3/>", ContentType = "text/html" };
        }
        private Models.Product GetProduct()
        {
            return new Product()
            {
                Id = 1,
                Nome = "Xbox One X",
                Descricao = "Jogue em 4K",
                Valor = 7000
            };
        }
    }
}

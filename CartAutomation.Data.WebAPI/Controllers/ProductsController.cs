using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Data.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartAutomation.Data.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(template: "getall")]
        public IActionResult GetList()
        {
            //return Ok(_productService.GetAll());
            Product product = new Product();
            product.CategoryId = 1;
            product.Name = "Samsung Glaxy Telefon";
            product.Price = 2000;
            _productService.Add(product);
            return Ok();
        }
    }
}
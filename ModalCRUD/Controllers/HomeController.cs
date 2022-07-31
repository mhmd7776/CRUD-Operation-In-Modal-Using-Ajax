using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ModalCRUD.Services;
using ModalCRUD.ViewModels;

namespace ModalCRUD.Controllers
{
    public class HomeController : Controller
    {
        #region Ctor

        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();

            return View(products);
        }

        [HttpGet("load-product-modal-body")]
        public IActionResult LoadProductModalBody(int productId)
        {
            var result = _productService.FillCreateOrEditProductViewModel(productId);

            return PartialView("_ProductModalParial", result);
        }

        [HttpPost("submit-product-modal")]
        public IActionResult SubmitProductModal(CreateOrEditProductViewModel productViewModel)
        {
            var result = _productService.CreateOrEditProduct(productViewModel);

            if (result)
            {
                return new JsonResult(new {status = "Success"});
            }

            return new JsonResult(new { status = "Error" });
        }

        [HttpGet("delete-product")]
        public IActionResult DeleteProduct(int productId)
        {
            var result = _productService.DeleteProduct(productId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ShopAPIusingZERO.Domain.ProductAggregate;
using ShopAPIusingZERO.Model;
using System.Net.Mail;
using System;
using Zero.SeedWorks;
using System.Xml;
using System.Xml.Linq;

namespace ShopAPIusingZERO.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            this._productRepository = productRepository;

        }

        [ProducesResponseType(typeof(ProductResponseModel), StatusCodes.Status200OK)]

        [HttpGet("/api/products")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productRepository.ListAllAsync();

            return Ok(products.Select(m => new ProductResponseModel
            {
                ProductId = m.ProductId,
                ProductName = m.ProductName,
                Quantity = m.Quantity.Value,
                Price = m.Price
            })) ;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ProductRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;
                var pName = ProductName.Create(model.ProductName);
                if (pName.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Quantity), "Not Valid");
                    isValid = false;
                }
                var price = ProductPrice.Create(model.Price);
                if (price.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Quantity), "Not Valid");
                    isValid = false;
                }
                var qty = ProductQuantity.Create(model.Quantity);
                if (qty.IsFailure) 
                {
                    ModelState.AddModelError(nameof(model.Quantity), "Not Valid");
                    isValid = false;
                }

                if (isValid)
                {
                    var product = new Product(pName.Value, qty.Value, price.Value);
                    await _productRepository.AddAsync(product);
                    await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    return Ok("Product added Successfully with id: " + product.ProductId);
                }
            }
            return ValidationProblem(ModelState);
        }

    }
}

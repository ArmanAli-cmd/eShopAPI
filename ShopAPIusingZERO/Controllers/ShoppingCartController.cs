using Microsoft.AspNetCore.Mvc;
using ShopAPIusingZERO.Domain.ProductAggregate;
using ShopAPIusingZERO.Domain.ShoppingCartAggregate;
using ShopAPIusingZERO.Domain.UserAggregate;
using ShopAPIusingZERO.Model;
using System.Threading;
using Zero.SeedWorks;

namespace ShopAPIusingZERO.Controllers
{
    [Route("api/cartItem")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IRepository<CartItem> _itemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;

        public ShoppingCartController(IRepository<CartItem> itemRepository, IRepository<Product> productRepository, IRepository<User> userRepository)
        {
            this._itemRepository = itemRepository;
            this._productRepository = productRepository;
            this._userRepository = userRepository;
        }

        [HttpGet("/api/cartItem")]
        public async Task<IActionResult> GetAllItem()
        {
            var items = await _itemRepository.ListAllAsync();

            return Ok(items.Select(m => new CartItemResponseModel
            {
                
                ProductId = m.ProductId,
                UserId = m.UserId,
                ProductName= m.ProductName,
                ProductQty= m.Qty,
                ProductPrice= m.Price,
            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsyncById(int id)
        {

            var item = await _itemRepository.GetByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(new CartItemResponseModel
            {
                ProductId = item.ProductId,
                UserId = item.UserId,
                ProductName = item.ProductName,
                ProductQty = item.Qty,
                ProductPrice = item.Price,

            });
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(CartItemRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid) 
            {
                bool isValid = true;

                var qty = ProductQuantity.Create(model.Qty);
                if (qty.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Qty), "Not Valid");
                    isValid = false;
                }
                if (isValid)
                {
                    //Get product by product Id
                    var product = await _productRepository.GetByIdAsync(model.ItemId);
                   
                    //Get user by userId
                    var user = await _userRepository.GetByIdAsync(model.UserId);
                   
                    //check either product or user are exist or not
                    if (product == null || user == null) return NotFound("Either User or item is not available");
                    
                    //final quantity in product table
                    var leftQuantity = ProductQuantity.Create(product.Quantity - model.Qty);
                    if (leftQuantity.Value < 0) return NotFound("Quantity should be less then " + product.Quantity);
                   
                    //total price of product
                    int totalprice = product.Price * model.Qty;

                    //add product to cart
                    var newItem = new CartItem(product.ProductId, user.UserId, product.ProductName, qty.Value, totalprice);
                    await _itemRepository.AddAsync(newItem);
                    await _itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    //Update product quantity
                    var updateProduct = product.Update(product.ProductName, leftQuantity.Value, product.Price);
                    await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    return Ok("Item added successfully in Shopping cart");
                }
            }
            return ValidationProblem(ModelState);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id, CancellationToken cancellationToken)
        {
            
                var cartItem = await _itemRepository.GetByIdAsync(id);
                if (cartItem == null) return NotFound("Item Not Avilable");
                _itemRepository.Delete(cartItem);
                await _itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                return Ok("Item deleted");

        }
    }
}

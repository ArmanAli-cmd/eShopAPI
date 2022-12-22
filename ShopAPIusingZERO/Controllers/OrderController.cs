using Microsoft.AspNetCore.Mvc;
using ShopAPIusingZERO.Domain.Order;
using ShopAPIusingZERO.Domain.OrderAggregate;
using ShopAPIusingZERO.Domain.ProductAggregate;
using ShopAPIusingZERO.Domain.ShoppingCartAggregate;
using ShopAPIusingZERO.Domain.UserAggregate;
using ShopAPIusingZERO.Model;
using ShopAPIusingZERO.Specification;
using Zero.SeedWorks;

namespace ShopAPIusingZERO.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CartItem> _itemRepository;
        private readonly IRepository<User> _userRepository;
        public OrderController(IRepository<Order> orderRepository, IRepository<CartItem> cartRepository, IRepository<User> userRepository)
        {
            this._orderRepository = orderRepository;
            this._itemRepository = cartRepository;
            _userRepository = userRepository;
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var order = await _orderRepository.ListAllAsync();
            return Ok(order);
        }

        [HttpGet("{OrderId}")]
        public async Task<IActionResult> GetOrderById(int OrderId)
        {
            var order = await _orderRepository.GetByIdAsync(OrderId);
            if(order == null) return NotFound("Order id doesn't exist");
            return Ok(order);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrderByUserId(int userId)
        {
            var orders = await _orderRepository.ListAllAsync();
            List<Order> userOrder = new();
            foreach(var order in orders)
            {
                if (order.UserId == userId)
                {
                    userOrder.Add(order);
                }
            }
            if(userOrder.Count == 0) return NotFound("Not found any order with user id"+ userId);
            return Ok(userOrder);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                //check userid is valid or not
                var user = await _userRepository.GetByIdAsync(model.UserID);
                if (user == null) return NotFound("User id is not valid");

                //Get all Items from cart by userId
                var cartItem = await _itemRepository.ListAsync(new CartItemsSpecification(model.UserID));
                //if cartItem is null
                if(cartItem.Count==0) return NotFound("Cart is Empty");

                List<OrderItem> items = new();
                foreach(var cart in cartItem)
                {
                    var abc = new OrderItem(cart.ProductId, cart.ProductName, cart.Qty, cart.Price);
                    items.Add(abc);
                }

                int Totalqty = 0, Totalprice=0;
                foreach (var product in items)
                {
                    Totalqty += product.Quantity;
                    Totalprice+= product.Price;
                }

                var orderItem = new Order(model.UserID, Totalqty, Totalprice, items);
                
                await _orderRepository.AddAsync(orderItem);
                await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                //after placed order, remove items from cart
                foreach(var cart in cartItem)
                {
                    _itemRepository.Delete(cart);
                    await _itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                }
                return Ok("Order Placed Successfully with OrderId: "+ orderItem.OrderId);
            }
            return ValidationProblem(ModelState);
        }

        //Post Order By user id and product id
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{userId}, {productId}")]
        public async Task<IActionResult> PostOrderByProductId(int userId, int productId, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                //check userid is valid or not
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null) return NotFound("User id is not valid");

                var cartItem = await _itemRepository.ListAsync(new CartItemsSpecification(userId));
                if (cartItem == null) return NotFound("cart is empty");

                List<OrderItem> items = new();
                int Totalqty = 0, Totalprice = 0;
                foreach (var cart in cartItem)
                {
                    if(cart.ProductId == productId)
                    {
                        var abc = new OrderItem(cart.ProductId, cart.ProductName, cart.Qty, cart.Price);
                        items.Add(abc);
                        Totalqty += cart.Qty;
                        Totalprice += cart.Price;
                    }
                }
                if (items.Count == 0) return NotFound("Product doesn't exist in cart");
                
                var orderItem = new Order(userId, Totalqty, Totalprice, items);

                await _orderRepository.AddAsync(orderItem);
                await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                //after placed order, remove items from cart
                foreach (var cart in cartItem)
                {
                    if(cart.ProductId == productId)
                    {
                        _itemRepository.Delete(cart);
                    }
                    await _itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                }
                return Ok("Order Placed Successfully with orderId: "+ orderItem.OrderId);
            }
            return ValidationProblem(ModelState);
        }
    }
}

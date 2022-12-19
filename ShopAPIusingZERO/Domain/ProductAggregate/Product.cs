using System.Net.Mail;
using System.Xml.Linq;
using System;
using Zero.SeedWorks;
using ShopAPIusingZERO.Domain.ShoppingCartAggregate;
using System.ComponentModel.DataAnnotations;
using Zero.SharedKernel.Types.Result;

namespace ShopAPIusingZERO.Domain.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        
        public int ProductId { get; private set; }
        public ProductName ProductName { get; private set; }
        public ProductQuantity Quantity { get; private set; }
        public ProductPrice Price { get; private set; }
        public Product(ProductName productName, ProductQuantity quantity, ProductPrice price)
        {
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
        public Product() { }

        public Result Update(ProductName productName, ProductQuantity quantity, ProductPrice price)
        {
            Price= price;
            ProductName = productName;
            Quantity = quantity;
            return Result.Success();
        }
    }
    
}

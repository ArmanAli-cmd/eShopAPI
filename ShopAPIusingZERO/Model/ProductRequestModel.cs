using System.ComponentModel.DataAnnotations;

namespace ShopAPIusingZERO.Model
{
    public class ProductRequestModel
    {
        [Required(ErrorMessage="Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Product Price is required")]
        public int Price { get; set; }
    }
}

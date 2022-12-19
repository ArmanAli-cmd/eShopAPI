namespace ShopAPIusingZERO.Model
{
    public class CartItemResponseModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set;}
        public int ProductQty { get; set;}
        public int ProductPrice { get; set;}
    }
}

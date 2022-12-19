using System.ComponentModel.DataAnnotations;

namespace ShopAPIusingZERO.Model
{
    public class OrderRequestModel
    {
        [Required(ErrorMessage = "User Id is required")]
        public int UserID { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ShopAPIusingZERO.Model
{
    public class UserRequestModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string MobileNumber { get; set; }
    }
}

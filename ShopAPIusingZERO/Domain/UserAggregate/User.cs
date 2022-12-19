using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace ShopAPIusingZERO.Domain.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public int UserId { get; private set; }
        public UserName Name { get; private set; }
        public UserEmail Email { get; private set; }
        public UserMobile MobileNumber { get; private set; }
        
        public User() { }
        public User(UserName userName, UserEmail userEmail, UserMobile mobileNumber)
        {
            Name = userName;
            Email = userEmail;
            MobileNumber = mobileNumber;
        }
        //Update user
        public Result Update(UserName userName, UserEmail userEmail, UserMobile mobileNumber)
        {
            Name = userName;
            Email = userEmail;
            MobileNumber = mobileNumber;
            return Result.Success();
        }
    }
}

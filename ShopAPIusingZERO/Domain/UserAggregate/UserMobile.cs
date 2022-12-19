using System.Text.RegularExpressions;
using Zero.SeedWorks;
using Zero.SharedKernel.Constants;
using Zero.SharedKernel.Types.Result;

namespace ShopAPIusingZERO.Domain.UserAggregate
{
    public class UserMobile: ValueObject
    {
        public string Value { get; }

        //Empty Value Condition Incase Value Not Passed From User
        private static readonly UserMobile _empty = new(string.Empty);
        public static UserMobile Empty => _empty;

        private UserMobile(string value)
        {
            Value = value;
        }

        public static Result<UserMobile> Create(string? value, bool allowNull = false)
        {   //Check if value of mobilenumber Is Null, 
            if (allowNull && value == null)
                return Result.Success(_empty);  //Result is success if value is null or empty
            //Check if Mobilenumber contains any space or not match to our give regex pattern
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, RegexPatterns.MobileNumber))
                return Result.Failure<UserMobile>("Mobile number is Invalid"); //result failure if space present on mobile no or not match to our regex pattern 

            return Result.Success(new UserMobile(value));//if all above conditions are false value passed to the private constructor
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator string(UserMobile mobileNumber)
        {
            return mobileNumber.Value;
        }
        public static explicit operator UserMobile(string mobilenumber)
        {
            return Create(mobilenumber).Value;
        }
    }
}

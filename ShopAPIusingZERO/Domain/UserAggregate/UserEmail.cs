using System.Net.Mail;
using System.Text.RegularExpressions;
using Zero.SeedWorks;
using Zero.SharedKernel.Constants;
using Zero.SharedKernel.Types.Result;

namespace ShopAPIusingZERO.Domain.UserAggregate
{
    public class UserEmail : ValueObject
    {
        public string Value { get; }

        public static readonly UserEmail _empty = new(string.Empty);
        public static UserEmail Empty => _empty;

        private UserEmail(string value)
        {
            Value = value;
        }

        public static Result<UserEmail> Create(string? value, bool allowNull = false)
        {
            if (allowNull && value == null)
                return Result.Success(_empty);
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, RegexPatterns.EmailAddress))
                return Result.Failure<UserEmail>("Email Address Is Invalid");

            return Result.Success(new UserEmail(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator string(UserEmail emailAddress)
        {
            return emailAddress.Value;
        }
        public static explicit operator UserEmail(string emailAddress)
        {
            return Create(emailAddress).Value;
        }
    }
}

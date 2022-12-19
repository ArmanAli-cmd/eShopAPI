using Zero.SeedWorks;
using Zero.SharedKernel.Types.Result;

namespace ShopAPIusingZERO.Domain.UserAggregate
{
    public class UserName : ValueObject
    {
        public string Value { get; }
        public static readonly char[] _notAllowedCharacters = new char[] { '$', '^', '`', '<', '>', '+', '/', '=', '~' };

        private UserName(string value)
        {
            Value = value;
        }
        public static Result<UserName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<UserName>("Name Can't Be Blank.");
            if (value.Length > 100)
                return Result.Failure<UserName>("Name Is Too Long.");
            if (value.IndexOfAny(_notAllowedCharacters) != -1)
                return Result.Failure<UserName>("Some special characters are not allowed in the name.");

            return Result.Success(new UserName(value));
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator string(UserName name)
        {
            return name.Value;
        }
        public static explicit operator UserName(string name)
        {
            return Create(name).Value;
        }
    }
}

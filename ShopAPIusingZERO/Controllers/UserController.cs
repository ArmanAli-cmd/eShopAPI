using Microsoft.AspNetCore.Mvc;
using ShopAPIusingZERO.Domain.UserAggregate;
using ShopAPIusingZERO.Model;
using System.Threading;
using Zero.SeedWorks;

namespace ShopAPIusingZERO.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        public UserController(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userRepository.ListAllAsync();
            return Ok(user.Select(m => new UserResponseModel
            {
                UserId = m.UserId,
                UserName = m.Name.Value,
                UserEmail= m.Email.Value,
                MobileNumber= m.MobileNumber.Value,
            }));
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequestModel model, CancellationToken cancellationToken)
        {
            if(ModelState.IsValid)
            {
                bool isValid = true;

                var name = UserName.Create(model.UserName!);
                if (name.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.UserName), name.Error.Message);
                    isValid = false;
                }

                var email = UserEmail.Create(model.UserEmail!, true);
                if (email.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.UserEmail), email.Error.Message);
                    isValid = false;
                }

                var mobileNumber = UserMobile.Create(model.MobileNumber!, true);
                if (mobileNumber.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.UserEmail), mobileNumber.Error.Message);
                    isValid= false;
                }

                if (isValid)
                {
                    var user = new User(name.Value, email.Value, mobileNumber.Value);
                    await _userRepository.AddAsync(user);
                    await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    return Ok(user);
                }
            }
            return ValidationProblem(ModelState);
            
            
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null) return BadRequest("User Not Available");

                bool isValid = true;
                var name = UserName.Create(model.UserName!);
                if (name.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.UserName), name.Error.Message);
                    isValid = false;
                }

                var email = UserEmail.Create(model.UserEmail!, true);
                if (email.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.UserEmail), email.Error.Message);
                    isValid = false;
                }

                var mobileNumber = UserMobile.Create(model.MobileNumber!, true);
                if (mobileNumber.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.UserEmail), mobileNumber.Error.Message);
                    isValid = false;
                }
                if (isValid)
                {
                    var result = user.Update(name.Value, email.Value, mobileNumber.Value);
                    if (result.IsSuccess)
                    {
                        await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                        return Ok("Successfully updated");
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
        
    }
}

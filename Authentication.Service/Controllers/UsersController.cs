using Authentication.Application.UserModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/Users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserModel[]),200)]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAll();
            if(users != null)
                return Ok(users);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel userCreateRequest)
        {
            string error =  await _userService.Create(userCreateRequest);
            if (String.IsNullOrEmpty(error))
            {
                    return Ok();                
            }
            return BadRequest(error);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserModel userUpdateRequest)
        {
            string error = await _userService.ValidateUserData(userUpdateRequest);
            if (String.IsNullOrEmpty(error))
            {
                //UserModel user = _userService.UserMap(userUpdateRequest);
                //error = await _userService.Update(user);
                if (String.IsNullOrEmpty(error))
                {
                    return Ok();
                }
            }

            return BadRequest(error);
        }       

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            string error = await _userService.CheckIfIdExist(id);
            if (String.IsNullOrEmpty(error))
            {
                error = await _userService.Delete(id);
                if (String.IsNullOrEmpty(error))
                {
                    return Ok();
                }
           }
            return BadRequest(error);
        }
    }
}

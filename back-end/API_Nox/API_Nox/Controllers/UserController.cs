using API_Nox.Model;
using API_Nox.Repositories.Interfaces;
using API_Nox.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_Nox.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userRepository.GetAllUsersAsync());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _userRepository.GetUserByIdAsync(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User userModel)
        {
            return Ok(await _userRepository.AddUserAsync(userModel));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserViewModel userModel, Guid id)
        {
            return Ok(await _userRepository.UpdateUserAsync(userModel, id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _userRepository.DeleteUserAsync(id));
        }
    }
}

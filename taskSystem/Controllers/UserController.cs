using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskSystem.Models;
using taskSystem.Repositories.Interfaces;

namespace taskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositorie _userRepositorie;
        public UserController(IUserRepositorie userRepositorie) {
            _userRepositorie = userRepositorie;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> SearchAllUsers()
        {
            List<UserModel> users = await _userRepositorie.SearchAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> SearchById(int id)
        {
            UserModel users = await _userRepositorie.SearchById(id);
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Register([FromBody] UserModel userModel)
        {
            UserModel user = await _userRepositorie.Add(userModel);

            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel userModel, int id)
        {
            userModel.Id = id;
            UserModel user = await _userRepositorie.Update(userModel, id);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            bool deleted = await _userRepositorie.Delete(id);
            return Ok(deleted);
        }


    }
}

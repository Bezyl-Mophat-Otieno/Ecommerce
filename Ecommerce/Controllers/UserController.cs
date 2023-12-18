using AutoMapper;
using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Services.Iservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUser _userservice;
        private readonly IMapper _mapper;

        public UserController(IUser userservice, IMapper mapper)
        {
            _userservice = userservice;
            _mapper = mapper;

        }

        [HttpPost("register")]


        public async Task<ActionResult<string>> RegisterUser(RegisterUserDto newuser)
        {

            var user = _mapper.Map<User>(newuser);


            // check if user exists 

            var checkuser = await _userservice.GetByEmail(newuser.Email);

            if (checkuser != null)
            {
                return BadRequest("User Already Exists");
            }

            // Password hashing
            user.Password = BCrypt.Net.BCrypt.HashPassword(newuser.Password);

            var response = await _userservice.RegisterUser(user);
            return Ok(response);

        }
        
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _userservice.GetUsers();

            if (users.Count == 0) return NotFound();

            return Ok(users);
        }

        [HttpPut("{Id}")]

        public async Task<ActionResult<string>> UpdateUser(Guid Id, RegisterUserDto newuser)
        {

            var existinguser = await _userservice.GetById(Id);
            if (existinguser == null) return NotFound("User with Id , does not exist");
            if (existinguser == null) return NotFound("User with that Id ,does not exist");
            var mappeduser = _mapper.Map(newuser, existinguser);

            var response = await _userservice.UpdateUserInfo();

            return Ok(response);

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<string>> DeleteUser(Guid Id)
        {
            var user = await _userservice.GetById(Id);
            if (user == null) return NotFound();

            await _userservice.DeleteUser(user);

            return NoContent();


        }


    }
}

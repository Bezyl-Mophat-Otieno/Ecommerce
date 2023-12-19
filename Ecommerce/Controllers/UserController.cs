using AutoMapper;
using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Services;
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
        private readonly IJwt _jwtservice;

        public UserController(IUser userservice, IMapper mapper , IJwt jwtservice)
        {
            _userservice = userservice;
            _mapper = mapper;
            _jwtservice = jwtservice;

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

        [HttpPost("login")]

        public async Task<ActionResult<string>> LoginUser(LoginDTO user ) {

            // Check if the User exists 

            var userexist = await _userservice.GetByEmail(user.Email);

            if (userexist == null) return NotFound();

            // Verify password match 

            var isMatch = BCrypt.Net.BCrypt.Verify(user.Password, userexist.Password);

            if (!isMatch) return Unauthorized("Invalid Credentials");

            var token = _jwtservice.GetToken(userexist);

            return Ok(token);

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

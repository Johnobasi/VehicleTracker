using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Core.Entities;
using VehicleTracker.Core.Interfaces;
using VehicleTracker.Web.Dto;
using VehicleTracker.Web.Response;

namespace VehicleTracker.Web.Controllers
{
    [Route("api/v1/identity")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtSecurity _jwtSecurity;
        private readonly ILogger<UserController> _logger;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager,IMapper mapper,IJwtSecurity jwtSecurity,ILogger<UserController> logger , SignInManager<User> signInManager  )
        {
            _userManager = userManager;
            _mapper = mapper;
           _jwtSecurity = jwtSecurity;
            _logger = logger;
            _signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequestDto request)
        {
            UserResponseDto userResponseDto = null;
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user != null)
                    return BadRequest(new BaseResponse<UserResponseDto> { Body = null, Code = "400", IsSuccessful = false, Message = "userName already exist" });
                var userModel = _mapper.Map<UserRequestDto, User>(request);

                var result = await _userManager.CreateAsync(userModel, request.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse<UserResponseDto> { Body = null, Code = "500", IsSuccessful = false, Message = "An error occured during registration" });
                }
               
            
             
                    }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"{ex.Message} {ex.InnerException} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse<UserResponseDto> { Body = null, Code = "500", IsSuccessful = false, Message = "An error occured during registration" });
            }

            return Ok(new BaseResponse<UserResponseDto> { Message = "Registration successful", Body = userResponseDto, Code = "200", IsSuccessful = true });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequestDto request)
        {
            UserResponseDto userResponseDto = null;
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user is null)
                    return NotFound(new BaseResponse<UserResponseDto> { Body = null, Code = "404", IsSuccessful = false, Message = "user cannot be found" });
               

                var result = await _signInManager.PasswordSignInAsync(user, request.Password,true,false);
                if (!result.Succeeded)
                {
                    return BadRequest( new BaseResponse<UserResponseDto> { Body = null, Code = "400", IsSuccessful = false, Message = "invalid password or username" });
                }

                userResponseDto = _mapper.Map<User, UserResponseDto>(user);
                userResponseDto.Token = _jwtSecurity.CreateToken(user, string.Empty);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"{ex.Message} {ex.InnerException} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse<UserResponseDto> { Body = null, Code = "500", IsSuccessful = false, Message = "An error occured during login" });
            }

            return Ok(new BaseResponse<UserResponseDto> { Message = "login successful", Body = userResponseDto, Code = "200", IsSuccessful = true });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constans;
using Core.CrossCuttingConcers.Caching;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Extensions;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration { get; }
        private IAuthService _authService;
        private IUserService _userService;
        private ICacheManager _cacheManager;
        private TokenOptions _tokenOptions;

        public AuthController(IAuthService authService, IUserService userService, ICacheManager cacheManager, IConfiguration configuration)
        {
            _authService = authService;
            _userService = userService;
            _cacheManager = cacheManager;
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }


            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                var claims = _userService.GetClaims(userToLogin.Data);
                _cacheManager.Add($"{CacheKeys.UserIdForClaim}={userToLogin.Data.Id}", claims.Data.Select(x => x.Name), _tokenOptions.AccessTokenExpiration);

                var oprClaims = _cacheManager.Get<IEnumerable<string>>($"{CacheKeys.UserIdForClaim}={userToLogin.Data.Id}").ToList();

                var refreshToken = new UserRefreshTokenDto()
                {
                    UserId = userToLogin.Data.Id,
                    RefreshToken = result.Data.RefreshToken,
                    RefresTokenExpiration = result.Data.RefreshTokenEndDate
                };
                _userService.UpdateRefreshToken(refreshToken);
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("refreshTokenLogin")]
        public ActionResult RefreshTokenLogin(string refreshToken)
        {
            var user = _userService.GetByRefreshToken(refreshToken).Data;
            var result = _authService.CreateAccessToken(user);
            if (user.RefreshTokenEndDate > DateTime.Now)
            {
                if (result.Success)
                {
                    var claims = _userService.GetClaims(user);
                    _cacheManager.Add($"{CacheKeys.UserIdForClaim}={user.Id}", claims.Data.Select(x => x.Name), _tokenOptions.AccessTokenExpiration);

                   

                    var refreshTokenAdd = new UserRefreshTokenDto()
                    {
                        RefreshToken = result.Data.RefreshToken,
                        RefresTokenExpiration = result.Data.RefreshTokenEndDate,
                        UserId = user.Id
                    };
                    _userService.UpdateRefreshToken(refreshTokenAdd);
                    return Ok(result);
                }

                else
                {
                    return BadRequest();
                }
            }
            else
            {
                if (!_cacheManager.IsAdd(user.Id.ToString()))
                {
                    throw new SecuredOperationException(UserMessages.RefreshTokenExpired);
                }
                else
                {
                    _cacheManager.Remove(user.Id.ToString());
                    throw new SecuredOperationException(UserMessages.RefreshTokenExpired);
                }
            }
           
        }


        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
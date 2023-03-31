using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StackoveflowClone.AuthorizationAuthentication;
using StackoveflowClone.Entities;
using StackoveflowClone.Exceptions;
using StackoveflowClone.Models;

namespace StackoveflowClone.Services
{
    public class UserServices : IUserServices
    {
        //private readonly PasswordHasher<User> _passwordHasher;
        private readonly DbContextStackoverflow _dbContext;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserHttpContextService _userHttpContext;

        public UserServices(DbContextStackoverflow dbContext, AuthenticationSettings authenticationSetings
            , IAuthorizationService authorizationService, IUserHttpContextService userHttpContext)
        {
            //_passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _authenticationSettings = authenticationSetings;
            _authorizationService = authorizationService;
            _userHttpContext = userHttpContext;
        }

        public void RegisterUser(UserRegisterDto user)
        {
            var newUser = new User();
            var newUserAddress = new Address();

            newUser.FullName = user.FullName;
            newUser.Email = user.Email;
            newUser.BirthDate = user.BirthDate;
            newUser.PhoneNumber = user.PhoneNumber;
            newUser.PassHash = user.PassHash;

            newUserAddress.User = newUser;
            newUserAddress.UserId = newUser.Id;
            newUserAddress.Country = user.Country;
            newUserAddress.City = user.City;
            newUserAddress.Street = user.Street;
            newUserAddress.Postalcode = user.Postalcode;

            //var hasedPass = _passwordHasher.HashPassword(newUser, user.PassHash);
            //newUser.PassHash = hasedPass;

            _dbContext.Add(newUser);
            _dbContext.Add(newUserAddress);
            _dbContext.SaveChanges();
        }

        public string Login(UserLoginDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Login);

            if (user == null)
                throw new NotFoundException("Invalid email address");

            if (user.PassHash != dto.Pass)
                throw new ForbiddenAccessException("Invalid email or Pass");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim("DataOfBirth", user.BirthDate.ToString("yyyy-MM-dd")),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(double.Parse(_authenticationSettings.JwtExpireDays));

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer
                , claims
                , expires: expires
                , signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public void ChangePass(UserLoginDto userLogin)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == userLogin.Login);
            if (user is null)
                throw new NotFoundException("User not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userHttpContext.User, user
                , new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbiddenAccessException("Invalid Authorization");

            user.PassHash = userLogin.Pass;
            _dbContext.SaveChanges();
        }
    }
}

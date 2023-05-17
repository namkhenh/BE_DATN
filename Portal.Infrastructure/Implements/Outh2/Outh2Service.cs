using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;
using Portal.Application.Interfaces.Outh2;
using Portal.Domain.Entities;
using Portal.Domain.ViewModel.Outh2;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Implements.Outh2
{
  public class Outh2Service : IOuth2Service
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Portal.Domain.Entities.AppUser> _userManager;
        private readonly SignInManager<Portal.Domain.Entities.AppUser> _signInManager;
        private readonly RoleManager<Portal.Domain.Entities.AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        public Outh2Service(ApplicationDbContext context, UserManager<Portal.Domain.Entities.AppUser> userManager, SignInManager<Portal.Domain.Entities.AppUser> signInManager, RoleManager<Portal.Domain.Entities.AppRole> roleManager, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        
        public async Task<string> Authenication(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
                if (!result.Succeeded)
                {
                    throw new Exception("Tk không tồn tại");
                }
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new[]
                {
                new Claim(ClaimTypes.Email,user.Email??""),

                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"] ?? ""));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return "";
        }
        //public bool CheckRefeshToken(RefeshToken refeshTokenOldOfUser)
        //{
        //    if(refeshTokenOldOfUser.ExpiresIn < DateTime.Now)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        public string GetRefeshToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new Portal.Domain.Entities.AppUser()
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };
            var create = await _userManager.CreateAsync(user, request.Password);
            if (create.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}

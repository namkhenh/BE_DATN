using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Common.Common.ActionResponse;
using Common.Dtos;
using Common.Dtos.Outh2;
using Common.Dtos.User;
using Common.Share.Common;
using Common.Share.SeedWorks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.IO;
using MongoDB.Driver;

using Portal.Application.Interfaces.User;
using Portal.Application.Repositories.Interfaces.RefeshToken;
using Portal.Domain.Entities;
using Portal.Domain.Enums;
using Portal.Domain.ViewModel.Outh2;
using Portal.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Implements.User
{
  public class UserService : IUserService
  {
    private readonly IConfiguration _config;
    private readonly IRefeshTokenRepository _refeshTokenRepository;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    protected readonly UserManager<Portal.Domain.Entities.AppUser> _userManager;
    private readonly SignInManager<Portal.Domain.Entities.AppUser> _signInManager;
    protected readonly RoleManager<Portal.Domain.Entities.AppRole> _roleManager;
    public UserService(UserManager<Portal.Domain.Entities.AppUser> userManager, RoleManager<Portal.Domain.Entities.AppRole> roleManager, ApplicationDbContext context
      , IMapper mapper, SignInManager<Portal.Domain.Entities.AppUser> signInManager, IConfiguration config, IRefeshTokenRepository refeshTokenRepository)
    {
      _refeshTokenRepository = refeshTokenRepository;
      _config = config;
      _mapper = mapper;
      _userManager = userManager;
      _roleManager = roleManager;
      _context = context;
      _signInManager = signInManager;
    }
    public virtual string ConvertKeyFromString(string id)
    {
      if (id == null)
      {
        return default;
      }
      return (string)TypeDescriptor.GetConverter(typeof(string)).ConvertFromInvariantString(id);
    }
    public virtual Task<bool> ExistsUserAsync(string userId)
    {
      var id = ConvertKeyFromString(userId);
      return _userManager.Users.AnyAsync(x => x.Id.Equals(id));
    }

    public async Task<ActionResponse<UserDto>> CreateUserAsync(CreateUpdateUser request)
    {
      ActionResponse<UserDto> resultUserRes = new();
      var user = new Portal.Domain.Entities.AppUser()
      {
        UserName = request.UserName,
        Description = request.UserName,
        PhoneNumber = request.PhoneNumber,
        Email = request.Email,
        FirstName = request.FirstName,
        LastName = request.LastName,
        CreatedDate = DateTimeOffset.Now,
        LastModifiedDate = DateTimeOffset.Now
      };
      var identityResult = await _userManager.CreateAsync(user, request.Password);
      if (identityResult.Succeeded)
      {
        var userDto = _mapper.Map<Portal.Domain.Entities.AppUser, UserDto>(user);
        resultUserRes.Success = true;
        resultUserRes.Data = userDto;
      }
    
      return resultUserRes;
    }

    public async Task<ActionResponse<UserDto>> CreateUserClaimsAsync(string userid, string claimType, string claimValue)
    {
      ActionResponse<UserDto> result = new();
      var user = await _userManager.FindByIdAsync(userid.ToString());
      if (user != null)
      {
        var userClaim = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
        if (userClaim.Succeeded)
        {
          var userDto = _mapper.Map<Portal.Domain.Entities.AppUser, UserDto>(user);
          result.Success = true;
          result.Data = userDto;
        }

      }
      return result;
    }

    public async Task<ActionResponse<UserDto>> CreateUserRoleAsync(string userId, string roleId)
    {
      ActionResponse<UserDto> result = new();
      var user = await _userManager.FindByIdAsync(userId);
      if (user != null)
      {
        var selectRole = await _roleManager.FindByIdAsync(roleId);
        if (selectRole != null)
        {
          await _userManager.AddToRoleAsync(user, selectRole.Name);
          var userDto = _mapper.Map<Portal.Domain.Entities.AppUser, UserDto>(user);
          result.Success = true;
          result.Data = userDto;
        }
      }
      return result;
    }

    public async Task<ActionResponse<UserDto>> DeleteUserAsync(string userId)
    {
      ActionResponse<UserDto> result = new();
      var userIdentity = await _userManager.FindByIdAsync(userId);
      if (userIdentity != null)
      {
        await _userManager.DeleteAsync(userIdentity);
        UserDto userDto = _mapper.Map<Portal.Domain.Entities.AppUser, UserDto>(userIdentity);
        result.Success = true;
        result.Data = userDto;
        return result;
      }
      return result;
    }

    public Task<ActionResponse<UserClaim>> DeleteUserClaimAsync(string userId, int claimId)
    {
      throw new NotImplementedException();
    }

    public Task<ActionResponse<UserDto>> DeleteUserRoleAsync(string userId, string roleId)
    {
      throw new NotImplementedException();
    }

    public async Task<ActionResponse<UserDto>> GetUserAsync(string userId)
    {
      ActionResponse<UserDto> result = new();
      var user = await _userManager.FindByIdAsync(userId);
      UserDto userDto = new();
      if (user != null)
      {
        userDto = _mapper.Map<Portal.Domain.Entities.AppUser, UserDto>(user);

      }
      return result;
    }

    public async Task<ActionResponse<PagedList<UserDto>>> GetUserByRoleAsync(string roleId, string search, int pageIndex = 1, int pageSize = 10)
    {
      ActionResponse<PagedList<UserDto>> result = new();
      var id = ConvertKeyFromString(roleId);
      PagedList<Portal.Domain.Entities.AppUser> pagedList = new();
      var users = _context.Set<Portal.Domain.Entities.AppUser>()
               .Join(_context.Set<IdentityUserRole<string>>(), u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
               .Where(t => t.ur.RoleId.Equals(id))
               .WhereIf(!string.IsNullOrEmpty(search), t => t.u.UserName.Contains(search) || t.u.Email.Contains(search))
               .Select(t => t.u);
      var pagedUsers = await users.PageBy(x => x.Id, pageIndex, pageSize)
                .ToListAsync();
      List<UserDto> lstUserDto = new();
      lstUserDto = _mapper.Map<List<Portal.Domain.Entities.AppUser>, List<UserDto>>(pagedUsers);
      var pageList = new PagedList<UserDto>(lstUserDto, pagedUsers.Count(), pageIndex, pageSize);
      result.Success = true;
      result.Data = pageList;
      return result;
    }

    public Task<ActionResponse<UserClaim>> GetUserClaimAsync(string userId, int claimId)
    {
      throw new NotImplementedException();
    }

    public Task<PagedList<UserClaim>> GetUserClaimsAsync(string userId, int pageIndex = 1, int pageSize = 10)
    {
      throw new NotImplementedException();
    }

    public Task<ActionResponse<UserDto>> GetUsersAsync(string search, int pageIndex = 1, int pageSize = 10)
    {
      throw new NotImplementedException();
    }

    public Task<ActionResponse<UserDto>> GetUsersByClaimAsync(string claimType, string claimValue, int pageIndex = 1, int pageSize = 10)
    {
      throw new NotImplementedException();
    }

    public Task<(ActionResponse<UserDto> resultUser, string userId)> UpdateUserAsync(CreateUpdateUser request)
    {
      throw new NotImplementedException();
    }

    public Task<ActionResponse<UserClaim>> UpdateUserClaimsAsync(string userId, string claimType, string claimValue, int claimId)
    {
      throw new NotImplementedException();
    }

    public Task<ActionResponse<UserDto>> UserChangePasswordAsync(string userId, string password)
    {
      throw new NotImplementedException();
    }

    public Task<ActionResponse<UserDto>> UpdateUser(CreateUpdateUser request)
    {
      throw new NotImplementedException();
    }

    public async Task<ActionResponse<UserDto>> Register(RegisterRequest request)
    {
      ActionResponse<UserDto> result = new();
      try
      {
        var userFromDbBeforeCreated = await _userManager.FindByNameAsync(request.UserName);
        if(userFromDbBeforeCreated== null)
        {
          var user = new Portal.Domain.Entities.AppUser()
          {
            UserName = request.UserName,
            Email = request.Email,
            Code = request.UserName,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.FirstName,
            Description= request.Description,
            CreatedDate = DateTime.Now, 
          };
          var userCreated = await _userManager.CreateAsync(user, request.Password);
          if (userCreated.Succeeded)
          {
            var appUserFromDb = await _userManager.FindByNameAsync(request.UserName);
            if (appUserFromDb != null)
            {
              var userDto = _mapper.Map<Portal.Domain.Entities.AppUser, UserDto>(appUserFromDb);
              result.Success = true;
              result.Data = userDto;
            }
          }
        }
        else
        {
          result.Success = false;
          result.Message = $"User {request.UserName} Created";
        }
        return result;
      }
      catch(Exception ex)
      {
        result.Success = false;
        result.Message = ex.Message;
        return result;
      }
      
     
    }

    public async Task<ActionResponse<TokenResponse>> Authentication(Common.Dtos.Outh2.AuthenticationRequest request)
    {
      ActionResponse<TokenResponse> resultToken = new();
      var user = await _userManager.FindByNameAsync(request.UserName);

      var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
      if (!result.Succeeded)
      {
        throw new Exception("Tk không tồn tại");
      }
      var roles = await _userManager.GetRolesAsync(user);
      var claims = new[]
      {
              new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(_config["Tokens:Issuer"],
          _config["Tokens:Issuer"],
          claims,
          expires: DateTime.Now.AddDays(1),
          signingCredentials: creds);
      var accessToken= new JwtSecurityTokenHandler().WriteToken(token);
      var refreshTokenNew = Guid.NewGuid().ToString().Replace("-", "");
      RefeshToken refeshToken = new();
      refeshToken.RefeshTokenName = refreshTokenNew;
      refeshToken.UserName = user.UserName;
      refeshToken.ExTime = DateTime.Now.AddDays(1);
      TokenResponse tokenRes = new();
      tokenRes.AccessToken = accessToken;
      tokenRes.RefeshToken = refreshTokenNew;
      await _refeshTokenRepository.CreateAsync(refeshToken);
      resultToken.Data = tokenRes;
      return resultToken;
    }

    public async Task<ActionResponse<TokenResponse>> RefeshToken(string refeshToken ,string accessToken)
    {
      ActionResponse<TokenResponse> result = new();
      var principal = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(accessToken)));
      var rolenmae = principal.Identities.ToList();
      var rolecontent = rolenmae[0].Claims.ToList();
      var userName = rolecontent[2].Value;
      var email = rolecontent[1].Value;
      var responserole = rolecontent[0].Value;
      var listrole = responserole.Split(";").ToList();
      var refeshTokenOld = await _refeshTokenRepository.FindAll().OrderByDescending(x=>x.CreatedDate).Where(x=>x.UserName == userName).FirstOrDefaultAsync();
      var utcExpireDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
      var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
      if (expireDate > DateTime.Now)
      {
        return ActionResponse<TokenResponse>.CreateErrorResponse("Token vẫn còn hạn!");
      }
      if (string.IsNullOrEmpty(refeshToken) || !refeshToken.Equals(refeshTokenOld.RefeshTokenName))
      {
        return ActionResponse<TokenResponse>.CreateErrorResponse("Token refresh không đúng. Yêu cầu đăng xuất, đăng nhập lại!");
      }
      if (refeshTokenOld.ExTime < DateTime.Now)
      {
        return ActionResponse<TokenResponse>.CreateErrorResponse("Token hết hạn mời bạn đăng xuất và đăng nhập lại");
      }
      var refreshTokenNew = Guid.NewGuid().ToString().Replace("-", "");
      //var idRefeshTokenNew = await _refeshTokenRepository.CreateAsync(new RefeshToken()
      //{
      //  UserName = userName,
      //  RefeshTokenName = refreshTokenNew,
      //  ExTime = DateTime.UtcNow.AddHours(3),
      //});
      if (string.IsNullOrEmpty(refeshToken.ToString())==false)
      {
        var claims = new[]
        {
                new Claim(ClaimTypes.Email,email),

                new Claim(ClaimTypes.Role, string.Join(";",listrole)),
                new Claim(ClaimTypes.Name, userName)
            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_config["Tokens:Issuer"],
         _config["Tokens:Issuer"],
         claims,
         expires: DateTime.Now.AddHours(3),
         signingCredentials: creds);
        var accessTokenNew = new JwtSecurityTokenHandler().WriteToken(token);
        var tokenRes = new TokenResponse()
        {
          AccessToken = accessTokenNew,
          RefeshToken = refreshTokenNew
        };
        result.Data = tokenRes;
      }
      return result;
    }
    private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      dateTime = dateTime.AddSeconds(utcExpireDate).ToLocalTime();
      return dateTime;
    }
    public string GetUserEmailFromToken(string token)
    {
      var jwtHandler = new JwtSecurityTokenHandler();
      var jwtToken = jwtHandler.ReadJwtToken(token);
      var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
      if (userIdClaim != null)
      {
        return userIdClaim.Value;
      }
      else
      {
        return null;
      }
    }
    private ClaimsPrincipal ValidateToken(string jwtToken)
    {
      IdentityModelEventSource.ShowPII = true;

      SecurityToken validatedToken;
      TokenValidationParameters validationParameters = new TokenValidationParameters();

      validationParameters.ValidateLifetime = true;

      validationParameters.ValidAudience = _config["Tokens:Issuer"];
      validationParameters.ValidIssuer = _config["Tokens:Issuer"];
      validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

      ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
      return principal;
    }
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
      var claims = new List<Claim>();
      var payload = jwt.Split('.')[1];
      var jsonBytes = ParseBase64WithoutPadding(payload);
      var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

      keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

      if (roles != null)
      {
        if (roles.ToString().Trim().StartsWith("["))
        {
          var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

          foreach (var parsedRole in parsedRoles)
          {
            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
          }
        }
        else
        {
          claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
        }

        keyValuePairs.Remove(ClaimTypes.Role);
      }

      claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

      return claims;
    }
    private byte[] ParseBase64WithoutPadding(string base64)
    {
      switch (base64.Length % 4)
      {
        case 2: base64 += "=="; break;
        case 3: base64 += "="; break;
      }
      return Convert.FromBase64String(base64);
    }
    public string GetUserIdFromToken(string token)
    {
      var jwtHandler = new JwtSecurityTokenHandler();
      var jwtToken = jwtHandler.ReadJwtToken(token);
      var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (userIdClaim != null)
      {
        return userIdClaim.Value;
      }
      else
      {
        return null;
      }
    }
  }
}

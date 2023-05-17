using Common.Common.ActionResponse;
using Common.Dtos.Outh2;
using Common.Dtos.User;
using Common.Share.SeedWorks;
using Hangfire.Mongo.Dto;
using Microsoft.AspNetCore.Identity;
using Portal.Domain.Entities;
using Portal.Domain.ViewModel.Outh2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces.User
{
  public interface IUserService
  {
    Task<ActionResponse<TokenResponse>> Authentication(Common.Dtos.Outh2.AuthenticationRequest request);
    Task<ActionResponse<TokenResponse>> RefeshToken(string refeshToken, string accessToken);
    Task<ActionResponse<UserDto>> UpdateUser(CreateUpdateUser request);
    Task<ActionResponse<UserDto>> Register(RegisterRequest request);
    Task<bool> ExistsUserAsync(string userId);

    Task<ActionResponse<UserDto>> GetUsersAsync(string search, int pageIndex = 1, int pageSize = 10);
    Task<ActionResponse<UserDto>> GetUsersByClaimAsync(string claimType, string claimValue, int pageIndex = 1, int pageSize = 10);

    Task<ActionResponse<PagedList<UserDto>>> GetUserByRoleAsync(string roleId, string search, int pageIndex = 1, int pageSize = 10);
    Task<ActionResponse<UserDto>> GetUserAsync(string userId);
    Task<ActionResponse<UserDto>> CreateUserAsync(CreateUpdateUser request);
    Task<(ActionResponse<UserDto> resultUser, string userId)> UpdateUserAsync(CreateUpdateUser request);
    Task<ActionResponse<UserDto>> DeleteUserAsync(string userId);
    Task<ActionResponse<UserDto>> CreateUserRoleAsync(string userId, string roleId);
    Task<ActionResponse<UserDto>> DeleteUserRoleAsync(string userId, string roleId);
    Task<PagedList<UserClaim>> GetUserClaimsAsync(string userId, int pageIndex = 1,
           int pageSize = 10);
    Task<ActionResponse<UserClaim>> GetUserClaimAsync(string userId, int claimId);
    Task<ActionResponse<UserDto>> CreateUserClaimsAsync(string userid, string claimType, string claimValue);
    Task<ActionResponse<UserClaim>> UpdateUserClaimsAsync(string userId, string claimType, string claimValue, int claimId);
    Task<ActionResponse<UserClaim>> DeleteUserClaimAsync(string userId, int claimId);
    Task<ActionResponse<UserDto>> UserChangePasswordAsync(string userId, string password);
  }
}

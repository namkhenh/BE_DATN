using Common.Dtos.Outh2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Interfaces.User;
using Portal.Application.Repositories.Interfaces.Menu;
using Portal.Domain.ViewModel.Outh2;

namespace Portal.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IMenuRepository _menuRepository;
    private readonly IUserService _userService;
    public UserController(IUserService userService, IMenuRepository menuRepository)
    {
      _menuRepository= menuRepository;
      _userService = userService;
    }
    [HttpPost("Authen")]
    public async Task<IActionResult> Authen([FromForm] Common.Dtos.Outh2.AuthenticationRequest request)
    {
      var result = await _userService.Authentication(request);
      return Ok(result.Data);
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm]RegisterRequest request)
    {
      var result = await _userService.Register(request);
      return Ok(result.Data);
    }
    [HttpGet("GetMenu")]
    [Authorize]
    public async Task<IActionResult> GetMenu()
    {
      var lst = await _menuRepository.FindAll().ToListAsync();
      return Ok(lst);
    }
    [HttpPost("RefeshToken")]
    [AllowAnonymous]
    public async Task<IActionResult> RefeshToken([FromForm] TokenRequest request)
    {
      var result = await _userService.RefeshToken(request.RefeshToken, request.AccessToken);
      return Ok(result.Data);
    }
  }
}

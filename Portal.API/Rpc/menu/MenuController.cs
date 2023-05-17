using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portal.API.Rpc.menu
{
  [Route("api/[controller]")]
  [ApiController]
  public class MenuController : RpcController
  {
    [Route(MenuRoot.Get), HttpPost]
    public IActionResult Demo()
    {
      return Ok();
    }
  }
}

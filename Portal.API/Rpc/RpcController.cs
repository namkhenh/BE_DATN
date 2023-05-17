using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portal.API.Rpc
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Policy = "Permission")]
  public class RpcController : ControllerBase
  {

  }
}

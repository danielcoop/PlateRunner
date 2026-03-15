using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlateRunner.Package.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "PlateRunner.Package")]
    public class PlateRunnerPackageApiController : PlateRunnerPackageApiControllerBase
    {

        [HttpGet("ping")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        public string Ping() => "Pong";
    }
}

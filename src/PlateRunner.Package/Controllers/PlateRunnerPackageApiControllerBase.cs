using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Routing;

namespace PlateRunner.Package.Controllers
{
    [ApiController]
    [BackOfficeRoute("platerunnerpackage/api/v{version:apiVersion}")]
    [Authorize(Policy = AuthorizationPolicies.SectionAccessContent)]
    [MapToApi(PackageConstants.ApiName)]
    public class PlateRunnerPackageApiControllerBase : ControllerBase
    {
    }
}

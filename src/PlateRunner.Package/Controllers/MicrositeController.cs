using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using PlateRunner.Package.Services.Interfaces;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace PlateRunner.Package.Controllers;

/// <summary>
/// Render controller for the <c>microsite</c> document type.
///
/// Umbraco automatically routes GET requests for any published node of type
/// <c>microsite</c> through this controller because its name matches the
/// document type alias (PascalCase convention).
///
/// The controller delegates all content retrieval and view-model construction
/// to <see cref="IMicrositeQueryService"/> and <see cref="IMicrositeMapper"/>
/// so that the action method stays thin and testable.
/// </summary>
public sealed class MicrositeController : RenderController
{
    private readonly IMicrositeQueryService _queryService;
    private readonly IMicrositeMapper _mapper;

    public MicrositeController(
        ILogger<MicrositeController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IMicrositeQueryService queryService,
        IMicrositeMapper mapper)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _queryService = queryService;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the default GET for a microsite node.
    /// Resolves the microsite ancestor (or the node itself), maps it to a
    /// <see cref="PlateRunner.Package.ViewModels.MicrositeViewModel"/>, then
    /// renders the view path declared on the resolved theme.
    /// </summary>
    public override IActionResult Index()
    {
        var content = CurrentPage;

        if (content is null)
        {
            return base.Index();
        }

        var micrositeContent = _queryService.GetMicrositeContent(content);

        if (micrositeContent is null)
        {
            // Node is not a microsite and has no microsite ancestor — fall through
            // to Umbraco's default rendering so the host site can handle it.
            return base.Index();
        }

        var viewModel = _mapper.Map(micrositeContent);

        // Render using the fully-qualified view path from the resolved theme,
        // e.g. "~/Views/Themes/MinimalElegant/Index.cshtml"
        return View(viewModel.Theme.ViewPath, viewModel);
    }
}

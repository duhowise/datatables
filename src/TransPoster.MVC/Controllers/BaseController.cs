using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TransPoster.MVC.Controllers;

public abstract class BaseController<T> : Controller
{
    private IMediator? _mediatorInstance;
    private ILogger<T>? _loggerInstance;
    private IStringLocalizer<T>? _localizerInstance;

    protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
    protected IStringLocalizer<T> _localizer => _localizerInstance ??= HttpContext.RequestServices.GetService<IStringLocalizer<T>>();

}
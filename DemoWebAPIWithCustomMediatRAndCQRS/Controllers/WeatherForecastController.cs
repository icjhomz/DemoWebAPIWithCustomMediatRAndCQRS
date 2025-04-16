using CustomMediatR.Common.Interfaces;
using DemoWebAPIWithCustomMediatRAndCQRS.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPIWithCustomMediatRAndCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMediator _mediator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("GetCustomerGuid")]
    public async Task<ActionResult<Guid>> GetUserNameAsync()
    {
        Guid userId = await _mediator.Send<CreateUserCommand, Guid>(new CreateUserCommand("John"));

        return Ok(userId);
    }

    [HttpGet("GetUserName/{userId}")]
    public async Task<ActionResult<string>> GetUserNameAsync(Guid userId)
    {
        string userName = await _mediator.Send<GetUserNameCommand, string>(new GetUserNameCommand(userId));

        return Ok(userName);
    }
}

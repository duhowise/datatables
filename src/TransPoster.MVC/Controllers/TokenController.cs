using Microsoft.AspNetCore.Mvc;
using TransPoster.Application.Features.Auth.Token.Command;

namespace TransPoster.MVC.Controllers
{
    public class TokenController : BaseApiController<TokenController>
    {
        [HttpPost()]
        public async Task<IActionResult> Get(AuthenticationCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : Unauthorized(result);
        }
    }
}

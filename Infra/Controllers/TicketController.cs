using Microsoft.AspNetCore.Mvc;
using Trackit.Core.UseCases;

namespace Trackit.Infra.Controller;

[ApiController]
[Route("api/v1/tickets")]
public class TicketController : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> CreateTicket([FromBody]CreateTicketDTO req, [FromServices]CreateTicketUseCase useCase)
  {
    try
    {
      await useCase.Execute(req.ClientId, req.TechId, req.Category, req.Priority, req.Description);
      return Created();
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
using Microsoft.AspNetCore.Mvc;
using Trackit.Core.UseCases;
using Trackit.Infra.DTO;

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

[HttpGet]
  public async Task<IActionResult> ListTicket([FromServices]ListTicketsUseCase useCase)
  {
    try
    {
      var tickets = await useCase.Execute();
      return Ok(tickets);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpDelete]
  public async Task<IActionResult> DeleteTicket([FromBody]string id, [FromServices]DeleteTicketUseCase useCase)
  {
    try
    {
      await useCase.Execute(id);
      return Ok();
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
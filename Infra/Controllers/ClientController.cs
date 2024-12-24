using Microsoft.AspNetCore.Mvc;
using Trackit.Core.UseCases;
using Trackit.Infra.DTO;

namespace Trackit.Infra.Controller;

[ApiController]
[Route("api/v1/clients")]
public class ClientController : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> CreateClient([FromBody]CreateClientDTO req, [FromServices]CreateClientUseCase useCase)
  {
    try
    {
      await useCase.Execute(req.Name, req.Cnpj, req.Phone, req.Email);
      return Created();
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
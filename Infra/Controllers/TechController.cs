using Microsoft.AspNetCore.Mvc;
using Trackit.Core.UseCases;
using Trackit.Infra.DTO;

namespace Trackit.Infra.Controller;

[ApiController]
[Route("api/v1/techs")]
public class TechController : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> CreateTech([FromBody]CreateTechDTO req, [FromServices]CreateTechUseCase useCase)
  {
    try
    {
      await useCase.Execute(req.Login, req.Name, req.Password, req.Phone, req.Role, req.Email);
      return Created();
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet]
  public async Task<IActionResult> ListTech([FromQuery]PaginationDTO req, [FromServices]ListTechUseCase useCase)
  {
    try
    {
      var techs = await useCase.Execute(req.Skip, req.Take);
      return Ok(techs);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
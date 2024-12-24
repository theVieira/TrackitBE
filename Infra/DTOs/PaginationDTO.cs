namespace Trackit.Infra.Controller;

public class PaginationDTO
{
  public int Skip { get; set; } = 0;
  public int Take { get; set; } = 20;
}
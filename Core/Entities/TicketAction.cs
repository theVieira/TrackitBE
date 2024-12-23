using System.Text.Json.Serialization;

namespace Trackit.Core.Entities;

public class TicketAction : BaseEntity
{
  public ActionType Type { get; set; }
  public string? Text { get; set; } = string.Empty;
  public Tech Author { get; set; } = new();

  public TicketAction() {}

  public TicketAction(Tech author)
  {
    Author = author;
    Type = ActionType.Time;
  }

  public TicketAction(Tech author, string text)
  {
    Type = ActionType.Text;
    Author = author;
    Text = text;
  }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ActionType
{
  Text,
  Time
}
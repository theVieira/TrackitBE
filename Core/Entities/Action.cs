using System.Text.Json.Serialization;
using Trackit.Utils;

namespace Trackit.Core.Entities;

public class Action : BaseEntity
{
  public ActionType Type { get; set; } = ActionType.Time;
  public Tech Author { get; private set; } = new();
  public string? Text { get; private set; }

  public Action() {}

  public Action(Tech author)
  {
    Type = ActionType.Time;
    Author = author;
  }

  public Action(Tech author, string text)
  {
    Author = author;
    Text = SpellCheck.CapitalizeText(text);
    Type = ActionType.Text;
  }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ActionType
{
  Text,
  Time
}
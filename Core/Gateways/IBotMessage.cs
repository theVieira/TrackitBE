namespace Trackit.Core.Gateways;

public interface IBotMessage
{
  Task SendMessage(string message);
}
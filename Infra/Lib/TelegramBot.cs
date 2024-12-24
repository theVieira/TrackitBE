using DotNetEnv;
using Telegram.Bot;
using Trackit.Core.Gateways;

namespace Trackit.Infra.Lib;

public class TelegramBot : IBotMessage
{
  private readonly string BotToken = Env.GetString("TELEGRAM_BOT_TOKEN");
  private readonly string ChatId = Env.GetString("TELEGRAM_CHAT_ID");

  public async Task SendMessage(string message)
  {
    var bot = new TelegramBotClient(BotToken);

    try
    {
      await bot.SendMessage(ChatId, message);
      return;
    }
    catch (Exception error)
    {
      throw new Exception($"send message error {error.Message}");
    }
  }
}
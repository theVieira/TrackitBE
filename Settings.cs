using DotNetEnv;

namespace Trackit;

public abstract class Settings
{
    public static string ConnectionString => Environment
        .GetEnvironmentVariable("CONNECTION_STRING") ?? string.Empty;

    public static string UploadUrl => Environment
        .GetEnvironmentVariable("UPLOAD_URL") ?? string.Empty;
    
    public static string UploadPath => Environment
        .GetEnvironmentVariable("UPLOAD_PATH") ?? string.Empty;
    
    public static string BotToken => Environment
        .GetEnvironmentVariable("BOT_TOKEN") ?? string.Empty;
    
    public static string ChatId => Environment
        .GetEnvironmentVariable("CHAT_ID") ?? string.Empty;
}
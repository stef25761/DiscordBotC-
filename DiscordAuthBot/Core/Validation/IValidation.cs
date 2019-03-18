using Discord.WebSocket;

namespace DiscordAuthBot.Core.Validation
{
    public interface IValidation
    {
        bool HasRole(SocketGuildUser user, string role);
    }
}

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordAuthBot.Core.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordAuthBot.Core.Commands
{
    
    public class UserCommand : ModuleBase<SocketCommandContext>, IValidation
    {
        public bool HasRole(SocketGuildUser user, string role)
        {
            return user.Roles.Any(x => x.Name == role);
        }
        [RequireContext(ContextType.Guild)]
        [Command("dm"), Summary("öffnet eine Direkt Nachricht, kurz dm")]
        public async Task DmMe()
        {
            var user = Context.User as SocketUser;
            string msg = string.Format("Hallo was kann ich für dich machen, {0}?" +
                "Gebe !help ein um meine Befehle zu sehen", user.Username);
            await user.SendMessageAsync(msg);
        }
        [RequireUserPermission(GuildPermission.SendMessages)]
        [RequireContext(ContextType.DM)]
        [Command("reg"), Alias("reg"), Summary("Server Authentifizierung")]
        public async Task SelfRegUser(string apiKey)
        {
            // GetOrCreateDMChannelAsync(RquestOption)
            // IUser interface to get user informations
            var user = Context.User as SocketGuildUser;
            var serverRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Kodash") as SocketRole;
            if (HasRole(user, serverRole.Name))
            {
                string msg = string.Format("Du bist in der Gruppe: {0}", serverRole.Name);
                await Context.User.SendMessageAsync(msg);
            }
            else
            {
                //TODO add gw2 api key here
                string msg = string.Format("Du wurdest der Gruppe {0} hinzugefügt", serverRole.Name);
                await user.AddRoleAsync(serverRole);
                await Context.User.SendMessageAsync(msg);
            }

        }
    }
}

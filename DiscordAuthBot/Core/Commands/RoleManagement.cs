using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordAuthBot.Core.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordAuthBot.Core.Commands
{
    
    public class RoleManagement : ModuleBase<SocketCommandContext>, IValidation
    {

        [RequireOwner]
        [Command("add"), Alias("add", "edit"),Summary("Nachtragen eines User in der Datenbank")]
        public async Task AddUserRole(SocketGuildUser user , string apiKey)
        {   //TODO add method is for admin, to add manual a user to the server or edit a user
            await Context.Channel.SendMessageAsync("Hello from the edit/add Command");
        }
        [RequireOwner]
        [Command("purge"), Alias("cleanup"), Summary("Löscht alle Nachrichten im Channel")]
        public async Task PurgeChat()
        {   //TODO purge all message in the current channel
            await Context.Channel.SendMessageAsync("Hello from the edit/add Command");
        }
        [RequireOwner]
        [Command("remove"), Alias("delete"),Summary("Entfernen der Usergruppe eines Users.Kann auch mit kick verbunden werden, falls nötig")]
        public async Task RemoveUserRole(SocketGuildUser user, string role)
        {
            var serverRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == role) as SocketRole;
            string byeMessage = string.Format("Du wurdest aus der Gruppe {0} entfernt", role);
            string errMsg = string.Format("Die angegebene Rolle: {0},  ist keine Rolle auf dem Server", role);
            string userNotInRoleMsg = string.Format("{0} hat nicht die Rolle {1}",user.Username, role);

            if (serverRole != null)
            {
                if (HasRole(user,role))
                {
                    await user.RemoveRoleAsync(serverRole);
                    await user.SendMessageAsync(byeMessage);
                }
                else
                {
                    await Context.Channel.SendMessageAsync(userNotInRoleMsg);
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(errMsg);

            }
        }
       
       
        private bool CorrectApiKey(string apikey)
        {
            return true;
        }

        public bool HasRole(SocketGuildUser user, string role)
        {
            return user.Roles.Any(x => x.Name == role);
        }
    }
}

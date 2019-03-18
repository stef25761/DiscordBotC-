using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordAuthBot.Core.Commands
{

    public class Help : ModuleBase<SocketCommandContext>
    {

        private CommandService _commandService;
        public Help(CommandService commandService)
        {
            _commandService = commandService;
        }
        [RequireUserPermission(GuildPermission.SendMessages)]
        [Command("help"), Alias("Help"), Summary("Hilfe benötigt? gebe !help oder !Help ein")]
        public async Task HelpTask()
        {
            EmbedBuilder builder = new EmbedBuilder();
            List<CommandInfo> commands = _commandService.Commands.ToList();
            foreach (CommandInfo command in commands)
            {
                var precontitions = command.Preconditions;
                foreach (var condition in precontitions)
                {
                    if (condition.CheckPermissionsAsync(Context, command, null).
                        Result.IsSuccess||precontitions.Count==0)
                    {
                        string embedFieldText = command.Summary ?? "Keine Info verfügbar";
                        builder.AddField(command.Name, embedFieldText);

                    }
                }
            }

            builder.WithAuthor(Context.Client.CurrentUser).
                WithCurrentTimestamp().
                WithTitle("Folgende Commands sind verfügbar:");
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }



    }
}


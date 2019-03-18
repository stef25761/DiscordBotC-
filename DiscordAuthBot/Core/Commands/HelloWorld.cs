using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordAuthBot.Core.Commands
{   // need SocketCOmmandConntext, without this u cant use the context
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloWorld")]
        public async Task dummyCommand()
        {
            await Context.Channel.SendMessageAsync("Hello world from C# Bot!");
        }
    }
}

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordAuthBot.Core.Commands;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordAuthBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;


        //start this mehtod if i start the bot
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug

            });
            _commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug

            });
            
            // important. If this line miss, no command works
            _client.MessageReceived += Client_MessageReceived;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                services: null);
            Help help = new Help(_commands);
            _client.Ready += Client_Ready;
            _client.Log += Client_Log;
            string token = "";
            using (var stream = new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).
                Replace(@"bin\Debug\netcoreapp2.0", @"Data\Token.txt"), FileMode.Open, FileAccess.Read))
            using (var readToken = new StreamReader(stream))
            {
                token = readToken.ReadToEnd();

                Console.WriteLine("dein aktueller Bot Token: " + token);
            }
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage arg)
        {
            Console.WriteLine($"{DateTime.Now} at {arg.Source}]{arg.Message}");
        }

        private async Task Client_Ready()
        {
            await _client.SetGameAsync("!help für Hilfe");
        }

        private async Task Client_MessageReceived(SocketMessage messageParam)
        {
            //conf commands 
            // as fired null, if it not the right data type
            var message = messageParam as SocketUserMessage;
            var ctx = new SocketCommandContext(_client, message);
            if (ctx.Message == null || ctx.Message.Content == "") return;
            if (ctx.User.IsBot) return;
            int argPos = 0;
            if (!(message.HasStringPrefix("!", ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            var restult = await _commands.ExecuteAsync(
                context: ctx,
                argPos: argPos,
                services: null);
            if (!restult.IsSuccess) await ctx.Channel.SendMessageAsync(restult.ErrorReason);
        }


    }
}

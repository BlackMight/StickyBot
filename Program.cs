using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using StickyBot.Core;

namespace StickyBot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        
  

        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

         

        public async Task StartAsync()
        {
            

            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;
            _client.Ready += RepeatingTimer.StartTimer;
            _client.ReactionAdded += OnReactionAdded;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            Global.Client = _client; 
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await ConsoleInput(); 
            await Task.Delay(-1);
        }

        private async Task ConsoleInput()
        {
            var input = string.Empty;
            while (input.Trim().ToLower() != "block")
            {
                
                input = Console.ReadLine();
                if (input.Trim().ToLower() == "message")
                {
                    ConsoleSendMessage();
                }
            }
        }

        private async void ConsoleSendMessage()
        {
            Console.WriteLine("Select the guild: ");
            var guild = GetSelectedGuild(_client.Guilds);
            var textChannel = GetSelectedTextChannel(guild.TextChannels);
            var msg = string.Empty;
            while(msg.Trim() == string.Empty)
            {
                Console.WriteLine("To The Republic: ");
                msg = Console.ReadLine();
            }
            await textChannel.SendMessageAsync(msg);
        }

        private SocketTextChannel GetSelectedTextChannel(IEnumerable<SocketTextChannel> channels)
        {
            var textChannels = channels.ToList();
            var maxIndex = textChannels.Count() - 1;
            for (var index = 0; index <= maxIndex; index++)
            {
                Console.WriteLine($"{index} - {textChannels[index].Name}");
            }

            var selectedIndex = -1;
            while (selectedIndex < 0 || selectedIndex > maxIndex)
            {
                var success = int.TryParse(Console.ReadLine().Trim(), out selectedIndex);
                if (!success) Console.WriteLine("That was an invalid index, try again.");
            }

            return textChannels[selectedIndex];
        }

        private SocketGuild GetSelectedGuild(IEnumerable<SocketGuild> guilds)
        {
            var socketGuilds = guilds.ToList();
            var maxIndex = socketGuilds.Count() - 1;
            for(var index = 0; index <= maxIndex; index++)
            {
                Console.WriteLine($"{index} - {socketGuilds[index].Name}");
            }

            var selectedIndex = -1;
            while(selectedIndex < 0 || selectedIndex > maxIndex)
            {
                var success = int.TryParse(Console.ReadLine().Trim(), out selectedIndex);
                if (!success)
                {
                    Console.WriteLine("That was an invalid index, try again.");
                    selectedIndex = -1;
                }
            }

            return socketGuilds[selectedIndex];
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if(reaction.MessageId == Global.MessageIdToTrack)
            {
                if(reaction.Emote.Name == ":Thinking:")
                {
                    await channel.SendMessageAsync("Oh I'm sorry did I break your concentration? Please continue...");
                }
                if (reaction.Emote.Name == "😑")
                {
                   await channel.SendMessageAsync("If Simon wants to play genji into torb, sym, rein, lucio, we have to let him.");
                   await channel.SendMessageAsync("If Azad wants to get all golds we have to BLAME him.");
                   await channel.SendMessageAsync("If David wants to play hitscan we have to let him.");

                }
                
            }
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message); 
        }
    }
   
}

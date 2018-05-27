using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StickyBot.Core.UserAccounts;
using Discord;

namespace StickyBot.Core.LevelingSystem
{
    internal static class Leveling
    {
        internal async static void UserSentMessage(SocketGuildUser user, SocketTextChannel channel)
        {
            //if the user has a timeout, ignore them

            var userAccount = UserAccounts.UserAccounts.GetAccount(user);
            uint oldLevel = userAccount.LevelNumber;
            userAccount.Skill += 50;
            UserAccounts.UserAccounts.SaveAccounts();
            uint newLevel = userAccount.LevelNumber;

            if(oldLevel != newLevel)
            {
                var embed = new EmbedBuilder();
                embed.WithColor(51, 204, 204);
                embed.WithTitle("LEVEL UP");
                embed.WithDescription(user.Username + " leveled up!");
                embed.AddInlineField("LEVEL", newLevel);
                embed.AddInlineField("SKILL", userAccount.Skill);

                
                await channel.SendMessageAsync("", false, embed);

            }
            
        }     
    }
}

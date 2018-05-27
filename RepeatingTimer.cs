using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers; 

namespace StickyBot.Core
{
    internal static class RepeatingTimer
    {
        private static Timer loopingTimer;
        private static SocketTextChannel channel;

        internal static Task StartTimer()
        {
            channel = Global.Client.GetGuild(416809623132176386).GetTextChannel(416809623677304843);

            loopingTimer = new Timer()
            {
                Interval = 43200000,
                AutoReset = true,
                Enabled = true
            };
            loopingTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask; 
        }

        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {
            await channel.SendMessageAsync("Good Morning Citizens of The Democratic People's Republic of David!!");
        }
    }
}

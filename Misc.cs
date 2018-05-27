using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using StickyBot.Core.UserAccounts;

namespace StickyBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {

        // 4.22.2018 Status: Passed
        // Command: Repeats user messages
        // R4I: Hide input message so only message is echoed - Leto, Gif stil needed- Jordan
        [Command("Echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message by " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 191, 255));
            Context.User.GetAvatarUrl();
            embed.WithThumbnailUrl("https://i.pinimg.com/originals/fa/55/57/fa555775ffa52b8f7c3a9865f0eebfa2.gif");
            

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        // 4.22.2018 Status: Passed
        // Command: Bot randomly selects from list
        // R4I: None
        [Command("Pick")]
        public async Task SelectOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("THE CHOICE HAS BEEN MADE: " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(254, 127, 156));
            embed.WithThumbnailUrl("http://i0.kym-cdn.com/photos/images/original/000/926/994/729.gif");

            Context.User.GetAvatarUrl();

            await Context.Channel.SendMessageAsync("", false, embed);
            DataStorage.AddPairToStorage(Context.User.Username + DateTime.Now.ToLongTimeString(), selection);
        }

        // 4.23.2018 Status: Passed
        // Command: DM's secret to Supreme Leader ONLY
        // R4I: None
        [Command("Secret")]
        public async Task RevealSecret([Remainder]string arg = "")
        {
            if (!UserIsSupremeLeader((SocketGuildUser)Context.User)) return;
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }

        private bool UserIsSupremeLeader(SocketGuildUser user)
        {
            string targetRoleName = "Supreme Leader";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);

        }

        // 4.23.2018 Status: Passed
        // Command: Posts Developer Updates to channel 
        // R4I: 
        [Command("Updates")]
        public async Task DevUpdate([Remainder]string arg = "")
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await Context.Channel.SendMessageAsync(Utilities.GetAlert("UPDATES"));
        }

        // 4.22.2018 Status: Passed
        // Command: Posts celebrations to chat
        // R4I: Hide messages - Leto(see echo), Establish more of a difference between echo -David
        [Command("Holiday")]
        public async Task Holiday([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("This Week We're Celebrating! ");
            embed.WithDescription(message);
            embed.WithColor(new Color(255, 211, 0));
            embed.WithThumbnailUrl("https://78.media.tumblr.com/tumblr_m1082bORxM1r04n3so1_500.gif");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        // 4.22.2018 Status: Passed
        // Command: Sends random images to republic channnel
        // R4I: DONE
        [Command("Meme")]
        public async Task ShowMeme([Remainder]string arg = "")
        {
            Random rand;
            string[] memelist;

            rand = new Random(); 

            memelist = new string[]
            {
                "Memes/HoodieDog.gif",
                "Memes/Party's Over.png",
                "Memes/EverythingsOkay.gif",
                "Memes/WheelChairCat.png",
                "Memes/WorldsHardestDab.gif"
            };

            int randomMemeIndex = rand.Next(memelist.Length); 
            string memeToPost = memelist[randomMemeIndex];
            await Context.Channel.SendFileAsync(memeToPost); 

        }

        // 4.22.2018 Status: Passed
        // Command: Sends random images to republic channnel
        // R4I: DONE
        [Command("Quote")]
        public async Task ShowQuote([Remainder]string arg = "")
        {
            Random rand;

            string[] textList;

            rand = new Random();

            textList = new string[]
            {    
               "See if you had a big brain you would have held up your shield, but you have a small brain so you didn't. -Azad",
               "IF SIMON WANTS TO PLAY GENJI AGAINST DVA, WINSTON, SYM WE HAVE TO LET HIM. -David",
               "Beep beep. -Leto",
               "There's just something we don't know about you JBlack. -Azad",
               "Have you ever been bullied? -Azad",
               "All we had to do was follow the damn train, CJ! -Big Smoke",
               "Goodbye forever friends -Jwools",
               "Just wait until one of you lands my brown piece, your f*cked! -David",
               "This game is rigged, RIGGED I tell ya! - David",
               "You're one of those huh? -Azad",
               "You just wait until I get my life together then all you bitches are f*cked! -Leto",
               "You must like wasting my time. -Azad",
               "Throw MY game, will ya!? -David",
               "Tekedits, tek f*cking edits. -David",
               "That's a strat, not a good one but a strat. -Simon",
               "Wait we had a mercy? -Azad",
               "That's craaazzzyyy! -Jordan",
               "INCREDIBLE! - Jordan",
               "Truly unfortunate. -Mr.Monopoly", 
               "My play of the game is next watch this. -Azad",
               "Your family won't last the winter. -David",
               "Bad news guys this is my worst map, it's my doomfist map. -David",
               "PIZZA TIME -David",
               "LASAGNA TIME -Jordan",
               "I rewatch my monopoly win over again on Fridays, just to remind myself of my true potential. -David",
               "Hey Jordan all that sutff I was saying about you earlier, I was actually joking. -Azad",
               "RAILROADS!! -Leto",
               "DOUBLES!! -David",
               "The only good people with mercy are gay guys and girls. -GW Wolfie",
               "You can't heal stupid. -Clarity",
               "I like turtles. -David",
               "We told him to stop going right. He keeps going right! -Azad"
            };

            int randomTextIndex = rand.Next(textList.Length);
            string TextToPost = textList[randomTextIndex];
            await Context.Channel.SendMessageAsync(TextToPost);

        }

        // 4.22.2018 Status: Passed
        // Command: Puts User in timeout for admins only
        // R4I: "Change user nickname to "Bad Cookie"" - David" 
        [Command("Timeout")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task Timeout(IGuildUser user, string reason = "Reason: Read My Title")
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Timeout");
            await (user as IGuildUser).AddRoleAsync(role);
            

        }

        // 4.22.2018 Status: Passed
        // Command: Sends "King You Lose" Gif to The Republic Channel 
        // R4I: DONE
        [Command("Roasted")]
        public async Task Roasted([Remainder]string arg = "")
        {
            var embed = new EmbedBuilder();
            await Context.Channel.SendFileAsync("Memes/GAME OVER.gif");
        }

        // 4.22.2018 Status: Passed
        // Command: Relays stored data into JSON file of number of "pairs"
        // R4I: None
        [Command("Doc")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetInfo()
        {
            await Context.Channel.SendMessageAsync("Document has " + DataStorage.GetPairsCount() + " items.");
            DataStorage.AddPairToStorage("Count" + DataStorage.GetPairsCount(), "TheCount" + DataStorage.GetPairsCount());
        }

        // 4.22.2018 Status: Passed
        // Command: Tell's users Level and Skill
        // R4I: None
        [Command("Level")]
        public async Task MySkill([Remainder]string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();

            target = mentionedUser ?? Context.User;


            var account = UserAccounts.GetAccount(target);
            await Context.Channel.SendMessageAsync(target.Username);
            await Context.Channel.SendMessageAsync($" Level: {account.LevelNumber} Skill Progress: {account.Skill}");
        }

        // 4.22.2018 Status: Passed
        // Command: Tell's users Level and Skill
        // R4I: Allow Admin to add to others
        [Command("Inc")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Increase(uint Skill)
        {
            var account = UserAccounts.GetAccount(Context.User);
            account.Skill += Skill;
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"Skill Increased: {Skill}");
        }

        // 4.29.2018 Status: Passed
        // Command: Has bot react to user emojis
        // R4I: Add more reaction emoji's, allow for reactions besides basic emoji's 
        [Command("React")]
        public async Task HandleReactionMessage()
        {
            RestUserMessage msg = await Context.Channel.SendMessageAsync("React to me!");
            Global.MessageIdToTrack = msg.Id; 
        }

        // 4.22.2018 Status: Succes
        // Command: Displays all commands
        // R4I: I am not sure
        [Command("Commands")]
        public async Task Commands([Remainder]string arg = "")
        {
            await Context.Channel.SendMessageAsync("!echo = repeats user messages");
            await Context.Channel.SendMessageAsync("!pick = bot randomly selects from list a|b|c|");
            await Context.Channel.SendMessageAsync("!secret = this command can only be used by admin");
            await Context.Channel.SendMessageAsync("!updates = updates from the bot maintence team");
            await Context.Channel.SendMessageAsync("!holiday = the bot celebrates a holiday");
            await Context.Channel.SendMessageAsync("!timeout = this command can only be used by admin");
            await Context.Channel.SendMessageAsync("!roasted = someone just got roasted!");
            await Context.Channel.SendMessageAsync("!doc = list of documented data");
            await Context.Channel.SendMessageAsync("!level = tells the level of the user");
            await Context.Channel.SendMessageAsync("!updates = updates from the bot maintence team");
            await Context.Channel.SendMessageAsync("!inc = increase user level");
            await Context.Channel.SendMessageAsync("!react = have bot react to emoji's");
        }

        // 5.25.2018 Status: Passed
        // Command: Bot maintence
        // R4I: Warn Users
        [Command("Warn")]
        [RequireUserPermission(GuildPermission.Administrator)]

        public async Task WarnUser(IGuildUser user)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserAccounts.SaveAccounts();

            //punishment check
            if (userAccount.NumberOfWarnings >= 3)
            {
                await Context.Channel.SendMessageAsync("GAME OVER");
                var embed = new EmbedBuilder();
                await Context.Channel.SendFileAsync("Memes/GAME OVER.gif");
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Timeout");
                await (user as IGuildUser).AddRoleAsync(role);
            }
            else if(userAccount.NumberOfWarnings == 2)
            {
                await Context.Channel.SendMessageAsync("You know you done fucked up right?");
            }
            else if (userAccount.NumberOfWarnings == 1)
            {
                await Context.Channel.SendMessageAsync($"Congratulations: You got a warning!");
                await Context.Channel.SendMessageAsync("Wooooo");
            }
        }

    }
}
  
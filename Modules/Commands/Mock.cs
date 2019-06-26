using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

using ConsoleApp1.Interfaces.Core;
using ConsoleApp1.Modules.Core;
using DSharpPlus.Entities;

namespace ConsoleApp1.Modules.Commands
{
    public class Mock : BaseCommand
    {
        [Command("mock")]
        public async Task Command(CommandContext ctx, DiscordMember member = null)
        {
            bool missingTargetUser = false;
            
            if (member == null)
            {
                member = ctx.Member;
                missingTargetUser = true;
            }
            
            await ctx.TriggerTypingAsync();

            IReadOnlyList<DiscordMessage> messages = await ctx.Channel.GetMessagesAsync();
            List<DiscordMessage> targetUserMessages = new List<DiscordMessage>();
            foreach(DiscordMessage msg in messages)
            {
                if (msg.Author.Equals(member))
                {
                    targetUserMessages.Add(msg);
                }
            }

            int targetIndex = member.Equals(ctx.Member) ? 1 : 0;
            char[] lastMessageParts = targetUserMessages[targetIndex].Content.ToCharArray();
            for (int i = 0; i < lastMessageParts.Length; i++)
            {
                if (i % 2 == 0)
                {
                    lastMessageParts[i] = char.ToUpper(lastMessageParts[i]);
                }
                else
                {
                    lastMessageParts[i] = char.ToLower(lastMessageParts[i]);
                }
            }
            
            if (missingTargetUser)
            {
                await ctx.RespondAsync($"{member.Mention} you didn't give me a username to mock, you moron");
            }
            
            string newMessage = new string(lastMessageParts);
            await ctx.RespondAsync(newMessage);
        }

        public override void Register()
        {
            Program.CommandsNext.RegisterCommands<Mock>();
            Logger.Log("Set up mock command!", Logger.Level.INFO);
        }
    }
}

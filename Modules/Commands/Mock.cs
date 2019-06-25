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
    public class Mock : ICommand
    {
        [Command("mock")]
        public async Task Command(CommandContext ctx, DiscordMember member = null)
        {
            /*
             * If no member was tagged, we'll set the member variable to the user
             * that invoked the command instead.
             */
            if (member == null)
            {
                member = ctx.Member;
            }
            
            IReadOnlyList<DiscordMessage> messages = await ctx.Channel.GetMessagesAsync();
            List<DiscordMessage> targetUserMessages = new List<DiscordMessage>();
            foreach(DiscordMessage msg in messages)
            {
                if (msg.Author.Equals(member))
                {
                    targetUserMessages.Add(msg);
                }
            }

            await ctx.TriggerTypingAsync();
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
            
            string newMessage = new string(lastMessageParts);
            if (member == ctx.Member)
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} you didn't give a username to mock, you idiot.");
            }
            
            await ctx.RespondAsync(newMessage);
        }

        public void Register()
        {
            Program.CommandsNext.RegisterCommands<Mock>();
            Logger.Log("Set up mock command!", Logger.Level.INFO);
        }
    }
}
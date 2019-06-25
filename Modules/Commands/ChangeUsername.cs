using System;
using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using ConsoleApp1.Modules.Core;
using DSharpPlus.Entities;

namespace ConsoleApp1.Modules.Commands
{
    public class UsernameChange : BaseCommand
    {
        
        /* 
         * Implements the Command() function which is called when the
         * CommandsNext Module triggers it
         */
        [Command("usernamechange")]
        public async Task Command(CommandContext ctx, DiscordMember member = null)
        {
            if (member == null)
            {
                await ctx.RespondAsync(
                    $"{ctx.Member.Mention} you need to give me a username to change, you mouth breather."
                );
                
                return;
            }
            
            try
            {
                await member.ModifyAsync("Replace me with db query pls kek");
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to change username! {ex.Message}", Logger.Level.EXEP);
                await ctx.RespondAsync(
                    $"Oi {ctx.Member.Mention}, {member.Username} is more important than you." +
                    " i'm not changing their username, peasant."
                );

                return;
            }

            await ctx.RespondAsync($"Changed your username {member.Mention}, hope you like it!");
        }

        /* 
         * Registers this class with all its commands for use in the bot
         */
        override public void Register()
        {
            Program.CommandsNext.RegisterCommands<UsernameChange>();
            Logger.Log("Set up username change command!", Logger.Level.INFO);
        }
    }
}
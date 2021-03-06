﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Core.Providers;
using Miunie.Discord.Embeds;

namespace Miunie.Discord.Adapters
{
    public class DiscordMessagesAdapter : IDiscordMessages
    {
        private readonly IDiscord _discord;
        private readonly ILanguageProvider _lang;

        public DiscordMessagesAdapter(IDiscord discord, ILanguageProvider lang)
        {
            _discord = discord;
            _lang = lang;
        }

        public async Task SendMessageAsync(MiunieChannel mc, PhraseKey phraseKey, params object[] parameters)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            var msg = _lang.GetPhrase(phraseKey.ToString(), parameters);
            await channel.SendMessageAsync(msg);
        }

        public async Task SendMessageAsync(MiunieChannel mc, MiunieUser mu)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            await channel.SendMessageAsync(embed: mu.ToEmbed(_lang));
        }

        public async Task SendMessageAsync(MiunieChannel mc, MiunieGuild mg)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            await channel.SendMessageAsync(embed: mg.ToEmbed(_lang));
        }

        public async Task SendMessageAsync(MiunieChannel mc, DirectoryListing dl)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            var result = string.Join("\n", dl.Result.Select(s => $":file_folder: {s}"));
            await channel.SendMessageAsync(result);
        }

        public async Task SendMessageAsync(MiunieChannel mc, IEnumerable<CurrencyData> tc)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            var result = string.Join("\n", tc.Select(c => $"{c.Amount} {c.Code} = {c.CzechCrowns} CZK"));
            await channel.SendMessageAsync(result);
        }

        public async Task SendMessageAsync(MiunieChannel mc, CurrencyConversionResult ccr)
        {
            var channel = _discord.Client.GetChannel(mc.ChannelId) as SocketTextChannel;
            var result = $"{ccr.FromValue} {ccr.FromCode} = {ccr.ToValue} {ccr.ToCode}";
            await channel.SendMessageAsync(result);
        }
    }
}

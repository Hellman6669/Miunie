﻿using Discord;
using Miunie.Core;
using Miunie.Core.Providers;
using System;
using System.Linq;

namespace Miunie.Discord.Embeds
{
    internal static class EmbedConstructor
    {
        public static Embed ToEmbed(this MiunieUser mUser, ILanguageProvider lang)
        {
            var realnessPhrase = lang.GetPhrase((mUser.IsBot ? PhraseKey.USER_EMBED_IS_BOT : PhraseKey.USER_EMBED_IS_HUMAN).ToString());

            return new EmbedBuilder()
                .WithColor(new Color(236, 64, 122))
                .WithTitle(lang.GetPhrase(PhraseKey.USER_EMBED_TITLE.ToString()))
                .WithThumbnailUrl(mUser.AvatarUrl)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_NAME_TITLE.ToString()), mUser.Name)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_REALNESS_TITLE.ToString()), realnessPhrase, true)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_REP_TITLE.ToString()), mUser.Reputation.Value.ToString(), true)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_ROLES_TITLE.ToString()), string.Join("\n", mUser.Roles.Select(r => r.Name)), true)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_JOINED_AT_TITLE.ToString()), $"{mUser.JoinedAt:d} at {mUser.JoinedAt:t} UTC")
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_CREATED_AT_TITLE.ToString()), $"{mUser.CreatedAt:d} at {mUser.CreatedAt:t} UTC", true)
                .AddField(lang.GetPhrase(PhraseKey.USER_EMBED_TIME_TITLE.ToString()), mUser.UtcTimeOffset.HasValue ? lang.GetPhrase(PhraseKey.USER_EMBED_TIME.ToString(), DateTime.UtcNow + mUser.UtcTimeOffset) : lang.GetPhrase(PhraseKey.USER_EMBED_TIME_NOSET.ToString()), true)
                .Build();
        }

        public static Embed ToEmbed(this MiunieGuild mGuild, ILanguageProvider lang)
            => new EmbedBuilder()
                .WithColor(new Color(236, 64, 122))
                .WithTitle(lang.GetPhrase(PhraseKey.GUILD_EMBED_TITLE.ToString()))
                .AddField(lang.GetPhrase(PhraseKey.GUILD_EMBED_NAME_TITLE.ToString()), mGuild.Name)
                .AddField(lang.GetPhrase(PhraseKey.GUILD_EMBED_STATS_TITLE.ToString()), mGuild.GetStats(), true)
                .AddField(lang.GetPhrase(PhraseKey.GUILD_EMBED_ROLES_TITLE.ToString()), string.Join(", ", mGuild.Roles.Select(r => r.Name.Replace("@", ""))), true)
                .AddField(lang.GetPhrase(PhraseKey.GUILD_EMBED_CREATED_AT_TITLE.ToString()), $"{mGuild.CreationDate:d} at {mGuild.CreationDate:t} UTC")
                .Build();
    }
}

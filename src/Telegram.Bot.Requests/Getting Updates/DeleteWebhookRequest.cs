﻿// ReSharper disable once CheckNamespace

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Remove webhook integration if you decide to switch back to getUpdates.
    /// </summary>
    //[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed record DeleteWebhookRequest : ParameterlessRequest<bool>
    {
        /// <summary>
        /// Initializes a new request
        /// </summary>
        public DeleteWebhookRequest()
            : base("deleteWebhook")
        { }
    }
}

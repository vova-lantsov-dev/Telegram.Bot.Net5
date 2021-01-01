using System;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Contains information about the current status of a webhook.
    /// </summary>
    //[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed record WebhookInfo : IAutoGeneratedWebhookInfo
    {
        /// <summary>
        /// Webhook URL, may be empty if webhook is not set up
        /// </summary>
        //[JsonProperty(Required = Required.Always)]
        public string Url { get; init; }

        /// <summary>
        /// True, if a custom certificate was provided for webhook certificate checks
        /// </summary>
        //[JsonProperty(Required = Required.Always)]
        public bool HasCustomCertificate { get; init; }

        /// <summary>
        /// Number of updates awaiting delivery
        /// </summary>
        //[JsonProperty(Required = Required.Always)]
        public int PendingUpdateCount { get; init; }

        /// <summary>
        /// Unix time for the most recent error that happened when trying to deliver an update via webhook
        /// </summary>
        //[JsonConverter(typeof(UnixDateTimeConverter))]
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime LastErrorDate { get; init; }

        /// <summary>
        /// Error message in human-readable format for the most recent error that happened when trying to deliver an update via webhook
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LastErrorMessage { get; init; }

        /// <summary>
        /// Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MaxConnections { get; init; }

        /// <summary>
        /// A list of update types the bot is subscribed to. Defaults to all update types
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public UpdateType[] AllowedUpdates { get; set; }
    }
}
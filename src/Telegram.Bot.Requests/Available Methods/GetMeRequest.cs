﻿using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// A simple method for testing your bot's auth token.
    /// </summary>
    //[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed record GetMeRequest : ParameterlessRequest<User>,
        IAutoGeneratedGetMeRequest
    {
        /// <summary>
        /// Initializes a new request
        /// </summary>
        public GetMeRequest()
            : base("getMe")
        { }
    }
}



// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents an animation file (GIF or H.264/MPEG-4 AVC video without sound) to be sent
    /// </summary>
    //[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public sealed record InputMediaAnimation : InputMediaBase, IInputMediaThumb,
        IAutoGeneratedInputMediaAnimation
    {
        /// <summary>
        /// Optional. Animation width
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Width { get; init; }

        /// <summary>
        /// Optional. Animation height
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Height { get; init; }

        /// <summary>
        /// Optional. Animation duration
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Duration { get; init; }

        /// <inheritdoc />
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMedia Thumb { get; init; }

        /// <summary>
        /// Initializes a new animation media to send with an <see cref="InputMedia"/>
        /// </summary>
        public InputMediaAnimation(InputMedia media)
        {
            Type = "animation";
            Media = media;
        }
    }
}

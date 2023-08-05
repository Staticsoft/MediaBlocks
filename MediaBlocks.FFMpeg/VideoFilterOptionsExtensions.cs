using FFMpegCore.Arguments;
using System;

namespace Staticsoft.MediaBlocks.FFMpeg;

public static class VideoFilterOptionsExtensions
{
    public static VideoFilterOptions FadeIn(this VideoFilterOptions options, int duration)
        => options.Fade(0, duration, "in");

    public static VideoFilterOptions FadeOut(this VideoFilterOptions options, int startTime, int duration)
        => options.Fade(startTime, duration, "out");

    static VideoFilterOptions Fade(this VideoFilterOptions options, int startTime, int duration, string direction)
    {
        options.Arguments.Add(new FadeArgument(startTime, duration, direction));
        return options;
    }

    public class FadeArgument : IVideoFilterArgument
    {
        readonly int Duration;
        readonly int StartFrom;
        readonly string Direction;

        public FadeArgument(int startFrom, int duration, string direction)
            => (StartFrom, Duration, Direction)
            = (startFrom, duration, direction);

        public string Key
            => "fade";

        public string Value
            => $"type={Direction}:start_time={StartTime:0.###}:duration={Seconds:0.###}";

        float StartTime
            => Convert.ToSingle(StartFrom) / 1000;

        float Seconds
            => Convert.ToSingle(Duration) / 1000;
    }
}
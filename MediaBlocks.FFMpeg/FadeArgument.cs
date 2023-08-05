using FFMpegCore.Arguments;
using System;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class FadeArgument : IVideoFilterArgument, IAudioFilterArgument
{
    readonly int Duration;
    readonly int StartFrom;
    readonly string Direction;

    public FadeArgument(int startFrom, int duration, string fadeFilter, string direction)
        => (StartFrom, Duration, Key, Direction)
        = (startFrom, duration, fadeFilter, direction);

    public string Key { get; }

    public string Value
        => $"type={Direction}:start_time={StartTime:0.###}:duration={Seconds:0.###}";

    float StartTime
        => Convert.ToSingle(StartFrom) / 1000;

    float Seconds
        => Convert.ToSingle(Duration) / 1000;
}
using FFMpegCore.Arguments;

namespace Staticsoft.MediaBlocks.FFMpeg;

public static class VideoFilterOptionsExtensions
{
    public static VideoFilterOptions FadeIn(this VideoFilterOptions options, int duration)
        => options.Fade(0, duration, "in");

    public static VideoFilterOptions FadeOut(this VideoFilterOptions options, int startTime, int duration)
        => options.Fade(startTime, duration, "out");

    static VideoFilterOptions Fade(this VideoFilterOptions options, int startTime, int duration, string direction)
    {
        options.Arguments.Add(new FadeArgument(startTime, duration, "fade", direction));
        return options;
    }
}

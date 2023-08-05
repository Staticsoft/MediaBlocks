using FFMpegCore.Arguments;

namespace Staticsoft.MediaBlocks.FFMpeg;

public static class AudioFilterOptionsExtensions
{
    public static AudioFilterOptions FadeIn(this AudioFilterOptions options, int duration)
        => options.Fade(0, duration, "in");

    public static AudioFilterOptions FadeOut(this AudioFilterOptions options, int startTime, int duration)
        => options.Fade(startTime, duration, "out");

    static AudioFilterOptions Fade(this AudioFilterOptions options, int startTime, int duration, string direction)
    {
        options.Arguments.Add(new FadeArgument(startTime, duration, "afade", direction));
        return options;
    }
}

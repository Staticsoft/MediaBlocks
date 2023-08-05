using Staticsoft.MediaBlocks.Abstractions;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class FadeProperties
{
    public MediaReference Media { get; init; } = new();
    public int Duration { get; init; } = 0;
}

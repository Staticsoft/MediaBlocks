using FFMpegCore;
using FFMpegCore.Arguments;
using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.TreeOperations.Abstractions;
using System;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class FadeInOperation : Operation<FadeInProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public FadeInOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(FadeInProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";
        await FFMpegArguments
            .FromFileInput(properties.Media.Path)
            .OutputToFile(output, overwrite: false, (options) => options
                .WithVideoFilters(filters => filters.FadeIn(properties.Duration))
            )
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output,
            Type = MediaType.Video
        };
    }
}

public class FadeInProperties
{
    public MediaReference Media { get; init; } = new();
    public int Duration { get; init; } = 0;
}

public static partial class VideoFilterOptionsExtensions
{
    public static VideoFilterOptions FadeIn(this VideoFilterOptions options, int duration)
    {
        options.Arguments.Add(new FadeInArgument(duration));
        return options;
    }

    public class FadeInArgument : IVideoFilterArgument
    {
        readonly int Duration;

        public FadeInArgument(int duration)
            => Duration = duration;

        public string Key
            => "fade";

        public string Value
            => $"type=in:start_frame=0:duration={Seconds:0.###}";

        float Seconds
            => Convert.ToSingle(Duration) / 1000;
    }
}
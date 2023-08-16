using FFMpegCore;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class FadeOutOperation : Operation<FadeProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public FadeOutOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(FadeProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";
        var analysed = await FFProbe.AnalyseAsync(properties.Media.Path);
        var startTime = EvaluateStartTime(properties, analysed.Duration);

        await FFMpegArguments
            .FromFileInput(properties.Media.Path)
            .OutputToFile(output, overwrite: false, (options) => options
                .WithVideoFilters(filters => filters.FadeOut(startTime, properties.Duration))
                .WithAudioFilters(filters => filters.FadeOut(startTime, properties.Duration))
            )
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output,
            Type = MediaType.Video
        };
    }

    static int EvaluateStartTime(FadeProperties properties, TimeSpan duration)
        => Convert.ToInt32((duration - TimeSpan.FromMilliseconds(properties.Duration)).TotalMilliseconds);
}

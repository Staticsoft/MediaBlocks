using FFMpegCore;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class FadeInOperation : Operation<FadeProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public FadeInOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(FadeProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";
        await FFMpegArguments
            .FromFileInput(properties.Video)
            .OutputToFile(output, overwrite: false, (options) => options
                .WithVideoFilters(filters => filters.FadeIn(properties.Duration))
                .WithAudioFilters(filters => filters.FadeIn(properties.Duration))
            )
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output,
            Type = MediaType.Video
        };
    }
}

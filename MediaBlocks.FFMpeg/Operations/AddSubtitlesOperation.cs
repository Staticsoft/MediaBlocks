using FFMpegCore;
using FFMpegCore.Arguments;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class AddSubtitlesOperation : Operation<AddSubtitlesOperationProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public AddSubtitlesOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(AddSubtitlesOperationProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";
        await FFMpegArguments
            .FromFileInput(properties.Video)
            .OutputToFile(output, overwrite: false, (options) => options
                .WithVideoFilters(filters => filters.HardBurnSubtitle(SubtitleHardBurnOptions.Create(properties.Subtitles)))
            )
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output
        };
    }
}

public class AddSubtitlesOperationProperties
{
    public string Subtitles { get; init; } = string.Empty;
    public string Video { get; init; } = string.Empty;
}

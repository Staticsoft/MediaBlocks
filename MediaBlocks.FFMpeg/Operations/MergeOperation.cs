using FFMpegCore;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class MergeOperation : Operation<MergeOperationProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public MergeOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(MergeOperationProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";

        var analysedAudio = await FFProbe.AnalyseAsync(properties.Audio);

        await FFMpegArguments
            .FromFileInput(properties.Image, verifyExists: false, (options) => options
                .Loop(1)
                .WithDuration(analysedAudio.Duration)
            )
            .AddFileInput(properties.Audio)
            .OutputToFile(output)
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output,
            Type = MediaType.Video
        };
    }
}

public class MergeOperationProperties
{
    public string Image { get; init; } = string.Empty;
    public string Audio { get; init; } = string.Empty;
}
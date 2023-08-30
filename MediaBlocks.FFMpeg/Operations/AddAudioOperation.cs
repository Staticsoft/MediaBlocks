using FFMpegCore;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class AddAudioOperation : Operation<AddAudioOperationProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public AddAudioOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(AddAudioOperationProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";

        await FFMpegArguments
            .FromFileInput(properties.Video)
            .AddFileInput(properties.Audio)
            .OutputToFile(output)
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output
        };
    }
}

public class AddAudioOperationProperties
{
    public string Video { get; init; } = string.Empty;
    public string Audio { get; init; } = string.Empty;
}

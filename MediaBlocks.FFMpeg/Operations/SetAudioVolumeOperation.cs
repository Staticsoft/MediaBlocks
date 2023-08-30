using FFMpegCore;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class SetAudioVolumeOperation : Operation<SetAudioVolumeOperationProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public SetAudioVolumeOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(SetAudioVolumeOperationProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp3";

        await FFMpegArguments
            .FromFileInput(properties.Audio)
            .OutputToFile(output, addArguments: options => options
                .WithAudioFilters(filters => filters.Volume(properties.Volume))
            )
            .ProcessAsynchronously();

        return new() { Path = output };
    }
}

public class SetAudioVolumeOperationProperties
{
    public string Audio { get; init; } = string.Empty;
    public int Volume { get; init; }
}
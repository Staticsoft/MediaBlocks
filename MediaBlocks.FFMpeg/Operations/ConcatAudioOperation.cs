using FFMpegCore;
using FFMpegCore.Enums;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class ConcatAudioOperation : Operation<string[], MediaReference>
{
    readonly IntermediateStorage Storage;

    public ConcatAudioOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(string[] files)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp3";
        await FFMpegArguments
            .FromConcatInput(files)
            .OutputToFile(output, overwrite: false, options => options.WithAudioBitrate(AudioQuality.Good))
            .ProcessAsynchronously();
        return new MediaReference
        {
            Path = output
        };
    }
}

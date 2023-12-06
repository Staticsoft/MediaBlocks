using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class ConcatVideoOperation : Operation<string[], MediaReference>
{
    readonly IntermediateStorage Storage;

    public ConcatVideoOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override Task<MediaReference> Process(string[] files)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";
        FFMpegCore.FFMpeg.Join(output, files);
        return Task.FromResult(new MediaReference()
        {
            Path = output
        });
    }
}

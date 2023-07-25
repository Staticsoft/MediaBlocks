using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.TreeOperations.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class ConcatOperation : Operation<MediaReference[], MediaReference>
{
    readonly IntermediateStorage Storage;

    public ConcatOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override Task<MediaReference> Process(MediaReference[] data)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";
        FFMpegCore.FFMpeg.Join(output, data.Select(file => file.Path).ToArray());
        return Task.FromResult(new MediaReference()
        {
            Path = output,
            Type = MediaType.Video
        });
    }
}

using FFMpegCore;
using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.TreeOperations.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class MergeOperation : Operation<MediaReference[], MediaReference>
{
    readonly IntermediateStorage Storage;

    public MergeOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(MediaReference[] data)
    {
        var image = data.Single(file => file.Type == MediaType.Image);
        var audio = data.Single(file => file.Type == MediaType.Audio);
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";

        var analysedAudio = await FFProbe.AnalyseAsync(audio.Path);

        await FFMpegArguments
            .FromFileInput(image.Path, verifyExists: false, (options) => options
                .Loop(1)
                .WithDuration(analysedAudio.Duration)
            )
            .AddFileInput(audio.Path)
            .OutputToFile(output)
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output,
            Type = MediaType.Video
        };
    }
}
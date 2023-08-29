using FFMpegCore;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class ImageVideoOperation : Operation<ImageVideoOperationProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public ImageVideoOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(ImageVideoOperationProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";

        await FFMpegArguments
            .FromFileInput(properties.Image, verifyExists: false, (options) => options
                .Loop(1)
                .WithDuration(TimeSpan.FromMilliseconds(properties.Duration))
            )
            .OutputToFile(output)
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output
        };
    }
}

public class ImageVideoOperationProperties
{
    public string Image { get; init; } = string.Empty;
    public int Duration { get; init; }
}
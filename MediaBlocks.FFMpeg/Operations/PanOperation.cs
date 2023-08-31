using FFMpegCore;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class PanOperation : Operation<PanOperationProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public PanOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(PanOperationProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.mp4";
        var analyzed = await FFProbe.AnalyseAsync(properties.Video);
        await FFMpegArguments
            .FromFileInput(properties.Video)
            .OutputToFile(output, overwrite: false, (options) => options
                .WithVideoFilters(filters => filters.Pan(properties.Direction, analyzed.PrimaryVideoStream!))
            )
            .ProcessAsynchronously();

        return new MediaReference()
        {
            Path = output
        };
    }
}

public class PanOperationProperties
{
    public string Video { get; init; } = string.Empty;
    public PanDirection Direction { get; init; }
}

public enum PanDirection
{
    Unknown,
    LeftToRight,
    RightToLeft
}
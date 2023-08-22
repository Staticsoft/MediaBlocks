using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class AssetOperation : Operation<AssetProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public AssetOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override Task<MediaReference> Process(AssetProperties properties)
    {
        var extension = properties.Path.Split('.').Last();
        var type = GetMediaReferenceType(extension);
        var output = $"{Storage.CreateIntermediateFilePath()}.{extension}";
        File.Copy(properties.Path, output, overwrite: true);
        return Task.FromResult(new MediaReference()
        {
            Path = output,
            Type = type
        });
    }

    static MediaType GetMediaReferenceType(string extension) => extension switch
    {
        "mp3" => MediaType.Audio,
        "png" => MediaType.Image,
        _ => throw new NotSupportedException($"Unsupported file extension '.{extension}'")
    };
}

public class AssetProperties
{
    public string Path { get; init; } = string.Empty;
}
using FFMpegCore;
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

    protected override async Task<MediaReference> Process(AssetProperties properties)
    {
        var extension = properties.Path.Split('.').Last();
        var type = GetMediaReferenceType(extension);
        var output = $"{Storage.CreateIntermediateFilePath()}.{extension}";
        File.Copy(properties.Path, output, overwrite: true);
        if (type == MediaType.Audio)
        {
            var analyzedAudio = await FFProbe.AnalyseAsync(properties.Path);
            return new AudioReference
            {
                Path = output,
                Duration = Convert.ToInt32(analyzedAudio.Duration.TotalMilliseconds)
            };
        }
        else if (type == MediaType.Text)
        {
            var text = await File.ReadAllTextAsync(properties.Path);
            return new TextReference
            {
                Path = output,
                Text = text
            };
        }
        return new MediaReference
        {
            Path = output
        };
    }

    static MediaType GetMediaReferenceType(string extension) => extension switch
    {
        "mp3" => MediaType.Audio,
        "png" => MediaType.Image,
        "txt" => MediaType.Text,
        _ => throw new NotSupportedException($"Unsupported file extension '.{extension}'")
    };
}

public class AssetProperties
{
    public string Path { get; init; } = string.Empty;
}
namespace Staticsoft.MediaBlocks.Abstractions;

public class MediaReference
{
    public string Path { get; set; } = string.Empty;
    public MediaType Type { get; set; } = MediaType.Unknown;
}

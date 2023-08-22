using Staticsoft.GraphOperations.Abstractions;

namespace Staticsoft.MediaBlocks.Abstractions;

public class MediaReference : OperationResult
{
    public string Path { get; set; } = string.Empty;
    public MediaType Type { get; set; } = MediaType.Unknown;

    public object RefAttribute
        => Path;
}

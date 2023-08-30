using Staticsoft.GraphOperations.Abstractions;

namespace Staticsoft.MediaBlocks.Abstractions;

public class MediaReference : OperationResult
{
    public string Path { get; init; } = string.Empty;

    public virtual object RefAttribute
        => Path;
}

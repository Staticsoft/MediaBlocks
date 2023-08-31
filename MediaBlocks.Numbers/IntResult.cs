using Staticsoft.GraphOperations.Abstractions;

namespace Staticsoft.MediaBlocks.Numbers;

public class IntResult : OperationResult
{
    public int Value { get; set; }

    public object RefAttribute
        => Value;
}

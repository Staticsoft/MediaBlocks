using Staticsoft.GraphOperations.Abstractions;

namespace Staticsoft.MediaBlocks.Numbers;

public class SumOperation : Operation<int[], IntResult>
{
    protected override Task<IntResult> Process(int[] numbers)
        => Task.FromResult(Sum(numbers));

    static IntResult Sum(int[] numbers)
        => new() { Value = numbers.Sum() };
}

using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using Staticsoft.MediaBlocks.Numbers;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests.Operations;

public class SumOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<ImageVideoOperation>()
        .With<SumOperation>();

    [Test]
    public Task CreatesVideoUsingDurationAsSumResult()
        => Process(new
        {
            Image = Image("green.png"),
            Sum = new
            {
                Type = "Sum",
                Properties = new[] { 500, 1000, 1500 }
            },
            ImageVideo = new
            {
                Type = "ImageVideo",
                Properties = new
                {
                    Image = new { Ref = "Image" },
                    Duration = new { Ref = "Sum" }
                }
            }
        });
}

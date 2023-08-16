using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class ConcatOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<ConcatOperation>()
        .With<MergeOperation>();

    [Test]
    public Task ConcatenatesTwoVideosIntoOne()
        => Process(
            Concat(
                Merge(
                    Image("green.png"),
                    Audio("beep.mp3")
                ),
                Merge(
                    Image("red.png"),
                    Audio("boops.mp3")
                )
            )
        );
}

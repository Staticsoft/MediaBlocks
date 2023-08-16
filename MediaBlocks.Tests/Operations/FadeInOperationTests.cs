using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class FadeInOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<MergeOperation>()
        .With<FadeInOperation>();

    [Test]
    public Task FadesInVideo()
        => Process(
            FadeIn(
                Merge(
                    Image("green.png"),
                    Audio("beep.mp3")
                ),
                250
            )
        );
}

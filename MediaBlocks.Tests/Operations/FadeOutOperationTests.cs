using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class FadeOutOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<MergeOperation>()
        .With<FadeOutOperation>();

    [Test]
    public Task FadesOutVideo()
        => Process(
            FadeOut(
                Merge(
                    Image("green.png"),
                    Audio("beep.mp3")
                ),
                250
            )
        );
}

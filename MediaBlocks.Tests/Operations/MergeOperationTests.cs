using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class MergeOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<MergeOperation>();

    [Test]
    public Task MergesImageAndAudioIntoVideoFile()
        => Process(
            Merge(
                Audio("beep.mp3"),
                Image("green.png")
            )
        );
}

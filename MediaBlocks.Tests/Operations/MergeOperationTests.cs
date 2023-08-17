using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class MergeOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<MergeOperation>();

    [Test]
    public Task MergesImageAndAudioIntoVideoFile()
        => Process(new
        {
            Audio = Audio("beep.mp3"),
            Image = Image("green.png"),
            Merged = Merge("Audio", "Image")
        });
}

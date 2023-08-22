using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class FadeOutOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<MergeOperation>()
        .With<FadeOutOperation>();

    [Test]
    public Task FadesOutVideo()
        => Process(new
        {
            Image = Image("green.png"),
            Audio = Audio("beep.mp3"),
            Merged = Merge("Image", "Audio"),
            FadedOut = FadeOut("Merged", 250)
        });
}

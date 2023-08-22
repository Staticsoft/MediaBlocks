using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class FadeInOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<MergeOperation>()
        .With<FadeInOperation>();

    [Test]
    public Task FadesInVideo()
        => Process(new
        {
            Image = Image("green.png"),
            Audio = Audio("beep.mp3"),
            Merged = Merge("Image", "Audio"),
            FadedIn = FadeIn("Merged", 250)
        });
}

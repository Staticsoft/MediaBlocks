using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class ConcatOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<ConcatOperation>()
        .With<MergeOperation>();

    [Test]
    public Task ConcatenatesTwoVideosIntoOne()
        => Process(new
        {
            GreenImage = Image("green.png"),
            RedImage = Image("red.png"),
            BeepAudio = Audio("beep.mp3"),
            BoopsAudio = Audio("boops.mp3"),
            GreenBeepVideo = Merge("GreenImage", "BeepAudio"),
            RedBoopsVideo = Merge("RedImage", "BoopsAudio"),
            Concatenated = Concat("GreenBeepVideo", "RedBoopsVideo")
        });
}

using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class ConcatAudioOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<ConcatAudioOperation>();

    [Test]
    public Task ConcatenatesTwoAudiosIntoOne()
        => Process(new
        {
            BeepAudio = Audio("beep.mp3"),
            BoopsAudio = Audio("boops.mp3"),
            Concatenated = ConcatAudio("BeepAudio", "BoopsAudio")
        });
}

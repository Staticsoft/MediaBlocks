using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class SetAudioVolumeOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<SetAudioVolumeOperation>();

    [Test]
    public Task ChangesVolumeToSpecified()
        => Process(new
        {
            Audio = Audio("beep.mp3"),
            HalvedAudio = new
            {
                Type = "SetAudioVolume",
                Properties = new
                {
                    Audio = new { Ref = "Audio" },
                    Volume = 50
                }
            }
        });
}
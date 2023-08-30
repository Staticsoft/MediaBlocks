using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class AddAudioOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<ImageVideoOperation>()
        .With<AddAudioOperation>();

    [Test]
    public Task AddsAudioToVideo()
        => Process(new
        {
            Audio = Audio("beep.mp3"),
            Image = Image("green.png"),
            Video = new
            {
                Type = "ImageVideo",
                Properties = new
                {
                    Image = new { Ref = "Image" },
                    Duration = new
                    {
                        GetAtt = new[]
                        {
                            "Audio",
                            "Duration"
                        }
                    }
                }
            },
            VideoWithAudio = new
            {
                Type = "AddAudio",
                Properties = new
                {
                    Audio = new { Ref = "Audio" },
                    Video = new { Ref = "Video" }
                }
            }
        });
}

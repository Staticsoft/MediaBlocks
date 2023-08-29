using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class ImageVideoOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<ImageVideoOperation>();

    [Test]
    public Task CreatesVideoFromImage()
        => Process(new
        {
            Audio = Audio("beep.mp3"),
            Image = Image("green.png"),
            ImageVideo = new
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
            }
        });
}
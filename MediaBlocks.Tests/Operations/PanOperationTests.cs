using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class PanOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<ImageVideoOperation>()
        .With<PanOperation>();

    [Test]
    public Task AppliesPanFilterLeftToRight()
        => Process(new
        {
            Image = Image("eye.png"),
            Video = new
            {
                Type = "ImageVideo",
                Properties = new
                {
                    Image = new { Ref = "Image" },
                    Duration = 1234
                }
            },
            Panned = new
            {
                Type = "Pan",
                Properties = new
                {
                    Video = new { Ref = "Video" },
                    Direction = "LeftToRight"
                }
            }
        });

    [Test]
    public Task AppliesPanFilterRightToLeft()
        => Process(new
        {
            Image = Image("eye.png"),
            Video = new
            {
                Type = "ImageVideo",
                Properties = new
                {
                    Image = new { Ref = "Image" },
                    Duration = 1234
                }
            },
            Panned = new
            {
                Type = "Pan",
                Properties = new
                {
                    Video = new { Ref = "Video" },
                    Direction = "RightToLeft"
                }
            }
        });
}

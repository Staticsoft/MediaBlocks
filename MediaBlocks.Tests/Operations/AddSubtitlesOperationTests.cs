using MediaBlocks.Subtitles;
using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class AddSubtitlesOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<ImageVideoOperation>()
        .With<SubtitlesOperation>()
        .With<AddSubtitlesOperation>();

    [Test]
    public Task BurnsSubtitlesIntoVideo()
        => Process(new
        {
            Image = Image("green.png"),
            Video = new
            {
                Type = "ImageVideo",
                Properties = new
                {
                    Image = new { Ref = "Image" },
                    Duration = 1000
                }
            },
            Subtitles = new
            {
                Type = "Subtitles",
                Properties = new
                {
                    Text = "Subtitles in the video",
                    Duration = 1000
                }
            },
            VideoWithSubtitles = new
            {
                Type = "AddSubtitles",
                Properties = new
                {
                    Subtitles = new { Ref = "Subtitles" },
                    Video = new { Ref = "Video" }
                }
            }
        });
}

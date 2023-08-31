using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
using Staticsoft.MediaBlocks.Subtitles;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class SubtitlesOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<SubtitlesOperation>();

    [Test]
    public Task GeneratesAdvancedSubStationAlphaFromText()
        => Process(new
        {
            Subtitles = new
            {
                Type = "Subtitles",
                Properties = new
                {
                    Text = "Hello, World!",
                    Duration = 1000
                }
            }
        });
}

public class ConcatSubtitlesOperationTests : OperationTest
{
    protected override GraphProcessorBuilder Graph(GraphProcessorBuilder graph) => graph
        .With<AssetOperation>()
        .With<SubtitlesOperation>()
        .With<ConcatSubtitlesOperation>();

    [Test]
    public Task ConcatenatesTwoSubtitlesIntoOne()
        => Process(new
        {
            Subtitles1 = new
            {
                Type = "Subtitles",
                Properties = new
                {
                    Text = "Text1",
                    Duration = 1230
                }
            },
            Subtitles2 = new
            {
                Type = "Subtitles",
                Properties = new
                {
                    Text = "Text2",
                    Duration = 2340
                }
            },
            Merged = new
            {
                Type = "ConcatSubtitles",
                Properties = new[]
                {
                    new { Ref = "Subtitles1" },
                    new { Ref = "Subtitles2" }
                }
            }
        });
}
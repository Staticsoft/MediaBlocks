using MediaBlocks.Subtitles;
using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.FFMpeg;
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
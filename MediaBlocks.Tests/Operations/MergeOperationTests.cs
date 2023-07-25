using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.MediaBlocks.FFMpeg;
using Staticsoft.TreeOperations.Memory;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class MergeOperationTests : OperationTest
{
    protected override TreeProcessorBuilder<MediaReference> Tree(TreeProcessorBuilder<MediaReference> tree) => tree
        .With<MergeOperation>();

    [Test]
    public Task MergesImageAndAudioIntoVideoFile()
        => Process(
            Merge(
                Audio("beep.mp3"),
                Image("green.png")
            )
        );
}

using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.MediaBlocks.FFMpeg;
using Staticsoft.TreeOperations.Memory;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class FadeInOperationTests : OperationTest
{
    protected override TreeProcessorBuilder<MediaReference> Tree(TreeProcessorBuilder<MediaReference> tree) => tree
        .With<MergeOperation>()
        .With<FadeInOperation>();

    [Test]
    public Task FadesInVideo()
        => Process(
            FadeIn(
                Merge(
                    Image("green.png"),
                    Audio("beep.mp3")
                ),
                250
            )
        );
}

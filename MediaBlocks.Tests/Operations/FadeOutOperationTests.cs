using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.MediaBlocks.FFMpeg;
using Staticsoft.TreeOperations.Memory;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class FadeOutOperationTests : OperationTest
{
    protected override TreeProcessorBuilder<MediaReference> Tree(TreeProcessorBuilder<MediaReference> tree) => tree
        .With<MergeOperation>()
        .With<FadeOutOperation>();

    [Test]
    public Task FadesOutVideo()
        => Process(
            FadeOut(
                Merge(
                    Image("green.png"),
                    Audio("beep.mp3")
                ),
                250
            )
        );
}

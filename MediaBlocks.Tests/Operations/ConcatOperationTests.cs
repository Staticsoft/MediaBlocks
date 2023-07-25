using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.MediaBlocks.FFMpeg;
using Staticsoft.TreeOperations.Memory;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public class ConcatOperationTests : OperationTest
{
    protected override TreeProcessorBuilder<MediaReference> Tree(TreeProcessorBuilder<MediaReference> tree) => tree
        .With<ConcatOperation>()
        .With<MergeOperation>();

    [Test]
    public Task ConcatenatesTwoVideosIntoOne()
        => Process(
            Concat(
                Merge(
                    Image("green.png"),
                    Audio("beep.mp3")
                ),
                Merge(
                    Image("red.png"),
                    Audio("boops.mp3")
                )
            )
        );
}

using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.TreeOperations.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Memory;

public class TreeMedia : Media
{
    readonly TreeProcessor<MediaReference> Tree;

    public TreeMedia(TreeProcessor<MediaReference> tree)
        => Tree = tree;

    public Task<MediaReference> Build(string configuration)
        => Tree.Process(configuration);
}

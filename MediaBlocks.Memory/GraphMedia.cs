using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Memory;

public class GraphMedia : Media
{
    readonly GraphProcessor Graph;

    public GraphMedia(GraphProcessor graph)
        => Graph = graph;

    public async Task<MediaReference> Build(string configuration)
    {
        var processed = await Graph.Process(configuration);
        return (MediaReference)processed;
    }
}

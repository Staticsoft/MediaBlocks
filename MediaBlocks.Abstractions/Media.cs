using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Abstractions;

public interface Media
{
    Task<MediaReference> Build(string configuration);
}

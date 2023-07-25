using Staticsoft.MediaBlocks.Abstractions;
using System.IO;

namespace Staticsoft.MediaBlocks.Memory;

public abstract class IntermediateStorageBase : IntermediateStorage
{
    public abstract string BasePath { get; }

    public void Dispose()
    {
        if (Directory.Exists(BasePath))
        {
            Directory.Delete(BasePath, true);
        }
    }
}

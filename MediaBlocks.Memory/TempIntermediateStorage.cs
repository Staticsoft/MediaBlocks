using System.IO;

namespace Staticsoft.MediaBlocks.Memory;

public class TempIntermediateStorage : IntermediateStorageBase
{
    public override string BasePath
        => Path.Combine(Path.GetTempPath(), nameof(TempIntermediateStorage));
}
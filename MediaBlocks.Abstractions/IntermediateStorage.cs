using System;
using System.IO;

namespace Staticsoft.MediaBlocks.Abstractions;

public interface IntermediateStorage : IDisposable
{
    string BasePath { get; }
    string CreateIntermediateFilePath()
    {
        if (!Directory.Exists(BasePath)) Directory.CreateDirectory(BasePath);

        return Path.Combine(BasePath, $"{Guid.NewGuid()}");
    }
}
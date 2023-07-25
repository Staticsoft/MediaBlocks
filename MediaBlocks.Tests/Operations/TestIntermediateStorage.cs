using Staticsoft.MediaBlocks.Abstractions;
using System.IO;
using System.Threading;

namespace Staticsoft.MediaBlocks.Tests;

public class TestIntermediateStorage : IntermediateStorage
{
    int Counter = 0;
    string Test = string.Empty;

    public string BasePath
        => Path.Combine(nameof(IntermediateStorage), Test);

    public string CreateIntermediateFilePath()
    {
        if (!Directory.Exists(BasePath)) Directory.CreateDirectory(BasePath);

        return Path.Combine(BasePath, $"{Interlocked.Increment(ref Counter)}");
    }

    public void PrepareTestStorage(string testName)
    {
        Test = testName;
        ClearDirectory();
    }

    void ClearDirectory()
    {
        if (Directory.Exists(BasePath))
        {
            foreach (var file in Directory.EnumerateFiles(BasePath))
            {
                File.Delete(file);
            }
        }
    }

    public void Dispose() { }
}
using Microsoft.Extensions.DependencyInjection;
using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.MediaBlocks.Memory;
using Staticsoft.Testing;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public abstract class OperationTestBase : TestBase<GraphMedia>
{
    protected abstract GraphProcessorBuilder Graph(GraphProcessorBuilder tree);

    protected override IServiceCollection Services => base.Services
        .AddSingleton<GraphMedia>()
        .UseJsonGraphProcessor(Graph)
        .AddSingleton<TestIntermediateStorage>()
        .ReuseSingleton<IntermediateStorage, TestIntermediateStorage>();

    protected Task Process(object properties, [CallerMemberName] string? testName = null)
    {
        if (testName == null) throw new NotSupportedException($"{nameof(testName)} cannot be null");
        Get<TestIntermediateStorage>().PrepareTestStorage(testName);
        var tree = JsonSerializer.Serialize(properties);
        return SUT.Build(tree);
    }

    protected static object Audio(string fileName)
        => Asset($"Assets/Audio/{fileName}");

    protected static object Image(string fileName)
        => Asset($"Assets/Images/{fileName}");

    protected static object Video(string fileName)
        => Asset($"Assets/Videos/{fileName}");

    static protected object Asset(object reference)
        => new
        {
            Type = nameof(Asset),
            Properties = new
            {
                Path = reference
            }
        };
}

public abstract class OperationTest : OperationTestBase
{
    static protected object Merge(object image, object audio)
        => new
        {
            Type = nameof(Merge),
            Properties = new
            {
                Image = new { Ref = image },
                Audio = new { Ref = audio }
            }
        };

    static protected object Concat(params object[] references)
        => new
        {
            Type = nameof(Concat),
            Properties = new
            {
                References = references
            }
        };

    static protected object FadeIn(object reference, int duration)
        => new
        {
            Type = nameof(FadeIn),
            Properties = new
            {
                Media = reference,
                Duration = duration
            }
        };

    static protected object FadeOut(object reference, int duration)
        => new
        {
            Type = nameof(FadeOut),
            Properties = new
            {
                Media = reference,
                Duration = duration
            }
        };
}
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
        var tree = JsonSerializer.Serialize(properties, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        return SUT.Build(tree);
    }

    protected static MediaReference Audio(string fileName)
        => new() { Path = $"Assets/Audio/{fileName}", Type = MediaType.Audio };

    protected static MediaReference Image(string fileName)
        => new() { Path = $"Assets/Images/{fileName}", Type = MediaType.Image };

    protected static MediaReference Video(string fileName)
        => new() { Path = $"Assets/Videos/{fileName}", Type = MediaType.Video };
}

public abstract class OperationTest : OperationTestBase
{
    static protected object Merge(params object[] references)
        => new
        {
            Type = nameof(Merge),
            Data = references
        };

    static protected object Concat(params object[] references)
        => new
        {
            Type = nameof(Concat),
            Data = references
        };

    static protected object FadeIn(object reference, int duration)
        => new
        {
            Type = nameof(FadeIn),
            Data = new
            {
                Media = reference,
                Duration = duration
            }
        };

    static protected object FadeOut(object reference, int duration)
        => new
        {
            Type = nameof(FadeOut),
            Data = new
            {
                Media = reference,
                Duration = duration
            }
        };
}
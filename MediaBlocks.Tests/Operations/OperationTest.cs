﻿using Microsoft.Extensions.DependencyInjection;
using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.MediaBlocks.Memory;
using Staticsoft.Testing;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace Staticsoft.MediaBlocks.Tests;

public abstract class OperationTestBase : TestBase<GraphMedia>
{
    protected abstract GraphProcessorBuilder Graph(GraphProcessorBuilder graph);

    protected override IServiceCollection Services => base.Services
        .AddSingleton<GraphMedia>()
        .UseJsonGraphProcessor(Graph)
        .AddSingleton<TestIntermediateStorage>()
        .ReuseSingleton<IntermediateStorage, TestIntermediateStorage>();

    protected Task Process(object properties, [CallerMemberName] string? testName = null)
    {
        if (testName == null) throw new NotSupportedException($"{nameof(testName)} cannot be null");
        Get<TestIntermediateStorage>().PrepareTestStorage(testName);
        var graph = JsonSerializer.Serialize(properties);
        return SUT.Build(graph);
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

    static protected object ConcatVideo(params object[] references)
        => new
        {
            Type = nameof(ConcatVideo),
            Properties = references.Select(reference => new { Ref = reference }).ToArray()
        };

    static protected object ConcatAudio(params object[] references)
        => new
        {
            Type = nameof(ConcatAudio),
            Properties = references.Select(reference => new { Ref = reference }).ToArray()
        };

    static protected object FadeIn(object reference, int duration)
        => new
        {
            Type = nameof(FadeIn),
            Properties = new
            {
                Video = new { Ref = reference },
                Duration = duration
            }
        };

    static protected object FadeOut(object reference, int duration)
        => new
        {
            Type = nameof(FadeOut),
            Properties = new
            {
                Video = new { Ref = reference },
                Duration = duration
            }
        };
}
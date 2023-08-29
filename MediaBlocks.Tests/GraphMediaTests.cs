using Microsoft.Extensions.DependencyInjection;
using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.GraphOperations.Memory;
using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.MediaBlocks.Memory;
using Staticsoft.Testing;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Staticsoft.MediaBlocks.Tests;

public class GraphMediaTests : TestBase<GraphMedia>
{
    protected override IServiceCollection Services => base.Services
        .UseJsonGraphProcessor(graph => graph
            .With<NothingOperation>()
        )
        .AddSingleton<GraphMedia>();

    [Test]
    public async Task ReturnsMediaReferenceWhenMediaIsBuilt()
    {
        var reference = await Build(new
        {
            Nothing = new
            {
                Type = "Nothing",
                Properties = new
                {
                    Path = "PathToVideoFile",
                    Type = "Video"
                }
            }
        });
        Assert.Equal("PathToVideoFile", reference.Path);
    }

    Task<MediaReference> Build<Configuration>(Configuration configuration)
        => SUT.Build(FromConfiguration(configuration));

    static string FromConfiguration<Configuration>(Configuration configuration)
        => JsonSerializer.Serialize(configuration);
}

public class NothingOperation : Operation<MediaReference, MediaReference>
{
    protected override Task<MediaReference> Process(MediaReference data)
        => Task.FromResult(data);
}

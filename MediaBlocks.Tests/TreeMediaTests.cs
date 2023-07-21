using Microsoft.Extensions.DependencyInjection;
using Staticsoft.MediaBlocks.Abstractions;
using Staticsoft.MediaBlocks.Memory;
using Staticsoft.Testing;
using Staticsoft.TreeOperations.Abstractions;
using Staticsoft.TreeOperations.Memory;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Staticsoft.MediaBlocks.Tests;

public class TreeMediaTests : TestBase<TreeMedia>
{
    protected override IServiceCollection Services => base.Services
        .UseJsonTreeProcessor<MediaReference>(tree => tree
            .With<NothingOperation>()
        )
        .AddSingleton<TreeMedia>();

    [Test]
    public async Task ReturnsMediaReferenceWhenMediaIsBuilt()
    {
        var reference = await Build(new
        {
            Type = "Nothing",
            Data = new
            {
                Path = "PathToVideoFile",
                Type = "Video"
            }
        });
        Assert.Equal("PathToVideoFile", reference.Path);
        Assert.Equal(MediaType.Video, reference.Type);
    }

    Task<MediaReference> Build<Configuration>(Configuration configuration)
        => SUT.Build(FromConfiguration(configuration));

    static string FromConfiguration<Configuration>(Configuration configuration)
        => JsonSerializer.Serialize(configuration, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
}

public class NothingOperation : Operation<MediaReference, MediaReference>
{
    protected override Task<MediaReference> Process(MediaReference data)
        => Task.FromResult(data);
}

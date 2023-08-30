using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;
using System.Text;

namespace MediaBlocks.Subtitles;

public class SubtitlesOperation : Operation<SubtitlesOperationProperties, MediaReference>
{
    readonly IntermediateStorage Storage;

    public SubtitlesOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(SubtitlesOperationProperties properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.ass";
        await GenerateASSFile(output, properties.Text, Convert.ToDouble(properties.Duration) / 1000);
        return new MediaReference { Path = output };
    }

    static async Task GenerateASSFile(string path, string text, double duration)
    {
        var options = new FileStreamOptions() { Access = FileAccess.Write, Mode = FileMode.CreateNew };
        using var file = new StreamWriter(path, Encoding.UTF8, options);
        await WriteHeader(file);
        await WriteStyles(file);
        await WriteEvents(file);
        await WriteSubtitles(file, text, duration);
    }

    static async Task WriteStyles(TextWriter file)
    {
        await file.WriteLineAsync("[V4+ Styles]");
        await file.WriteLineAsync("Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding");
        await file.WriteLineAsync("Style: Default,Arial,28,&H00FFFFFF,&H000000FF,&H00000000,&H00000000,0,0,0,0,100,100,0,0,1,2,2,2,10,10,10,1");
        await file.WriteLineAsync();
    }

    static async Task WriteEvents(TextWriter file)
    {
        await file.WriteLineAsync("[Events]");
        await file.WriteLineAsync("Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text");
    }

    static async Task WriteHeader(TextWriter file)
    {
        await file.WriteLineAsync("[Script Info]");
        await file.WriteLineAsync("; This is an Advanced Sub Station Alpha v4+ script.");
        await file.WriteLineAsync("Title: Simple ASS Example");
        await file.WriteLineAsync("ScriptType: v4.00+");
        await file.WriteLineAsync("WrapStyle: 0");
        await file.WriteLineAsync("ScaledBorderAndShadow: yes");
        await file.WriteLineAsync("YCbCr Matrix: None");
        await file.WriteLineAsync();
    }

    static async Task WriteSubtitles(TextWriter file, string text, double duration)
    {
        var startTime = "0:00:00.00";
        var endTime = $"0:00:{duration:00.00}";

        await file.WriteLineAsync($"Dialogue: 0,{startTime},{endTime},Default,,0,0,0,,{text}");
    }
}

public class SubtitlesOperationProperties
{
    public string Text { get; init; } = string.Empty;
    public int Duration { get; init; }
}

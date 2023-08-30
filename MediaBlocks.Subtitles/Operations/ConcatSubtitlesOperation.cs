using Staticsoft.GraphOperations.Abstractions;
using Staticsoft.MediaBlocks.Abstractions;

namespace MediaBlocks.Subtitles;

public class ConcatSubtitlesOperation : Operation<string[], MediaReference>
{
    readonly IntermediateStorage Storage;

    public ConcatSubtitlesOperation(IntermediateStorage storage)
        => Storage = storage;

    protected override async Task<MediaReference> Process(string[] properties)
    {
        var output = $"{Storage.CreateIntermediateFilePath()}.ass";
        await MergeAdvancedSubStationAlphaFiles(properties, output);
        return new() { Path = output };
    }

    static async Task MergeAdvancedSubStationAlphaFiles(string[] assFiles, string outputFile)
    {
        var writeOptions = new FileStreamOptions() { Access = FileAccess.Write, Mode = FileMode.Create };
        using var merged = new StreamWriter(outputFile, writeOptions);

        var timeOffset = 0;
        foreach (var file in assFiles)
        {
            var options = new FileStreamOptions() { Access = FileAccess.Read, Mode = FileMode.Open };
            using var reader = new StreamReader(file, options);

            var lastDialogue = new DialogueLine();
            while (!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;
                if (DialogueLine.TryParse(line, out var dialogue))
                {
                    await merged.WriteLineAsync($"{dialogue.WithOffset(TimeSpan.FromMilliseconds(timeOffset))}");

                    lastDialogue = dialogue;
                }
                else if (timeOffset == 0)
                {
                    await merged.WriteLineAsync(line);
                }
            }

            timeOffset += Convert.ToInt32(lastDialogue.End.TotalMilliseconds);
        }
    }
}

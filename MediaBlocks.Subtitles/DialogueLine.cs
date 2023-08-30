using System.Diagnostics.CodeAnalysis;

namespace MediaBlocks.Subtitles;

record DialogueLine
{
    public string Layer { get; init; } = string.Empty;
    public TimeSpan Start { get; init; }
    public TimeSpan End { get; init; }
    public string Style { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string MarginL { get; init; } = string.Empty;
    public string MarginR { get; init; } = string.Empty;
    public string MarginV { get; init; } = string.Empty;
    public string Effect { get; init; } = string.Empty;
    public string Text { get; init; } = string.Empty;

    public static bool TryParse(string line, [NotNullWhen(true)] out DialogueLine? result)
    {
        if (!line.StartsWith("Dialogue:"))
        {
            result = null;
            return false;
        }

        var values = line.Replace("Dialogue: ", string.Empty).Split(',');
        result = new()
        {
            Layer = values[0],
            Start = TimeSpan.Parse(values[1]),
            End = TimeSpan.Parse(values[2]),
            Style = values[3],
            Name = values[4],
            MarginL = values[5],
            MarginR = values[6],
            MarginV = values[7],
            Effect = values[8],
            Text = values[9]
        };
        return true;
    }

    public DialogueLine WithOffset(TimeSpan offset)
        => this with
        {
            Start = Start.Add(offset),
            End = End.Add(offset)
        };

    public override string ToString()
        => $"Dialogue: {Layer},{ToTimestamp(Start)},{ToTimestamp(End)},{Style},{Name},{MarginL},{MarginR},{MarginV},{Effect},{Text}";

    static string ToTimestamp(TimeSpan time)
    {
        var centiseconds = time.Milliseconds / 10;
        return $"{time:hh\\:mm\\:ss}.{centiseconds:D2}";
    }
}
namespace Staticsoft.MediaBlocks.Abstractions;

public class TextReference : MediaReference
{
    public string Text { get; init; } = string.Empty;

    public override object RefAttribute
        => Text;
}

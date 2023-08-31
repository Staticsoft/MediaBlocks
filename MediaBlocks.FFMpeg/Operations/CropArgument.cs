using FFMpegCore.Arguments;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class CropArgument : IVideoFilterArgument
{
    readonly int CroppedWidth;
    readonly int CroppedHeight;
    readonly string PositionX;
    readonly string PositionY;

    public CropArgument(int croppedWidth, int croppedHeight, string positionX, string positionY)
        => (CroppedWidth, CroppedHeight, PositionX, PositionY)
        = (croppedWidth, croppedHeight, positionX, positionY);

    public string Key
        => "crop";

    public string Value
        => $"out_w={CroppedWidth}:out_h={CroppedHeight}:x='{PositionX}':y='{PositionY}'";
}

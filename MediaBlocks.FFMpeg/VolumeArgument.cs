using FFMpegCore.Arguments;
using System;

namespace Staticsoft.MediaBlocks.FFMpeg;

public class VolumeArgument : IAudioFilterArgument
{
    readonly int Volume;

    public VolumeArgument(int volume)
        => Volume = volume;

    public string Key
        => "volume";

    public string Value
        => $"{Convert.ToSingle(Volume) / 100}";
}
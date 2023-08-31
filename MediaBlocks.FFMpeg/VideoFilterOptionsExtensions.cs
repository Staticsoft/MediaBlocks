using FFMpegCore;
using FFMpegCore.Arguments;
using System;

namespace Staticsoft.MediaBlocks.FFMpeg;

public static class VideoFilterOptionsExtensions
{
    public static VideoFilterOptions FadeIn(this VideoFilterOptions options, int duration)
        => options.Fade(0, duration, "in");

    public static VideoFilterOptions FadeOut(this VideoFilterOptions options, int startTime, int duration)
        => options.Fade(startTime, duration, "out");

    static VideoFilterOptions Fade(this VideoFilterOptions options, int startTime, int duration, string direction)
    {
        options.Arguments.Add(new FadeArgument(startTime, duration, "fade", direction));
        return options;
    }

    public static VideoFilterOptions Pan(this VideoFilterOptions options, PanDirection direction, VideoStream video)
    {
        var zoom = 80;
        var zoomedWidth = video.Width * zoom / 100;
        var zoomedHeight = video.Height * zoom / 100;
        var positionX = GetPanPositionX(video, zoom, direction);
        var positionY = $"{video.Height * (100 - zoom) / 200}";
        options.Arguments.Add(new CropArgument(zoomedWidth, zoomedHeight, positionX, positionY));
        return options;
    }

    static string GetPanPositionX(VideoStream video, int zoom, PanDirection direction) => direction switch
    {
        PanDirection.LeftToRight => $"(t/{video.Duration.TotalSeconds})*({video.Width * (100 - zoom) / 100})",
        PanDirection.RightToLeft => $"(1 - t/{video.Duration.TotalSeconds})*({video.Width * (100 - zoom) / 100})",
        _ => throw new NotSupportedException($"Direction '{direction}' is not supported")
    };
}

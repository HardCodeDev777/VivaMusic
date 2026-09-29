using System;
using System.Text.Json.Serialization;
using YoutubeDLSharp.Options;

namespace VivaMusic.Models;

public record struct AppConfigData(bool SaveExtractedCover,
    string DownloadFolderPath, AudioConversionFormat YtdlFormat)
{
    public AppConfigData() : this(false,
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                AudioConversionFormat.Mp3)
    { }
}

// For Native-AOT/Trimming
[JsonSerializable(typeof(AppConfigData))]
internal partial class AppConfigJsonContext : JsonSerializerContext { }
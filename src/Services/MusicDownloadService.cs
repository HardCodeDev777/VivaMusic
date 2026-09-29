using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VivaMusic.Utils;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace VivaMusic.Services;

public static class MusicDownloadService
{
    /// <returns>Path to created file</returns>
    public static async Task<string> DownloadMusicWithUI(string songName, string artistName,
        IProgress<double> percentProgress, IProgress<string> textProgress)
    {

        var progress = new Progress<DownloadProgress>(p =>
        {
            percentProgress.Report(p.Progress * 100);

            if (p.State == DownloadState.PreProcessing)
                textProgress.Report("Prepocessing...");
            else if (p.State == DownloadState.Downloading)
            {
                textProgress.Report($"Downloaded: {Math.Round(p.Progress * 100, 1)}%, " +
                    $"Speed: {p.DownloadSpeed}");
            }

        });

        var appDir = AppContext.BaseDirectory;

        var ytdl = new YoutubeDL
        {
            OutputFolder = AppConfigService.Config.DownloadFolderPath,
            OutputFileTemplate = "%(title)s.%(ext)s",
            FFmpegPath = Path.Combine(appDir, "External", "ffmpeg.exe"),
            YoutubeDLPath = Path.Combine(appDir, "External", "yt-dlp.exe")
        };

        var options = new OptionSet
        {
            ExtractAudio = true,
            AudioFormat = AppConfigService.Config.YtdlFormat,
            EmbedMetadata = true,
            EmbedThumbnail = true,
            PlaylistItems = "1",
            Retries = 1
        };

        Log.Information($"Artist name: {artistName}, song name: {songName}");

        var formedSongString = Uri.EscapeDataString(songName);
        var formedArtistString = Uri.EscapeDataString(artistName);

        var url = $"https://music.youtube.com/search?q={formedArtistString}+{formedSongString}#songs";
        Log.Information($"Formed url: {url}");
        Log.Information("Download started...");

        textProgress.Report("Download started...");

        var res = await ytdl.RunVideoDownload(url, overrideOptions: options, progress: progress);
        if (res.Success)
        {
            Log.Information($"Song successfully saved: {res.Data}");
            textProgress.Report("File successfully saved");

            if (!File.Exists(res.Data))
            {
                var dir = Path.GetDirectoryName(res.Data)!;
                var newestFile = new DirectoryInfo(dir)
                    .GetFiles("*mp3")
                    .OrderByDescending(f => f.CreationTime)
                    .FirstOrDefault();

                if (newestFile is not null) return newestFile.FullName;
            }

            return res.Data;
        }
        else
        {
            throw new Exception($"Errors: {string.Join('\n', res.ErrorOutput)}");
        }
    }
}

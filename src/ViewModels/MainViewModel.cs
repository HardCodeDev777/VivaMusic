using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using VivaMusic.Services;
using VivaMusic.Utils;

namespace VivaMusic.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isFinding;

    [ObservableProperty]
    private string _artistName = string.Empty;
    [ObservableProperty]
    private string _songName = string.Empty;

    [ObservableProperty]
    private double _downloadProgressValue;

    [ObservableProperty]
    private string _downloadProgressText = string.Empty;

    [ObservableProperty]
    private Bitmap? _coverImage;

    public MainViewModel()
    {
        CoverImage = LoadDefaulCover();
    }

    private Bitmap LoadDefaulCover()
    {
        var uri = new Uri("avares://VivaMusic/Assets/Blank.png");
        using var stream = AssetLoader.Open(uri);
        return new Bitmap(stream);
    }

    [RelayCommand]
    private async Task FindSong()
    {
        if (string.IsNullOrEmpty(SongName) || string.IsNullOrEmpty(ArtistName))
            return;

        IsFinding = true;

        try
        {
            var percentProgress = new Progress<double>(p =>
            {
                DownloadProgressValue = p;
            });

            var textProgress = new Progress<string>(t =>
            {
                DownloadProgressText = t;
            });

            var pathToCreatedFile = await MusicDownloadService.DownloadMusicWithUI(SongName,
                ArtistName, percentProgress, textProgress);

            if (string.IsNullOrEmpty(pathToCreatedFile))
            {
                Log.Warning("Music file wasn't created!");
                return;
            }

            var pathToCoverFile = FileUtils.ExtractSongCover(pathToCreatedFile);
            if (string.IsNullOrEmpty(pathToCoverFile))
            {
                Log.Warning("Cover file wasn't created!");
                return;
            }

            using (var stream = File.OpenRead(pathToCoverFile))
            {
                CoverImage = new Bitmap(stream);
            }

            if (!AppConfigService.Config.SaveExtractedCover)
                File.Delete(pathToCoverFile);
            

            Log.Information($"Done successfully");
        }
        catch (Exception ex) 
        {
            HandleUtils.WriteErrorAndShowMessage($"Error downloading and saving song: {ex.Message}");
        }
        finally
        {
            IsFinding = false;
        }
    }
}

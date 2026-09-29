using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VivaMusic.Services;
using YoutubeDLSharp.Options;

namespace VivaMusic.ViewModels;

public partial class SettingsOptionsViewModel : ObservableObject
{
    public IReadOnlyList<AudioConversionFormat> AvailableFormats { get; }

    [ObservableProperty]
    private string _downloadFolderPath = string.Empty;

    [ObservableProperty]
    private bool _saveExtractedCover;

    [ObservableProperty]
    private AudioConversionFormat _selectedFormat;

    
    public SettingsOptionsViewModel()
    {
        AvailableFormats = Enum.GetValues<AudioConversionFormat>()
            .Where(d => d != AudioConversionFormat.Wav && d != AudioConversionFormat.Best)
            .ToList();

        DownloadFolderPath = AppConfigService.Config.DownloadFolderPath;
        SaveExtractedCover = AppConfigService.Config.SaveExtractedCover;
        SelectedFormat = AppConfigService.Config.YtdlFormat;
    }

    partial void OnSelectedFormatChanged(AudioConversionFormat value)
    {
        AppConfigService.Config.YtdlFormat = value;
        AppConfigService.Save();
    }

    [RelayCommand]
    private async Task OpenFileDialog(TopLevel? topLevel)
    {
        if (topLevel is null) return;

        var result = await topLevel!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Open Directory",
            AllowMultiple = false
        });

        if (result.Count > 0)
        {
            var folderPath = result[0].Path.LocalPath;
            Log.Information($"Selected folder: {folderPath}");

            DownloadFolderPath = folderPath;
            AppConfigService.Config.DownloadFolderPath = folderPath;
            AppConfigService.Save();
        }
    }

    [RelayCommand]
    private void ClearSettings()
    {
        AppConfigService.CreateEmpty();
        DownloadFolderPath = AppConfigService.Config.DownloadFolderPath;
        SaveExtractedCover = AppConfigService.Config.SaveExtractedCover;
        SelectedFormat = AppConfigService.Config.YtdlFormat;
    }


    partial void OnSaveExtractedCoverChanged(bool value)
    {
        AppConfigService.Config.SaveExtractedCover = SaveExtractedCover;
        AppConfigService.Save();
    }
}

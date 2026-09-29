using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Threading.Tasks;

namespace VivaMusic.Views;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();
    
    private void OpenSettings(object? sender, RoutedEventArgs e)
    {
        SettingsOverlay.Opacity = 1;
        SettingsOverlay.IsHitTestVisible = true;
        SettingsPanel.TriggerFadeIn();
    }

    private async void SettingsPanel_CloseRequested(object? sender, EventArgs e)
    {
        SettingsPanel.TriggerFadeOut();
        await Task.Delay(300);
        SettingsOverlay.Opacity = 0;
        SettingsOverlay.IsHitTestVisible = false;
    }
}
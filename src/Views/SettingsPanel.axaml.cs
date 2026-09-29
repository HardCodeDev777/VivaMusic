using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace VivaMusic.Views
{
    public partial class SettingsPanel : UserControl
    {
        public event EventHandler? CloseRequested;

        public SettingsPanel()
        {
            InitializeComponent();
        }

        private void CloseSettings(object? sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        public void TriggerFadeIn()
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                SettingsBorder.Opacity = 1;
            });
        }

        public void TriggerFadeOut()
        {
            SettingsBorder.Opacity = 0;
        }
    }
}
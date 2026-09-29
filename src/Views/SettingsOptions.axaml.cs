using Avalonia.Controls;
using VivaMusic.ViewModels;

namespace VivaMusic.Views
{
    public partial class SettingsOptions : UserControl
    {
        public SettingsOptions()
        {
            InitializeComponent();
            DataContext = new SettingsOptionsViewModel();
        }

    }
}
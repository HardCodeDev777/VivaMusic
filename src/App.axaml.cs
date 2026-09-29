using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.Threading.Tasks;
using VivaMusic.Services;
using VivaMusic.Utils;
using VivaMusic.ViewModels;
using VivaMusic.Views;

namespace VivaMusic;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            e.SetObserved();
            HandleUtils.WriteErrorAndShowMessage($"Unhandled error: {e.Exception.Message}");
        };

        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            var ex = e.ExceptionObject as Exception;
            HandleUtils.WriteErrorAndShowMessage($"Unhandled error: {ex?.Message}");
        };

        AppConfigService.Load();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
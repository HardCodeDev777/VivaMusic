using Avalonia;
using Serilog;
using System;
using System.IO;
using VivaMusic.Utils;

namespace VivaMusic;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "Logs/vivamusic-log-.txt"),
                rollingInterval: RollingInterval.Minute)
            .CreateLogger();

        try
        {
            Log.Information("Starting app...");
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
          //  UiUtils.ShowErrorMessageBox(ex.Message);
            Log.Fatal(ex, "Fatal error on start");
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}

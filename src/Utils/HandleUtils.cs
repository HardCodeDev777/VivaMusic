using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Serilog;

namespace VivaMusic.Utils;

public static class HandleUtils
{
    public static void WriteErrorAndShowMessage(string message)
    {
        Log.Error(message);

        Dispatcher.UIThread.InvokeAsync(async () =>
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", message,
                ButtonEnum.Ok, Icon.Error);
            await box.ShowAsync();
        });
    }
}

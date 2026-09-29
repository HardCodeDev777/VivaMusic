using Serilog;
using System;
using System.IO;
using System.Text.Json;
using VivaMusic.Models;

namespace VivaMusic.Services;

public static class AppConfigService
{
    public static AppConfigData Config;

    private static string ConfigPath => Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "config.json");

    public static void CreateEmpty()
    {
        Config = new AppConfigData();
        Save();
        Log.Information("New config created");
    }

    public static void Load()
    {
        if (!File.Exists(ConfigPath))
        {
            Log.Information("Config not found, creating new...");
            CreateEmpty();
            return;
        }

        try
        {
            var json = File.ReadAllText(ConfigPath);
            Config = JsonSerializer.Deserialize(json, AppConfigJsonContext.Default.AppConfigData);
            Log.Information("Config loaded");
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Error reading existed config, creating new...");
            CreateEmpty();
        }
    }

    public static void Save()
    {
        var dir = Path.GetDirectoryName(ConfigPath)!;
        if (!Directory.Exists(dir))
        {
            Log.Information("Creating directory for config...");
            Directory.CreateDirectory(dir);
        }

        var serializedJson = JsonSerializer.Serialize(Config, AppConfigJsonContext.Default.AppConfigData);
        File.WriteAllText(ConfigPath, serializedJson);
        Log.Information("Config saved");
    }
}

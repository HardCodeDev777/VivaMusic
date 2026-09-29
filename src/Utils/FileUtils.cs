using System.IO;

namespace VivaMusic.Utils;

public static class FileUtils
{
    /// <returns>Path to created file</returns>
    public static string ExtractSongCover(string pathToFile)
    {
        using (var file = TagLib.File.Create(pathToFile))
        {
            var pictures = file.Tag.Pictures;
            if (pictures.Length > 0)
            {
                var binData = pictures[0].Data.Data;
                if (binData is not null)
                {
                    var savePath = Path.Combine(Path.GetDirectoryName(pathToFile)!,
                        $"{Path.GetFileName(pathToFile)}_cover.jpg");
                    File.WriteAllBytes(savePath, binData);

                    return savePath;
                }
            }
        }

        return string.Empty;
    }
}

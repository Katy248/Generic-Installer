using System.Runtime.InteropServices;
using Tomlet;
using Tomlet.Models;

namespace GenericInstaller.Shared;

public class ConfigurationProvider
{
    public async Task<InstallConfiguration> Provide(string filePath)
    {
        var file = await GetFile(filePath);
        var tomlString = TomletMain.TomlStringFrom(file.FullName);
        var result = TomletMain.To<InstallConfiguration>(tomlString);

        return result;
    }

    private async Task<FileInfo> GetFile(string path)
    {
        if (path.StartsWith("http:") || path.StartsWith("https:"))
        {
            return await FetchFile(path);
        }
        return new FileInfo(path);
    }

    private async Task<FileInfo> FetchFile(string url)
    {
        var client = new HttpClient();
        await using var stream = await client.GetStreamAsync(url);
        await using var writer = File.Create(TemporaryFilePath);
        while (stream.CanRead)
        {
            writer.WriteByte((byte)stream.ReadByte());
        }

        return new FileInfo(TemporaryFilePath);
    }
    private string TemporaryFilePath
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) 
                || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) 
                || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                return "/tmp/installer-config.toml";
            }
            return "";

        }
    }
}
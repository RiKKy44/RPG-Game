using System;
using System.IO;
namespace OODProject;

public static class ConfigLoader
{
    public static ConfigurationData Load(string filePath)
    {
        var config = new ConfigurationData();

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Config file not found", filePath);
        }
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith("#") || trimmedLine.StartsWith("[")||trimmedLine.StartsWith(";"))
            {
                continue;
            }

            var parts = trimmedLine.Split('=', 2);

            if (parts.Length == 2)
            {
                var key = parts[0].Trim();

                var value = parts[1].Trim();
                
                if(key.Equals("PlayerName", StringComparison.OrdinalIgnoreCase))
                {
                    config.PlayerName = value;
                }

                if (key.Equals("LogPath", StringComparison.OrdinalIgnoreCase))
                {
                    config.LogPath = value;
                }
                
            }

        }
        return config;
    }
}
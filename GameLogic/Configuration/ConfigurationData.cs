namespace OODProject;

public record ConfigurationData()
{
    public string PlayerName { get; set; } = "Unknown Player";
    public string LogPath  { get; set; } = "Log/";
}
namespace OODProject.Logs;

public class FileLogStrategy: ILogStrategy

{
    
    private readonly string _filePath;

    public FileLogStrategy(string directory, string playerName)
    {
        Directory.CreateDirectory(directory);
        
        string timeStamp = DateTime.Now.ToString("yyyYMMdd_HHmmss");

        _filePath = Path.Combine(directory, $"{playerName}_{timeStamp}.log");
    }
    public void Write(string message)
    {
        File.AppendAllText(_filePath, message + Environment.NewLine);
    }
}
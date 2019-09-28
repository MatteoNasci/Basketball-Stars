
public interface ISaveable
{
    string FullFilename { get; }
    void Save();
    void Load();
    void DeleteFile();
    void Reset();
}
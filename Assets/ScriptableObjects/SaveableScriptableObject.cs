using UnityEngine;
using System.IO;
public abstract class SaveableScriptableObject : ScriptableObject, ISaveable
{
    public abstract string Filename { get; }
    public abstract string FolderName { get; }
    public string FullFilename { get; private set; }
    public string FolderPath { get; private set; }

    public abstract void Reset();
    protected virtual void OnEnable()
    {
        FolderPath = FolderName == null ? string.Empty : Path.Combine(Application.persistentDataPath, FolderName);
        FullFilename = Path.ChangeExtension(Path.Combine(FolderPath, Filename), Utils.JsonExtension);
        Reset();
        Load();
    }
    public void Save()
    {
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }
        File.WriteAllText(FullFilename, JsonUtility.ToJson(this));
    }
    public void Load()
    {
        if (!File.Exists(FullFilename))
        {
            Save();
        }
        else
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(FullFilename), this);
        }
    }
    public void DeleteFile()
    {
        if (File.Exists(FullFilename))
        {
            File.Delete(FullFilename);
        }
    }
}

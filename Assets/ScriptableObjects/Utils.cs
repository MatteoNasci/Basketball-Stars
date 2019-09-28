
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
[CreateAssetMenu(menuName = "Utils")]
public class Utils : ScriptableObject //scriptableObject so that I can register these methods to UnityEvents from the editor
{
    public const string ConfigFolderName = "Config";
    public const string SellableFolderName = "Sellable";
    public static readonly string BallsFolderName = Path.Combine(SellableFolderName, "Balls");
    public const string JsonExtension = "json";
    public const float DefaultDestroyDelay = 0.05f;

    public static int GetRandomInt(int min, int maxExcluded, int excludedIndex)
    {
        int Index = Random.Range(min, maxExcluded);

        if (Index == excludedIndex)
        {
            Index++;
            if (Index >= maxExcluded)
            {
                Index = min;
            }
        }

        return Index;
    }
    public void DestroyGameobject(GameObject toDestroy)
    {
        if (toDestroy)
        {
            GameObject.Destroy(toDestroy, DefaultDestroyDelay);
        }
    }
    public void UnloadUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }
    public void LoadSceneAdditive(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
    }
    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    public static bool IsTrue(BoolField field)
    {
        return field && field.Value;
    }
}

using UnityEngine;
using System.IO;
[CreateAssetMenu(menuName = "Game Data")]
public class GameData : SaveableScriptableObject
{
    public const int CleanScoreValue = 3;
    public const int DirtyScoreValue = 2;
    public const int FireballScoreMultiplier = 2;

    public const string GameFilename = "GameData";
    public const float AbsoluteMinScoreRewardMultiplier = 0.0f;
    public const float AbsoluteMinWinRewardMultiplier = 0.0f;

    public bool UseAI { get { return useAi; } set { useAi = value; } }
    public bool UseOvershootBar { get { return useOvershootBar; } set { useOvershootBar = value; } }


    public float ScoreRewardMultiplier { get { return scoreRewardMultiplier; } set { scoreRewardMultiplier = Mathf.Max(value, AbsoluteMinScoreRewardMultiplier); } }
    public float WinRewardMultiplier { get { return winRewardMultiplier; } set { winRewardMultiplier = Mathf.Max(value, AbsoluteMinWinRewardMultiplier); } }
    public string GameDataFilename { get; private set; }

    public override string Filename { get { return GameFilename; } }
    public override string FolderName { get { return Utils.ConfigFolderName; } }

    [SerializeField]
    [Range(AbsoluteMinScoreRewardMultiplier, float.MaxValue)]
    private float scoreRewardMultiplier;

    [SerializeField]
    [Range(AbsoluteMinWinRewardMultiplier, float.MaxValue)]
    private float winRewardMultiplier;

    [SerializeField]
    private bool useAi;

    [SerializeField]
    private bool useOvershootBar;

    public override void Reset()
    {
        useOvershootBar = true;
        useAi = false;
        winRewardMultiplier = 3.0f;
        scoreRewardMultiplier = 1.0f;
    }
}

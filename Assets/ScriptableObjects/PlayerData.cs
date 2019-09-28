using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : SaveableScriptableObject
{
    public const string PlayerFilename = "Players";
    public const float AbsoluteMinTopForceAmount = 750.0f;
    public const float AbsoluteMaxBottomForceAmount = 0.0f;
    public const float AbsoluteMinForceMultiplier = 0.01f;
    public const float AbsoluteMinLaunchDuration = 0.1f;

    public float MaxForceAmount { get { return maxForceAmount; } set { maxForceAmount = Mathf.Max(value, AbsoluteMinTopForceAmount); } }
    public float MinForceAmount { get { return minForceAmount; } set { minForceAmount = Mathf.Max(value, AbsoluteMaxBottomForceAmount); } }
    public float MaxLaunchDuration { get { return maxLaunchDuration; } set { maxLaunchDuration = Mathf.Max(value, AbsoluteMinLaunchDuration); } }
    public float ForceMultiplier { get { return forceMultiplier; } set { forceMultiplier = Mathf.Max(value, AbsoluteMinForceMultiplier); } }
    public string BallPath
    {
        get { return ballPath; }
        set
        {
            if (ballPath != value)
            {
                ballPath = value;
                prefab = null;
            }
        }
    }

    public override string Filename { get { return PlayerFilename; } }
    public override string FolderName { get { return Utils.ConfigFolderName; } }

    [SerializeField]
    [Range(AbsoluteMinLaunchDuration, 10.0f)]
    private float maxLaunchDuration;

    [SerializeField]
    [Range(AbsoluteMinForceMultiplier, float.MaxValue)]
    private float forceMultiplier;

    [SerializeField]
    [Range(AbsoluteMinTopForceAmount, float.MaxValue)]
    private float maxForceAmount = AbsoluteMinTopForceAmount;

    [SerializeField]
    [Range(AbsoluteMaxBottomForceAmount, float.MaxValue)]
    private float minForceAmount;

    [SerializeField]
    private string ballPath = string.Empty;

    private GameObject prefab;

    private List<Player> players = new List<Player>();

    public int GetPlayersCount() { return players.Count; }
    public Player GetPlayer(int Index)
    {
        if (Index < 0 || Index >= players.Count)
        {
            return null;
        }
        return players[Index];
    }
    public Player GetAiPlayer()
    {
        Player Ai = null;
        for (int i = 0; i < players.Count; i++)
        {
            Player Current = players[i];
            if (!Current.IsHumanPlayer)
            {
                Ai = Current;
                break;
            }
        }
        return Ai;
    }
    public Player GetHumanPlayer()
    {
        Player Human = null;
        for (int i = 0; i < players.Count; i++)
        {
            Player Current = players[i];
            if (Current.IsHumanPlayer)
            {
                Human = Current;
                break;
            }
        }
        return Human;
    }
    public void AddPlayer(Player ToAdd)
    {
        players.Add(ToAdd);
    }
    public void RemovePlayer(Player ToRemove)
    {
        players.Remove(ToRemove);
    }
    public GameObject GetBallPrefab()
    {
        if (!prefab)
        {
            prefab = Resources.Load<GameObject>(ballPath);
        }
        return prefab;
    }
    void OnDisable()
    {
        prefab = null;
    }
    public void UpdateValues(float maxLaunchDuration, float forceMultiplier, float maxForceAmount, float minForceAmount, string BallPath)
    {
        this.maxLaunchDuration = maxLaunchDuration;
        this.forceMultiplier = forceMultiplier;
        this.maxForceAmount = maxForceAmount;
        this.minForceAmount = minForceAmount;
        this.BallPath = BallPath;
        Save();
    }

    public override void Reset()
    {
        minForceAmount = 500.0f;
        maxForceAmount = 750.0f;
        forceMultiplier = 0.25f;
        maxLaunchDuration = 1.0f;
        ballPath = string.Empty;
    }
}

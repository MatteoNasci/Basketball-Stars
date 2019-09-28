using UnityEngine;
public class AiLauncher : Player
{
    [SerializeField]
    private AiStats stats;
    [SerializeField]
    private GameData gameData;

    private float timer;
    private float duration;

    protected override void OnValidate()
    {
        base.OnValidate();
        Id = AiPlayerId;
    }

    protected override void Start()
    {
        if(!stats || !gameData)
        {
            Debug.LogErrorFormat("{0} of type {1} requires AiStats 'stats' and GameData 'gameData' to be valid references!", this, GetType());
            MyTransform.root.gameObject.SetActive(false);
            return;
        }

        if (!gameData.UseAI) //if ai is not allowed make sure to not activate the object
        {
            MyTransform.root.gameObject.SetActive(false);
        }
        else
        {
            base.Start();
        }
    }
    public override void PrepareShot(bool changeLaunchLocation)
    {
        base.PrepareShot(changeLaunchLocation);
        timer = 0.0f;
        duration = stats.LaunchDelay;
        this.enabled = true;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > duration)
        {
            float intervall = 1.0f - stats.Precision;
            float suggestedScore = CurrentSuggestedForce;
            float min = suggestedScore - (suggestedScore * intervall);
            float max = suggestedScore + (suggestedScore * intervall);

            LaunchBall(Random.Range(min, max));
            this.enabled = false;
        }
    }
}

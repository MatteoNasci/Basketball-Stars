
using UnityEngine;
using UnityEngine.UI;
public class FireballPowerup : MonoBehaviour
{
    public int CurrentScore { get { return playerScore ? playerScore.Value : 0; } }
    public int ScoreGained { get { return Utils.IsTrue(isFireballActive) ? scoreForActivation : CurrentScore - lastSavedScore; } }
    public float ScoreGainedNormalized
    {
        get
        {
            float result = 0.0f;

            if (isFireballActive)
            {
                if (isFireballActive.Value)
                {
                    result = 1.0f - (Mathf.Approximately(0.0f, fireballDuration) ? 1.0f : timer / fireballDuration);
                }
                else
                {
                    result = Mathf.Approximately(0.0f, scoreForActivation) ? 1.0f : (float)ScoreGained / scoreForActivation;
                }
            }

            return Mathf.Clamp01(result);
        }
    }

    [SerializeField]
    private float fireballDuration = 10.0f;
    [SerializeField]
    private int scoreForActivation = 10;
    [SerializeField]
    private BoolField isFireballActive;
    [SerializeField]
    private IntField playerScore;

    [SerializeField]
    private GameObject fireballEffectPrefab;
    [SerializeField]
    private int playerId = -1;
    [SerializeField]
    private ScaledProgressBar firebar;

    private GameObject fireballEffect;
    private float timer;
    private int lastSavedScore;

    private void Reset()
    {
        if (Application.isEditor)
        {
            OnValidate();
        }
    }
    private void OnValidate()
    {
        Player player = GetComponent<Player>();
        if (player)
        {
            playerId = player.Id;
            if (player.Score)
            {
                playerScore = player.Score;
            }
            if (player.GetIsFireballActive)
            {
                isFireballActive = player.GetIsFireballActive;
            }
        }
    }
    private void Awake()
    {
        if (isFireballActive)
        {
            isFireballActive.Value = false;
        }
        if (fireballEffectPrefab)
        {
            fireballEffect = GameObject.Instantiate(fireballEffectPrefab, transform);
        }
        UpdateEffect();
    }
    private void OnEnable()
    {
        if (!isFireballActive || !playerScore)
        {
            Debug.LogErrorFormat("{0} of type {1} requires a BoolField reference and an IntReference", this, this.GetType());
        }
        UpdateEffect();
    }

    public void UpdateEffect()
    {
        if (fireballEffect)
        {
            fireballEffect.SetActive(Utils.IsTrue(isFireballActive));
        }
        if (firebar)
        {
            firebar.SetFillRate(ScoreGainedNormalized);
        }
    }

    public void StopFireball(Player player)
    {
        if (!player || player.Id != this.playerId)
        {
            return;
        }

        DisableFireball();
    }
    public void ActivateFireball()
    {
        if (isFireballActive)
        {
            isFireballActive.Value = true;
        }
        timer = 0.0f;
        UpdateEffect();
    }

    void Update()
    {
        if (!Utils.IsTrue(isFireballActive))
        {
            if (ScoreGained >= scoreForActivation)
            {
                ActivateFireball();
                return;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= fireballDuration)
            {
                DisableFireball();
                return;
            }
        }

        UpdateEffect();
    }
    public void DisableFireball()
    {
        if (playerScore && Utils.IsTrue(isFireballActive))
        {
            lastSavedScore = playerScore.Value;
            isFireballActive.Value = false;
        }
        UpdateEffect();
    }
}

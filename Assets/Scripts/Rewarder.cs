
using UnityEngine;
using UnityEngine.UI;
public class Rewarder : MonoBehaviour
{
    public const string WinText = "You Win!";
    public const string WinTextSingle = "New best score!";
    public const string LoseText = "You Lose!";
    public const string LoseTextSingle = "Nice practice!";

    [SerializeField]
    private AudioSource victorySound;
    [SerializeField]
    private AudioSource defeatSound;
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private TextUiData rewardText = new TextUiData();
    [SerializeField]
    private PlayerData players;
    [SerializeField]
    private CurrencyHolder currency;
    [SerializeField]
    private GameData data;

    [SerializeField]
    private ParticleSystem victoryEffect;
    void Start()
    {
        if (!players || !data || !currency)
        {
            Debug.LogErrorFormat("{0} of type {1} is missing core references to players, data and currency", this, this.GetType());
            return;
        }

        Player Human = players.GetHumanPlayer();
        Player Ai = players.GetAiPlayer();

        if (!Human || !Human.Score)
        {
            Debug.LogErrorFormat("{0} of type {1} could not find an human player reference or its score field", this, this.GetType());
            return;
        }


        bool playerWon = (data.UseAI && Ai && Ai.Score) ? Human.Score.Value > Ai.Score.Value : Human.Score.Value > currency.BestScore;

        if (resultText)
        {
            if (data.UseAI)
            {
                resultText.text = playerWon ? WinText : LoseText;
            }
            else
            {
                resultText.text = playerWon ? WinTextSingle : LoseTextSingle;
            }
        }

        float finalScore = Human.Score.Value * data.ScoreRewardMultiplier;

        if (playerWon)
        {
            finalScore *= data.WinRewardMultiplier;
            if (victorySound)
            {
                victorySound.Play();
            }
            if (victoryEffect)
            {
                victoryEffect.Play(true);
            }
        }
        else
        {
            if (victoryEffect)
            {
                victoryEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            if (defeatSound)
            {
                defeatSound.Play();
            }
        }

        currency.UpdateValues((int)finalScore, Human.Score.Value);

        if (rewardText.IsTextValid)
        {
            rewardText.Text = rewardText.Prefix + (int)finalScore + rewardText.Suffix;
        }
    }

}

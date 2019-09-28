using UnityEngine;
using System.IO;
[CreateAssetMenu(menuName = "Currency")]
public class CurrencyHolder : SaveableScriptableObject
{
    public const string CurrencyFilename = "Currency";

    public int Currency { get { return currency; } }
    public int BestScore
    {
        get { return bestScore; }
        set
        {
            if (value > bestScore)
            {
                bestScore = value;
            }
        }
    }

    public override string Filename { get { return CurrencyFilename; } }
    public override string FolderName { get { return Utils.ConfigFolderName; } }

    [SerializeField]
    private int currency;
    [SerializeField]
    private int bestScore;

    public void UpdateValues(int CurrencyToAdd, int Score)
    {
        currency += CurrencyToAdd;
        bestScore = Mathf.Max(bestScore, Score);
        Save();
    }
    public void SumCurrency(int AmountToSum)
    {
        currency += AmountToSum;
        Save();
    }

    public override void Reset()
    {
        currency = 0;
        bestScore = 0;
    }
}

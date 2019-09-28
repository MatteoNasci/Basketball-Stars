
using UnityEngine;
public class CurrencyWriter : MonoBehaviour
{
    [SerializeField]
    private TextUiData currencyUi = new TextUiData();
    [SerializeField]
    private TextUiData bestScoreUi = new TextUiData();
    [SerializeField]
    private CurrencyHolder currency;

    void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        if (!currency)
        {
            Debug.LogErrorFormat("{0} of type {1} requires a valid reference to CurrencyHolder", this, this.GetType());
            return;
        }

        if (currencyUi.IsTextValid)
        {
            currencyUi.Text = currencyUi.Prefix + currency.Currency.ToString() + currencyUi.Suffix;
        }
        if (bestScoreUi.IsTextValid)
        {
            bestScoreUi.Text = bestScoreUi.Prefix + currency.BestScore.ToString() + bestScoreUi.Suffix;
        }
    }
}

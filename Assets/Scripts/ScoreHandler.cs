
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ScoreTextData
{
    public bool IsValid { get { return Text && Field; } }
    public Text Text;
    public IntField Field;
    public int LastScore;
}
public class ScoreHandler : MonoBehaviour
{
    private const string defaultText = "";

    [SerializeField]
    private ScoreTextData[] scoreTexts = new ScoreTextData[0];

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            ScoreTextData data = scoreTexts[i];
            if (data.IsValid)
            {
                data.Text.text = defaultText;
                data.LastScore = data.Field.Value;
            }
        }
    }
    public void UpdateText()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            ScoreTextData data = scoreTexts[i];
            if (data.IsValid && data.LastScore != data.Field.Value) //update text only if score has been modified
            {
                data.LastScore = data.Field.Value;
                data.Text.text = data.LastScore.ToString();
            }
        }
    }
}

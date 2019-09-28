
using UnityEngine.UI;
using UnityEngine;
using System;
[Serializable]
public class AiStatsOption
{
    public TextUiData PrecisionText = new TextUiData();
    public TextUiData DelayText = new TextUiData();
    public Slider Precision;
    public Slider Delay;
    public AiStats Stats;

    public bool AreAllFieldsValid()
    {
        return PrecisionText.IsTextValid && DelayText.IsTextValid && Precision && Delay && Stats;
    }
}
public class AiOptionsShower : BaseOptionsShower
{
    public const float TextPrecision = 100.0f;

    [SerializeField]
    private AiStatsOption aiOptions = new AiStatsOption();

    public override void ResetValues()
    {
        if (aiOptions.Stats)
        {
            aiOptions.Stats.Reset();
        }
        else
        {
            DebugError();
        }

        OnEnable();
    }

    protected override void OnEnable()
    {
        if (aiOptions.AreAllFieldsValid())
        {
            aiOptions.Precision.value = aiOptions.Stats.Precision;
            aiOptions.Delay.value = aiOptions.Stats.LaunchDelay;
        }
        else
        {
            DebugError();
        }

        base.OnEnable();
    }
    protected override void UpdateText()
    {
        if (aiOptions.AreAllFieldsValid())
        {
            aiOptions.PrecisionText.Text = aiOptions.PrecisionText.Prefix + ((int)(aiOptions.Precision.value * TextPrecision) / TextPrecision).ToString() + aiOptions.PrecisionText.Suffix;
            aiOptions.DelayText.Text = aiOptions.DelayText.Prefix + ((int)(aiOptions.Delay.value * TextPrecision) / TextPrecision).ToString() + aiOptions.DelayText.Suffix;
        }
        else
        {
            DebugError();
        }
    }
    private void OnDisable()
    {
        if (aiOptions.Stats)
        {
            aiOptions.Stats.Save();
        }
        else
        {
            DebugError();
        }
    }
    void DebugError()
    {
        Debug.LogErrorFormat("{0} of type {1} requires AiStatsOption to be fully initialized correctly", this, this.GetType());
    }
}

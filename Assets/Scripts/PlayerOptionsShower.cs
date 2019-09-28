using UnityEngine.UI;
using System;
using UnityEngine;
[Serializable]
public class PlayerOption
{
    public bool IsValid { get { return MaxForceAmountText.IsTextValid && MinForceAmountText.IsTextValid && MaxLaunchDurationText.IsTextValid && ForceMultiplierText.IsTextValid && MaxForceAmount && MinForceAmount && MaxLaunchDuration && ForceMultiplier && Data; } }
    public TextUiData MaxForceAmountText = new TextUiData();
    public TextUiData MinForceAmountText = new TextUiData();
    public TextUiData MaxLaunchDurationText = new TextUiData();
    public TextUiData ForceMultiplierText = new TextUiData();
    public Slider MaxForceAmount;
    public Slider MinForceAmount;
    public Slider MaxLaunchDuration;
    public Slider ForceMultiplier;
    public PlayerData Data;
}
public class PlayerOptionsShower : BaseOptionsShower
{
    public const float TextPrecision = 100.0f;

    [SerializeField]
    private PlayerOption playerOptions = new PlayerOption();

    public override void ResetValues()
    {
        if (playerOptions.Data)
        {
            playerOptions.Data.Reset();
        }
        else
        {
            DebugError();
        }
        OnEnable();
    }

    protected override void OnEnable()
    {
        if (playerOptions.IsValid)
        {
            playerOptions.MaxForceAmount.value = playerOptions.Data.MaxForceAmount;
            playerOptions.MinForceAmount.value = playerOptions.Data.MinForceAmount;
            playerOptions.MaxLaunchDuration.value = playerOptions.Data.MaxLaunchDuration;
            playerOptions.ForceMultiplier.value = playerOptions.Data.ForceMultiplier;
        }
        else
        {
            DebugError();
        }
        base.OnEnable();
    }
    protected override void UpdateText()
    {
        if (playerOptions.IsValid)
        {
            playerOptions.MaxForceAmountText.Text = playerOptions.MaxForceAmountText.Prefix + ((int)(playerOptions.MaxForceAmount.value * TextPrecision) / TextPrecision).ToString() + playerOptions.MaxForceAmountText.Suffix;
            playerOptions.MinForceAmountText.Text = playerOptions.MinForceAmountText.Prefix + ((int)(playerOptions.MinForceAmount.value * TextPrecision) / TextPrecision).ToString() + playerOptions.MinForceAmountText.Suffix;
            playerOptions.MaxLaunchDurationText.Text = playerOptions.MaxLaunchDurationText.Prefix + ((int)(playerOptions.MaxLaunchDuration.value * TextPrecision) / TextPrecision).ToString() + playerOptions.MaxLaunchDurationText.Suffix;
            playerOptions.ForceMultiplierText.Text = playerOptions.ForceMultiplierText.Prefix + ((int)(playerOptions.ForceMultiplier.value * TextPrecision) / TextPrecision).ToString() + playerOptions.ForceMultiplierText.Suffix;
        }
        else
        {
            DebugError();
        }
    }
    private void OnDisable()
    {
        if (playerOptions.Data)
        {
            playerOptions.Data.Save();
        }
        else
        {
            DebugError();
        }
    }
    void DebugError()
    {
        Debug.LogErrorFormat("{0} of type {1} is not initiallized correctly", this, this.GetType());
    }
}
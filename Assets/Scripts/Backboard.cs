
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

[Serializable]
public class BonusLaunchToggled : UnityEvent<bool> { }

public class Backboard : MonoBehaviour
{
    public const string PrefixBonusText = "+";
    public bool IsBonusLaunchActive { get { return launchBonusWhenHit != 0; } }

    [SerializeField]
    private BonusLaunchToggled onBonusLaunchToggled;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float chanceToActivateBonusLaunch = 0.25f;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float chanceToDeactivateBonusLaunch = 0.5f;

    [Range(0, 5)]
    [SerializeField]
    private int minLaunchBonus = 4;
    [Range(5, 6)]
    [SerializeField]
    private int maxLaunchBonus = 5;

    [SerializeField]
    private Text bonusAmountText;

    private int launchBonusWhenHit;

    private void OnValidate()
    {
        if (!bonusAmountText)
        {
            bonusAmountText = GetComponentInChildren<Text>(true);
        }
    }
    private void Reset()
    {
        if (Application.isEditor)
        {
            OnValidate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.transform.root.GetComponent<Player>();
        if (player)
        {
            player.OnLaunchDirtied(launchBonusWhenHit);
        }
    }
    public void TryToToggleBonusLaunch()
    {
        int previousBonus = launchBonusWhenHit;

        if (!IsBonusLaunchActive) //if bonus is active try to activate it, otherwise try to deactivate it
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= chanceToActivateBonusLaunch)
            {
                launchBonusWhenHit = UnityEngine.Random.Range(minLaunchBonus, maxLaunchBonus + 1);
            }
        }
        else if (UnityEngine.Random.Range(0.0f, 1.0f) <= chanceToDeactivateBonusLaunch)
        {
            launchBonusWhenHit = 0;
        }

        if (launchBonusWhenHit != previousBonus)
        {
            if (bonusAmountText)
            {
                bonusAmountText.text = PrefixBonusText + launchBonusWhenHit.ToString();
            }
            onBonusLaunchToggled.Invoke(IsBonusLaunchActive);
        }
    }

}

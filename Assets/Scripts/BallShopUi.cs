using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
[Serializable]
public class BallShopUiElements
{
    public GameObject BallEquippedSign;
    public TextPopup BallEquippedPopup;
    public GameObject UnlockUi;
    public GameObject ItemNotOwnedUi;
    public TextUiData BuyText = new TextUiData();
}
public class BallShopUi : MonoSequencer<Rotator>
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private CurrencyHolder currency;
    [SerializeField]
    private BallData[] balls = new BallData[0];
    [SerializeField]
    private Transform spawnsParent;

    [SerializeField]
    private float rotationSpeed = 90.0f;
    [SerializeField]
    private Axis rotationAx = Axis.Y;

    [SerializeField]
    private BallShopUiElements ui = new BallShopUiElements();

    public bool IsBallEquipped(BallData ball)
    {
        return ((ball && playerData) ? ball.PrefabPath == playerData.BallPath : false);
    }
    public void TryEquipSelected()
    {
        if (playerData && IsCurrentIndexValid)
        {
            BallData ballData = balls[CurrentIndex];

            if (ballData && ballData.IsBought)
            {
                playerData.BallPath = ballData.PrefabPath;
                if (ui.BallEquippedPopup)
                {
                    ui.BallEquippedPopup.StartPopup();
                }
            }
        }
        UpdateUi(true);
    }
    public void BuySelected()
    {
        TryBuySelected();
    }
    public bool TryBuySelected()
    {
        if (IsCurrentIndexValid && currency)
        {
            BallData current = balls[CurrentIndex];
            if (current)
            {
                bool bought = current.Buy(currency);
                UpdateUi();
                return bought;
            }
        }
        return false;
    }
    public override void ActivateIndex(int IndexToActivate)
    {
        base.ActivateIndex(IndexToActivate);
        UpdateUi();
    }
    private void Awake()
    {
        toSequence = new Rotator[balls.Length];
        for (int i = 0; i < toSequence.Length; i++)
        {
            BallData ball = balls[i];
            if (!ball)
            {
                Debug.LogErrorFormat("{0} of type {1} found a null reference in its ballsdata array index {2}!", this, this.GetType(), i);
                continue;
            }

            GameObject prefab = ball.GetBallPrefab();
            if (!prefab)
            {
                Debug.LogErrorFormat("{0} of type {1} found a null reference while attempting to get ball prefab from ballsdata array index {2}!", this, this.GetType(), i);
                continue;
            }

            Rotator rotator = GameObject.Instantiate(prefab, spawnsParent).AddComponent<Rotator>();
            rotator.gameObject.SetActive(false);
            rotator.SetValues(this.rotationSpeed, this.rotationAx);
            rotator.transform.localPosition = Vector3.zero;
            toSequence[i] = rotator;
        }
        if (!playerData || !currency)
        {
            Debug.LogErrorFormat("{0} of type {1} required PlayerData and CurrencyHolder valid references!", this, this.GetType());
            gameObject.SetActive(false);
        }
    }
    protected override void OnDisable()
    {
        if (playerData)
        {
            playerData.Save();
        }
        base.OnDisable();
    }
    protected override void OnEnable()
    {
        if (ui.BallEquippedSign)
        {
            ui.BallEquippedSign.SetActive(false);
        }
        if (ui.BuyText.IsTextValid)
        {
            ui.BuyText.Ui.gameObject.SetActive(false);
        }
        if (ui.ItemNotOwnedUi)
        {
            ui.ItemNotOwnedUi.SetActive(false);
        }
        if (ui.UnlockUi)
        {
            ui.UnlockUi.SetActive(false);
        }
        base.OnEnable();
    }
    private void UpdateUi(bool requestBuy = false)
    {
        if (IsCurrentIndexValid)
        {
            BallData selected = balls[CurrentIndex];
            bool isCurrentEquipped = IsBallEquipped(selected);

            if (selected && ui.ItemNotOwnedUi)
            {
                ui.ItemNotOwnedUi.SetActive(!selected.IsBought);
            }
            if (ui.BallEquippedSign)
            {
                ui.BallEquippedSign.SetActive(isCurrentEquipped);
            }
            if (requestBuy && selected && !selected.IsBought)
            {
                if (ui.BuyText.IsTextValid)
                {
                    ui.BuyText.Text = ui.BuyText.Prefix + selected.Price.ToString() + ui.BuyText.Suffix;
                    ui.BuyText.Ui.gameObject.SetActive(true);
                }
                if (ui.UnlockUi)
                {
                    ui.UnlockUi.SetActive(true);
                }
            }
        }
    }
}

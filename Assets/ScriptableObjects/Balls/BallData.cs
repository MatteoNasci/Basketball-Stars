using UnityEngine;
[CreateAssetMenu(menuName = "Ball Data")]
public class BallData : SaveableScriptableObject, ISellable
{
    public bool IsBought { get { return isBought; } }

    public int Price { get { return price; } }

    public string PrefabPath { get { return prefabPath; } }

    public override string Filename { get { return this.name; } }

    public override string FolderName { get { return Utils.BallsFolderName; } }

    [SerializeField]
    private int price;
    [SerializeField]
    private bool isBought;
    [SerializeField]
    private string prefabPath = string.Empty;

    private GameObject ballPrefab;

    public GameObject GetBallPrefab()
    {
        if (!ballPrefab)
        {
            ballPrefab = Resources.Load<GameObject>(prefabPath);
        }
        return ballPrefab;
    }
    protected void OnDisable()
    {
        ballPrefab = null;
    }
    public bool Buy(CurrencyHolder Currency)
    {
        if (!isBought && Currency && Currency.Currency >= price)
        {
            isBought = !isBought;
            Currency.SumCurrency(-price);
            Save();
            return true;
        }
        return false;
    }

    public override void Reset()
    {

    }
}

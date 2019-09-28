
public interface ISellable : ISaveable
{
    bool IsBought { get; }
    int Price { get; }
    bool Buy(CurrencyHolder currency);
}
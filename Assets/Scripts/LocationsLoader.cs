using UnityEngine;

public class LocationsLoader : MonoBehaviour
{
    [SerializeField]
    private Transform locationsParent;
    [SerializeField]
    private LaunchLocations locationsHolder;

    public void LoadLocations()
    {
        ClearLocations();
        if (locationsHolder && locationsParent)
        {
            locationsHolder.SetChildsAsLocations(locationsParent);
        }
        else
        {
            Debug.LogErrorFormat("{0} of type {1} required a valid reference to location parent and location holder", this, this.GetType());
        }
    }
    private void Awake()
    {
        LoadLocations();
    }
    private void OnDestroy()
    {
        ClearLocations();
    }
    private void ClearLocations()
    {
        if (locationsHolder)
        {
            locationsHolder.ClearLocations();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Locations Holder")]
public class LaunchLocations : ScriptableObject
{

    private List<LaunchLocation> locations = new List<LaunchLocation>();
    public void ClearLocations()
    {
        locations.Clear();
    }
    public void SetChildsAsLocations(Transform parent)
    {
        if (parent)
        {
            ClearLocations();
            parent.GetComponentsInChildren<LaunchLocation>(false, locations);
        }
    }
    public LaunchLocation GetRandomLocation(out int outLocationIndex, int excludedIndex = -1)
    {
        outLocationIndex = Utils.GetRandomInt(0, locations.Count, excludedIndex);

        return GetLocation(outLocationIndex);
    }
    public LaunchLocation GetLocation(int index)
    {
        if (locations.Count == 0)
        {
            throw new System.Exception("No Launch locations available!");
        }
        if (index < 0)
        {
            index = 0;
        }
        return locations[index % locations.Count];
    }
}

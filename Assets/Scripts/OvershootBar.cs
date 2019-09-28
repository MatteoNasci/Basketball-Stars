using UnityEngine;
public class OvershootBar : ScaledProgressBar
{
    [SerializeField]
    private FloatField suggestedLaunchForce;
    [SerializeField]
    private GameData data;
    [SerializeField]
    private PlayerData playerData;
    private void OnEnable()
    {
        this.enabled = false;
        if(!data || !suggestedLaunchForce)
        {
            Debug.LogErrorFormat("{0} of type {1} requires gamedata and a floatfield suggestedforce valid references", this, this.GetType());
            return;
        }

        if (data.UseOvershootBar)
        {
            SetFillRate(1.0f - ((suggestedLaunchForce.Value - playerData.MinForceAmount) / (playerData.MaxForceAmount - playerData.MinForceAmount)));
        }
    }
}

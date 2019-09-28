using UnityEngine;

public class InitializePlayersData : MonoBehaviour
{
    [SerializeField]
    private PlayerData data;
    [SerializeField]
    private BallData defaultBall;

    private void Awake()
    {
        GameObject.Destroy(gameObject, Utils.DefaultDestroyDelay);
        if (!data || !defaultBall)
        {
            Debug.LogWarningFormat("{0} of type {1} does not have all necessary references to work", this, this.GetType());
            return;
        }

        data.Load();
        if (data.BallPath == null || data.BallPath.Length == 0)
        {
            data.BallPath = defaultBall.PrefabPath;
            data.Save();
        }
    }
}

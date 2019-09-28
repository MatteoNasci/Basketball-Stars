using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class GameAreaExited : UnityEvent<Player> { }

public class GameArea : MonoBehaviour
{
    [SerializeField]
    private GameAreaExited onGameAreaExited;

    private void OnTriggerExit(Collider other)
    {
        Player player = other.transform.root.GetComponent<Player>();
        if (player)
        {
            player.OnGameAreaExited();
            onGameAreaExited.Invoke(player);
        }
    }
}

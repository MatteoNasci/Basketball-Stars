using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
[Serializable]
public class OnEndGame : UnityEvent { }

public class WaitForEndGame : MonoBehaviour
{
    [SerializeField]
    private OnEndGame endGame;

    [SerializeField]
    private PlayerData playersToWait;
	// Update is called once per frame
	void Update ()
    {
        bool readyToEnd = true;

        for (int i = 0; i < playersToWait.GetPlayersCount(); i++)
        {
            Player player = playersToWait.GetPlayer(i);
            if (player)
            {
                if (player.IsBallLaunching)
                {
                    readyToEnd = false;
                }
                else
                {
                    player.transform.root.gameObject.SetActive(false);
                }
            }
        }

        //proceed to end the game only when all players are not currently in their launch phase

        if (readyToEnd)
        {
            this.enabled = false;
            endGame.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerAudio : RandomAudio
{
    [SerializeField]
    private int playerId;

    protected override void OnValidate()
    {
        base.OnValidate();

        Player player = GetComponent<Player>();
        if (player)
        {
            playerId = player.Id;
        }
    }
    public void Play(Player player)
    {
        if(player && this.playerId == player.Id)
        {
            Play();
        }
    }
    public void Stop(Player player)
    {
        if (player && this.playerId == player.Id)
        {
            Stop();
        }
    }
}

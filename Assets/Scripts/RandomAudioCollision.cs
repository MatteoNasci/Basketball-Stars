using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioCollision : RandomAudio
{
    private void OnCollisionEnter(Collision collision)
    {
        Play();
    }
}

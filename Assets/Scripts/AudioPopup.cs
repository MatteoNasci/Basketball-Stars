using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPopup : MonoPopup<AudioSource>
{
    protected override ActivateTarget GetActivationAction()
    {
        return StartAudio;
    }

    protected override DeactivateTarget GetDeactivationAction()
    {
        return StopAudio;
    }

    private static void StopAudio(AudioSource target)
    {
        if (target)
        {
            target.enabled = false;
        }
    }

    private static void StartAudio(AudioSource target)
    {
        if (target)
        {
            target.enabled = true;
        }
    }
}

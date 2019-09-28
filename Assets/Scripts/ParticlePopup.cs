using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePopup : MonoPopup<ParticleSystem>
{
    private static void StopParticle(ParticleSystem target)
    {
        if (target)
        {
            target.Stop(true);
        }
    }

    private static void StartParticle(ParticleSystem target)
    {
        if (target)
        {
            target.Play(true);
        }
    }

    protected override ActivateTarget GetActivationAction()
    {
        return StartParticle;
    }

    protected override DeactivateTarget GetDeactivationAction()
    {
        return StopParticle;
    }
}

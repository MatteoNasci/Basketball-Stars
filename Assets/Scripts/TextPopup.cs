using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoPopup<Text>
{
    protected override ActivateTarget GetActivationAction()
    {
        return EnableText;
    }

    private static void EnableText(Text target)
    {
        if (target)
        {
            target.enabled = true;
        }
    }

    protected override DeactivateTarget GetDeactivationAction()
    {
        return DisableText;
    }

    private static void DisableText(Text target)
    {
        if (target)
        {
            target.enabled = false;
        }
    }
}
